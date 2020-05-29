using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;

namespace GameStoreAppCF.Models
{
    public class ReportManager
    {
        public static List<GamesToReportViewModel> PrepareGamesToReport(GameStoreDB db, DateTime dateFrom, DateTime dateTo)
        {
            var gamesToReport = new List<GamesToReportViewModel>();
            var games = new List<Game>();
            var actualDateTo = dateTo.AddDays(1);
            var orders = (from order in db.Order
                         where order.Date >= dateFrom &&
                         order.Date <= actualDateTo
                         select order).ToList();
            foreach (var order in orders)
            {
                foreach (var game in order.Game)
                {
                    games.Add(game);
                }
            }
            var groupedGames = from game in games group games by game.Name;
            foreach (var game in groupedGames)
            {
                gamesToReport.Add(new GamesToReportViewModel()
                {
                    Name = game.Key,
                    Quantity = game.Count(),
                    Price = game.First().First().Price,
                    Total = game.First().First().Price * game.Count()
                });
            }
            return gamesToReport;
            
        }

        public static byte[] GenerateChartImage(List<GamesToReportViewModel> info)
        {
            return new Chart(width: 600, height: 400)
                .AddSeries("Default", chartType: "Pie",
                    xValue: info, xField: "Name",
                    yValues: info, yFields: "Total").GetBytes();
        }

        public static string CreateReport(
            DateTime dateFrom,
            DateTime dateTo,
            List<GamesToReportViewModel> list,
            string templateFileName, 
            string receiptFileName)
        {
            File.Copy(templateFileName, receiptFileName);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(receiptFileName, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                docText = ReplaceByWord(docText, "dateFrom", dateFrom.ToString("dd-MM-yyyy"));
                docText = ReplaceByWord(docText, "dateTo", dateTo.ToString("dd-MM-yyyy"));
                docText = ReplaceByWord(docText, "Total", (from game in list select game.Total).Sum().ToString() + " ₽");
                using (StreamWriter sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.OpenOrCreate)))
                {
                    sw.Write(docText);
                }
                wordDocument.Save();
                EditTable(wordDocument, list);
                return receiptFileName;
            }

        }

        static string ReplaceByWord(string docText, string template, string replacement)
        {
            Regex regexText = new Regex(template);
            return regexText.Replace(docText, replacement);
        }

        static Table EditTable(WordprocessingDocument doc, List<GamesToReportViewModel> list)
        {
            Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().First();
            int i = 1;
            foreach (var game in list)
            {
                table.AppendChild(CreateRow(i++, game));
            }

            return table;
        }

        static TableRow CreateRow(int i, GamesToReportViewModel game)
        {
            // Create 1 row to the table.
            TableRow tr1 = new TableRow();
            // Add a cell to each column in the row.
            TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(game.Name))));
            TableCell tc2 = new TableCell(new Paragraph(new Run(new Text(game.Price.ToString() + " ₽"))));
            TableCell tc3 = new TableCell(new Paragraph(new Run(new Text(game.Quantity.ToString()))));
            TableCell tc4 = new TableCell(new Paragraph(new Run(new Text(game.Total.ToString() + " ₽"))));
            tr1.Append(tc1, tc2, tc3, tc4);
            return tr1;
        }
    }
}
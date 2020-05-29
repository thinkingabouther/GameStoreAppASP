using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class ReceiptManager
    {
        public static string CreateReceipt(Order order, string templateFileName, string receiptFileName)
        {
            File.Copy(templateFileName, receiptFileName);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(receiptFileName, true))
            {
                string docText = null;

                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                EditTable(wordDocument, order);
                docText = ReplaceByWord(docText, "ClientName", order.Client.Name);
                docText = ReplaceByWord(docText, "ClientEmail", order.Client.Email);
                docText = ReplaceByWord(docText, "OrderID", order.ID.ToString());
                docText = ReplaceByWord(docText, "Total", (from game in order.Game
                                                           select game.Price).Sum().ToString());

                using (StreamWriter sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.OpenOrCreate)))
                {
                    sw.Write(docText);
                }
                wordDocument.Save();
                return receiptFileName;
            }
        }

        static string ReplaceByWord(string docText, string template, string replacement)
        {
            Regex regexText = new Regex(template);
            return regexText.Replace(docText, replacement);
        }

        static Table EditTable(WordprocessingDocument doc, Order order)
        {
            Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().First();
            int i = 1;
            var groupedGames = from game in order.Game group game by game.Name;
            foreach (var game in groupedGames)
            {
                table.AppendChild(CreateRow(i++, game));
            }

            return table;
        }

        static TableRow CreateRow(int i, IGrouping<string, Game> game)
        {
            // Create 1 row to the table.
            TableRow tr1 = new TableRow();
            // Add a cell to each column in the row.
            TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(game.Key))));
            TableCell tc2 = new TableCell(new Paragraph(new Run(new Text(game.First().Price.ToString() + " ₽"))));
            TableCell tc3 = new TableCell(new Paragraph(new Run(new Text(game.Count().ToString()))));
            TableCell tc4 = new TableCell(new Paragraph(new Run(new Text((game.Count() * game.First().Price).ToString() + " ₽"))));
            tr1.Append(tc1, tc2, tc3, tc4);
            return tr1;
        }
    }
}
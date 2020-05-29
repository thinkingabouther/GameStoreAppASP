using GameStoreAppCF.Models;
using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.Controllers
{
    public class ReportsController : Controller
    {
        GameStoreDB db = new GameStoreDB();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report(string dateFrom, string dateTo)
        {
            TempData["dateTo"] = dateTo;
            TempData["dateFrom"] = dateFrom;
            DateTime _dateFrom = DateTime.Parse(dateFrom);
            DateTime _dateTo = DateTime.Parse(dateTo);
            var games = ReportManager.PrepareGamesToReport(db, _dateFrom, _dateTo);
            ViewBag.Chart = ReportManager.GenerateChartImage(games);
            TempData["model"] = games;
            return PartialView(games);
        }

        public ActionResult ExportReport()
        {
            DateTime dateTo = DateTime.Parse(TempData["dateTo"].ToString());
            DateTime dateFrom = DateTime.Parse(TempData["dateFrom"].ToString());
            List<GamesToReportViewModel> model = (List<GamesToReportViewModel>)TempData["model"];
            string directoryPath = "~/ReportTemplates/";
            string fileName = $"Report{dateFrom:dd-MM-yyyy}-{dateTo:dd-MM-yyyy}.docx";
            string templateFile = "ReportTemplate.docx";
            string reportFileName = Server.MapPath(directoryPath + fileName);
            string templateFileName = Server.MapPath(directoryPath + templateFile);
            ReportManager.CreateReport(dateFrom, dateTo, model, templateFileName, reportFileName);
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(reportFileName);
            Response.End();
            string virtualPath = Request.MapPath(directoryPath + fileName);
            TempData.Keep("dateTo");
            TempData.Keep("dateFrom");
            TempData.Keep("model");
            System.IO.File.Delete(virtualPath);
            return new HttpStatusCodeResult(202);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
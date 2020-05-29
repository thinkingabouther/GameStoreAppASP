using GameStoreAppCF.Models;
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
            DateTime _dateFrom = DateTime.Parse(dateFrom);
            DateTime _dateTo = DateTime.Parse(dateTo);
            var games = ReportManager.PrepareGamesToReport(db, _dateFrom, _dateTo);
            return PartialView(games);
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
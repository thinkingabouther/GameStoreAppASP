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
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report(string dateFrom, string dateTo)
        {
            DateTime _dateFrom = DateTime.Parse(dateFrom);
            DateTime _dateTo = DateTime.Parse(dateTo);
            var games = ReportManager.PrepareGamesToReport(_dateFrom, _dateTo);
            return PartialView(games);
        }
    }
}
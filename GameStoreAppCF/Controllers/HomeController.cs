using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStoreAppCF.Models;


namespace GameStoreAppCF.Controllers
{
    public class HomeController : Controller
    {
        private GameStoreDB db = new GameStoreDB();
        public ActionResult Index()
        {
            var game = db.Game.Include(g => g.Author).Include(g => g.Genre).Include(g => g.Manufacturer).Include(g => g.Type);
            return View(game.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
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
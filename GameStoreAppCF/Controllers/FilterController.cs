using GameStoreAppCF.Models;
using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FormCollection = System.Web.Mvc.FormCollection;

namespace GameStoreAppCF.Controllers
{
    public class FilterController : Controller
    {
        private GameStoreDB db = new GameStoreDB();

        // GET: Filter
        public ActionResult Index()
        {
            ViewBag.Genres = new SelectList(db.Genre, "ID", "Name");
            ViewBag.Types = new SelectList(db.Type, "ID", "Name");
            var filter = new FilterViewModel();
            filter.MinDuration = FilterQueries.GetMinDuration();
            filter.MaxDuration = FilterQueries.GetMaxDuration();
            filter.MinPlayers = FilterQueries.GetMinPlayers();
            filter.MaxPlayers = FilterQueries.GetMaxPlayers();
            filter.MaxPrice = FilterQueries.GetMaxPrice();
            filter.MinPrice = FilterQueries.GetMinPrice();
            filter.MaxDifficulty = 5;
            filter.MinDifficulty = 1;
            return PartialView(filter);
        }


    }
}
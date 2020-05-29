using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStoreAppCF.Models;
using GameStoreAppCF.ViewModels;

namespace GameStoreAppCF.Controllers
{
    public class HomeController : Controller
    {
        private GameStoreDB db = new GameStoreDB();
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
            return View(filter);
        }

        public ActionResult ShowGames()
        {
            var game = db.Game;
            return PartialView("~\\Views\\Home\\GamesCatalog.cshtml", game.ToList());
        }

        public ActionResult GamesCatalog(FormCollection formcollection)
        {
            var type = formcollection["Type"] == String.Empty ? null : db.Type.Find(int.Parse(formcollection["Type"])).Name;
            var genre = formcollection["Genre"] == String.Empty ? null : db.Genre.Find(int.Parse(formcollection["Genre"])).Name;
            var price = formcollection["price"];
            var minPrice = double.Parse(price.Substring(0, price.IndexOf('₽'))) - 1;
            var maxPrice = double.Parse(price.Substring(price.LastIndexOf(' ') + 1, price.LastIndexOf('₽') - price.LastIndexOf(' ') - 1)) + 1;
            var difficulty = formcollection["diffiulty"];
            var minDifficulty = int.Parse(difficulty.Substring(0, difficulty.IndexOf(' ')));
            var maxDifficulty = int.Parse(difficulty.Substring(difficulty.LastIndexOf(' ') + 1));
            var players = formcollection["players"];
            var minPlayers = int.Parse(players.Substring(0, players.IndexOf(' ')));
            var maxPlayers = int.Parse(players.Substring(players.LastIndexOf(' ') + 1));
            var duration = formcollection["duration"];
            var minDuration = int.Parse(duration.Substring(0, duration.IndexOf(' ')));
            var maxDuration = int.Parse(duration.Substring(duration.LastIndexOf(' ') + 1));
            var startswith = formcollection["startswith"];
            var games = FilterQueries.GetGamesParamsWithFilter(db, startswith, minPrice, maxPrice, minDuration, maxDuration, minDifficulty, maxDifficulty, minPlayers, maxPlayers, genre, type);
            return PartialView(games);
        }

        public ActionResult OpenGameDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View("~\\Views\\SingleGames\\Index.cshtml", game);
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
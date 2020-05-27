using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace GameStoreAppCF.Controllers
{
    public class GamesCatalogController : Controller
    {
        private GameStoreDB db = new GameStoreDB();
        // GET: GamesCatalog
        public ActionResult Index()
        {
            var game = db.Game;
            return View(game.ToList());
        }

        public ActionResult ShowGames(List<Game> games)
        {
            return View(games);
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

        public ActionResult ApplyFilter(FormCollection formcollection)
        {
            var type = db.Type.Find(int.Parse(formcollection["Type"]));
            var genre = db.Genre.Find(int.Parse(formcollection["Genre"]));
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
            var games = FilterQueries.GetGamesParamsWithFilter(startswith, minPrice, maxPrice, minDuration, maxDuration, minDifficulty, maxDifficulty, minPlayers, maxPlayers, genre.Name, type.Name);
            return PartialView(games);
        }
    }
}
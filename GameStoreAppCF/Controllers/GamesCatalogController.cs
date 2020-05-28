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
    }
}
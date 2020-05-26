using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.Controllers
{
    public class SingleGamesController : Controller
    {
        // GET: SingleGames
        public ActionResult Index(Game game)
        {
            return View(game);
        }
    }
}
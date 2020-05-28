using GameStoreAppCF.Models;
using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.Controllers
{
    public class CartController : Controller
    {
        GameStoreDB db = new GameStoreDB();
        // GET: Cart
        public ActionResult Cart()
        {
            var games = GetCartDetails(out double total);
            ViewBag.Total = total;
            return PartialView(games);
        }

        public ActionResult Delete(string name)
        {
            var cart = (Cart)Session["Cart"];
            cart.Delete(name);
            Session["Cart"] = cart;
            return RedirectToAction("Index", "Home");
        }

        private List<GamesToCartViewModel> GetCartDetails(out double total)
        {
            var games = new List<GamesToCartViewModel>();
            var cart = (Cart)Session["Cart"];
            total = 0;
            if (cart != null)
            {
                foreach (var gameCountPair in cart.Games)
                {
                    var game = (from currentgame in db.Game
                                where currentgame.Name == gameCountPair.Key
                                select currentgame).Single();
                    games.Add(new GamesToCartViewModel()
                    {
                        Name = game.Name,
                        Quantity = gameCountPair.Value,
                        Price = game.Price,
                        Total = game.Price * gameCountPair.Value
                    });
                    total += game.Price * gameCountPair.Value;
                }
            }
            return games;
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
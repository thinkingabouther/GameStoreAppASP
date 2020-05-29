using GameStoreAppCF.Models;
using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.Controllers
{
    public class OrderConfirmationController : Controller
    {
        GameStoreDB db = new GameStoreDB();
        // GET: OrderConfirmation
        public ActionResult Index()
        {
            var games = GetCartDetails(out double total);
            ViewBag.Total = total;
            return View(games);
        }
        public ActionResult AddOrderToDB(FormCollection collection)
        {
            var name = collection["Name"];
            var email = collection["Email"];
            var model = GetCartDetails(out double total);
            var order = CreateOrder(model);
            var client = new Client
            {
                Name = name,
                Email = email,
            };
            db.Client.Add(client);
            order.Client = client;
            db.Order.Add(order);
            db.SaveChanges();
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

        private Order CreateOrder(List<GamesToCartViewModel> games)
        {
            var order = new Order();
            foreach (var game in games)
            {
                var currentGame = (from gameEntity in db.Game
                                    where gameEntity.Name == game.Name
                                    select gameEntity).Single();
                for (int i = 0; i < game.Quantity; i++)
                {
                    order.Game.Add(currentGame);
                }
            }
            order.Date = DateTime.Now;
            return order;            
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

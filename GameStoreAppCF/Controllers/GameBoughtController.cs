using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.Controllers
{
    public class GameBoughtController : Controller
    {
        GameStoreDB db = new GameStoreDB();
        // GET: GameBought
        public ActionResult GameBoughtModal(int gameID)
        {
            int id = gameID;
            Cart cart = (Cart)Session["Cart"] != null ? (Cart)Session["Cart"] : new Cart();
            cart.Add(db.Game.Find(id).Name);
            Session["Cart"] = cart;
            return PartialView();
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
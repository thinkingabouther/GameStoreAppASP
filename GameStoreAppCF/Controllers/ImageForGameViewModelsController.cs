using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using GameStoreAppCF.Models;

namespace GameStoreAppCF.Controllers
{
    public class ImageForGameViewModelsController : Controller
    {
        private GameStoreDB db = new GameStoreDB();

        // GET: ImageForGameViewModels/Create
        public ActionResult Create(int? id)
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
            return View(new ImageForGameViewModel { GameID = game.ID});
        }

        // POST: ImageForGameViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,GameID,Image")] ImageForGameViewModel imageForGameViewModel)
        {
            Game game = db.Game.Find(imageForGameViewModel.GameID);
            MemoryStream target = new MemoryStream();
            imageForGameViewModel.Image.InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            game.Image = data;
            db.SaveChanges();
            return RedirectToAction("Index", "Games");
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

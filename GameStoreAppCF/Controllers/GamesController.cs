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
    public class GamesController : Controller
    {
        private GameStoreDB db = new GameStoreDB();

        // GET: Games
        public ActionResult Index()
        {
            var game = db.Game.Include(g => g.Author).Include(g => g.Genre).Include(g => g.Manufacturer).Include(g => g.Type);
            return View(game.ToList());
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.Author_ID = new SelectList(db.Author, "ID", "Name");
            ViewBag.Genre_ID = new SelectList(db.Genre, "ID", "Name");
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturer, "ID", "Name");
            ViewBag.Type_ID = new SelectList(db.Type, "ID", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Difficulty,Min_Duration,Max_Duration,Min_Players,Max_Players,Price,Quantity,Image,Author_ID,Type_ID,Genre_ID,Manufacturer_ID")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author_ID = new SelectList(db.Author, "ID", "Name", game.Author_ID);
            ViewBag.Genre_ID = new SelectList(db.Genre, "ID", "Name", game.Genre_ID);
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturer, "ID", "Name", game.Manufacturer_ID);
            ViewBag.Type_ID = new SelectList(db.Type, "ID", "Name", game.Type_ID);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Author_ID = new SelectList(db.Author, "ID", "Name", game.Author_ID);
            ViewBag.Genre_ID = new SelectList(db.Genre, "ID", "Name", game.Genre_ID);
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturer, "ID", "Name", game.Manufacturer_ID);
            ViewBag.Type_ID = new SelectList(db.Type, "ID", "Name", game.Type_ID);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Difficulty,Min_Duration,Max_Duration,Min_Players,Max_Players,Price,Quantity,Image,Author_ID,Type_ID,Genre_ID,Manufacturer_ID")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_ID = new SelectList(db.Author, "ID", "Name", game.Author_ID);
            ViewBag.Genre_ID = new SelectList(db.Genre, "ID", "Name", game.Genre_ID);
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturer, "ID", "Name", game.Manufacturer_ID);
            ViewBag.Type_ID = new SelectList(db.Type, "ID", "Name", game.Type_ID);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
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
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
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

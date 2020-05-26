using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace GameStoreAppCF.Controllers
{
    public class CreateReviewController : Controller
    {
        private GameStoreDB db = new GameStoreDB();
        // GET: CreateReview
        public ActionResult CreateReview(Review review)
        {
            return View(review);
        }

        public ActionResult AddReview(Review review)
        {
            review.Date = DateTime.Now;
            db.Review.Add(review);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    MessageBox.Show($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            return RedirectToAction("OpenGameDetails", "Home", new { id = review.Game_ID });
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
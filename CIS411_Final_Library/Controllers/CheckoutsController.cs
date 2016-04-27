using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS411_Final_Library.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace CIS411_Final_Library.Controllers
{
    [Authorize]
    public class CheckoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Checkouts
        public ActionResult Index([Bind(Prefix = "id")] int bookId)
        {
            var book = db.Books.Find(bookId);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        // GET: Checkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // GET: Checkouts/Create
        public ActionResult Create(int Id)
        {

            var book = db.Books.Find(Id);
            Checkout checkout = new Checkout();
            checkout.Books = book;
            checkout.CheckoutDate = DateTime.Today;
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckoutID,BookID,CheckoutDate,Quantity,DueDate")] Checkout checkout, int id)
        {

            if (ModelState.IsValid)
            {
                //create the due date 7 days from checkout date
                checkout.DueDate = checkout.CheckoutDate.AddDays(7);

                //find the book that is being checked out and update its quantity
                Book book = db.Books.Find(id);
                book.Quantity = book.Quantity - checkout.Quantity;
                checkout.Books = book;

                string x = User.Identity.GetUserId();
                checkout.ApplicationUsers = db.Users.Find(x);

                db.Checkouts.Add(checkout);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = checkout.BookId });
            }

            return View(checkout);
        }

        //// GET: Checkouts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    var checkout = db.Checkouts.Find(id);
        //    if (checkout == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(checkout);
        //}

        //// POST: Checkouts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Checkout checkout)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(checkout).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", new { id = checkout.BookId });
        //    }
        //    return View(checkout);
        //}

        // GET: Checkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // POST: Checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Checkout checkout = db.Checkouts.Find(id);

            //if you delete the checkout, then return the quantity the book in inventory
            Book book = checkout.Books;
            book.Quantity = book.Quantity + checkout.Quantity;

            db.Checkouts.Remove(checkout);
            db.SaveChanges();
            return RedirectToAction("MyCheckouts");
        }

        public ActionResult MyCheckouts()
        {
            string currentUser = User.Identity.GetUserId();
            var checkouts = db.Checkouts.Where(p => p.ApplicationUsers.Id == currentUser);
            return View(checkouts.ToList());

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

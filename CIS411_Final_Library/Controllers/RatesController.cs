using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS411_Final_Library.Models;
using Microsoft.AspNet.Identity;

namespace CIS411_Final_Library.Controllers
{
    [Authorize]
    public class RatesController : Controller
    {
         ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rates
        public ActionResult Index([Bind(Prefix = "id")] int bookId)
        {
            var x = User.Identity;
            ViewBag.x = x.GetUserId();

            var book = db.Books.Find(bookId);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();

         }

        [HttpGet]
        public ActionResult Create (int bookId)
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = rate.BookId });
            }
            return View(rate);
        }

        //// GET: Rates/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Rates.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        //// POST: Rates/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = rate.BookId });
            }
            return View(rate);
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

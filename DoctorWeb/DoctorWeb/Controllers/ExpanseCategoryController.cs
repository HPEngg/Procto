using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using System.Data.Entity.Infrastructure;

namespace DoctorWeb.Controllers
{
    public class ExpanseCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpanseCategory
        public ActionResult Index()
        {
            return View(db.ExpanseCategories.ToList());
        }

        // GET: ExpanseCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpanseCategory expanseCategory = db.ExpanseCategories.Find(id);
            if (expanseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expanseCategory);
        }

        // GET: ExpanseCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpanseCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ExpanseCategory expanseCategory)
        {
            if (ModelState.IsValid)
            {
                db.ExpanseCategories.Add(expanseCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expanseCategory);
        }

        // GET: ExpanseCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpanseCategory expanseCategory = db.ExpanseCategories.Find(id);
            if (expanseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expanseCategory);
        }

        // POST: ExpanseCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ExpanseCategory expanseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expanseCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expanseCategory);
        }

        // GET: ExpanseCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpanseCategory expanseCategory = db.ExpanseCategories.Find(id);
            if (expanseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expanseCategory);
        }

        // POST: ExpanseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ExpanseCategory expanseCategory = db.ExpanseCategories.Find(id);
                db.ExpanseCategories.Remove(expanseCategory);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Expense Category is used in existing Expense, so it can not be deleted.";
                return RedirectToAction("Index");
            }
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

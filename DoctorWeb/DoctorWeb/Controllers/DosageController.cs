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
using PagedList;

namespace DoctorWeb.Controllers
{
    public class DosageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dosage
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dosages = from s in db.Dosages
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                dosages = dosages.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(dosages.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }
        // GET: Dosage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dosage dosage = db.Dosages.Find(id);
            if (dosage == null)
            {
                return HttpNotFound();
            }
            return View(dosage);
        }

        // GET: Dosage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dosage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Dosage dosage)
        {
            if (ModelState.IsValid)
            {
                db.Dosages.Add(dosage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dosage);
        }

        // GET: Dosage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dosage dosage = db.Dosages.Find(id);
            if (dosage == null)
            {
                return HttpNotFound();
            }
            return View(dosage);
        }

        // POST: Dosage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Dosage dosage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dosage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dosage);
        }

        // GET: Dosage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dosage dosage = db.Dosages.Find(id);
            if (dosage == null)
            {
                return HttpNotFound();
            }
            return View(dosage);
        }

        // POST: Dosage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Dosage dosage = db.Dosages.Find(id);
                db.Dosages.Remove(dosage);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Dosage is used in existing Prescription, so it can not be deleted.";
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

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
using System.Web.Configuration;

namespace DoctorWeb.Controllers
{
    [Authorize]
    public class DozController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Doz
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

            var dozes = from s in db.Dozes
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                dozes = dozes.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(dozes.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Doz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doz doz = db.Dozes.Find(id);
            if (doz == null)
            {
                return HttpNotFound();
            }
            return View(doz);
        }

        // GET: Doz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Doz doz)
        {
            if (ModelState.IsValid)
            {
                db.Dozes.Add(doz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doz);
        }

        // GET: Doz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doz doz = db.Dozes.Find(id);
            if (doz == null)
            {
                return HttpNotFound();
            }
            return View(doz);
        }

        // POST: Doz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Doz doz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doz);
        }

        // GET: Doz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doz doz = db.Dozes.Find(id);
            if (doz == null)
            {
                return HttpNotFound();
            }
            return View(doz);
        }

        // POST: Doz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Doz doz = db.Dozes.Find(id);
                db.Dozes.Remove(doz);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Doz value is assigned in existing Prescription, so it can not be deleted.";
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

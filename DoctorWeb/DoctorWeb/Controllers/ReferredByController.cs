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
    public class ReferredByController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReferredBy
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

            var referals = from s in db.ReferredBy
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                referals = referals.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(referals.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: ReferredBy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferredBy referredBy = db.ReferredBy.Find(id);
            if (referredBy == null)
            {
                return HttpNotFound();
            }
            return View(referredBy);
        }

        // GET: ReferredBy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReferredBy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ReferredBy referredBy)
        {
            if (ModelState.IsValid)
            {
                db.ReferredBy.Add(referredBy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referredBy);
        }

        // GET: ReferredBy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferredBy referredBy = db.ReferredBy.Find(id);
            if (referredBy == null)
            {
                return HttpNotFound();
            }
            return View(referredBy);
        }

        // POST: ReferredBy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ReferredBy referredBy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referredBy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referredBy);
        }

        // GET: ReferredBy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferredBy referredBy = db.ReferredBy.Find(id);
            if (referredBy == null)
            {
                return HttpNotFound();
            }
            return View(referredBy);
        }

        // POST: ReferredBy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ReferredBy referredBy = db.ReferredBy.Find(id);
                db.ReferredBy.Remove(referredBy);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This referral is already used for one or more patients, so it can not be deleted.";
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

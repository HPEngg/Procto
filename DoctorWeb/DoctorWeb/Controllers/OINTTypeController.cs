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
    public class OINTTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OINTType
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

            var ointtypes = from s in db.OINTTypes
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ointtypes = ointtypes.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(ointtypes.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: OINTType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OINTType oINTType = db.OINTTypes.Find(id);
            if (oINTType == null)
            {
                return HttpNotFound();
            }
            return View(oINTType);
        }

        // GET: OINTType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OINTType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] OINTType oINTType)
        {
            if (ModelState.IsValid)
            {
                db.OINTTypes.Add(oINTType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oINTType);
        }

        // GET: OINTType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OINTType oINTType = db.OINTTypes.Find(id);
            if (oINTType == null)
            {
                return HttpNotFound();
            }
            return View(oINTType);
        }

        // POST: OINTType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] OINTType oINTType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oINTType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oINTType);
        }

        // GET: OINTType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OINTType oINTType = db.OINTTypes.Find(id);
            if (oINTType == null)
            {
                return HttpNotFound();
            }
            return View(oINTType);
        }

        // POST: OINTType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                OINTType oINTType = db.OINTTypes.Find(id);
                db.OINTTypes.Remove(oINTType);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Medicine Type is set in one or more existing Prescription, so it can not be deleted.";
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

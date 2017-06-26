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
    public class PatientTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PatientType
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

            var patienttypes = from s in db.PatientTypes
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                patienttypes = patienttypes.Where(s => s.PatientTypeName.Contains(searchString));
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(patienttypes.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: PatientType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // GET: PatientType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PatientTypeName")] PatientType patientType)
        {
            if (ModelState.IsValid)
            {
                db.PatientTypes.Add(patientType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientType);
        }

        // GET: PatientType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // POST: PatientType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientTypeName")] PatientType patientType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientType);
        }

        // GET: PatientType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // POST: PatientType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PatientType patientType = db.PatientTypes.Find(id);
                db.PatientTypes.Remove(patientType);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Patient Type is set in one or more existing Prescription, so it can not be deleted.";
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

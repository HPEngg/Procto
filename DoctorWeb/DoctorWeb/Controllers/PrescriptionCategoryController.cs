﻿using System;
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
    public class PrescriptionCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrescriptionCategory
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var prescriptioncategories = from s in db.PrescriptionCategories
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                prescriptioncategories = prescriptioncategories.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(prescriptioncategories.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));

            //return View(db.PrescriptionCategories.ToList());
        }

        // GET: PrescriptionCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }

        // GET: PrescriptionCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrescriptionCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] PrescriptionCategory prescriptionCategory)
        {
            if (ModelState.IsValid)
            {
                db.PrescriptionCategories.Add(prescriptionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prescriptionCategory);
        }

        // GET: PrescriptionCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }

        // POST: PrescriptionCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] PrescriptionCategory prescriptionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescriptionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescriptionCategory);
        }

        // GET: PrescriptionCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }

        // POST: PrescriptionCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
                db.PrescriptionCategories.Remove(prescriptionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "This category contains medicines, please delete medicines first.";
                return RedirectToAction("Index");
            }
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

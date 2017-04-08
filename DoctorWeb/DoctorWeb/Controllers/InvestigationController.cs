﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;

namespace DoctorWeb.Controllers
{
    public class InvestigationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Investigation
        public ActionResult Index()
        {
            return View(db.Investigations.ToList());
        }

        // GET: Investigation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investigation investigation = db.Investigations.Find(id);
            if (investigation == null)
            {
                return HttpNotFound();
            }
            return View(investigation);
        }

        // GET: Investigation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Investigation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Investigation investigation)
        {
            if (ModelState.IsValid)
            {
                db.Investigations.Add(investigation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(investigation);
        }

        // GET: Investigation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investigation investigation = db.Investigations.Find(id);
            if (investigation == null)
            {
                return HttpNotFound();
            }
            return View(investigation);
        }

        // POST: Investigation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Investigation investigation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investigation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(investigation);
        }

        // GET: Investigation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investigation investigation = db.Investigations.Find(id);
            if (investigation == null)
            {
                return HttpNotFound();
            }
            return View(investigation);
        }

        // POST: Investigation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Investigation investigation = db.Investigations.Find(id);
            db.Investigations.Remove(investigation);
            db.SaveChanges();
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

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
using System.Web.Configuration;

namespace DoctorWeb.Controllers
{
    [Authorize]
    public class PaymentTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentType
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

            var paymenttypes = from s in db.PaymentTypes
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                paymenttypes = paymenttypes.Where(s => s.PaymentTypeName.Contains(searchString));
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(paymenttypes.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: PaymentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = db.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // GET: PaymentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PaymentTypeName,Rupees")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.PaymentTypes.Add(paymentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentType);
        }

        // GET: PaymentType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = db.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PaymentTypeName,Rupees")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: PaymentType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = db.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PaymentType paymentType = db.PaymentTypes.Find(id);
                db.PaymentTypes.Remove(paymentType);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Charge is applied to one or more existing Prescription, so it can not be deleted.";
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

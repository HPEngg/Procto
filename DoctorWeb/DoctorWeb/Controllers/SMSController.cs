﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using DoctorWeb.Models.Tools;

namespace DoctorWeb.Controllers
{
    public class SMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMS
        public ActionResult Index()
        {
            return View(db.ShortMessages.ToList());
        }

        // GET: SMS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // GET: SMS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MobileNumber,Message,Status,Date,FromDate,ToData,Patients")] SMS sMS)
        {
            string targetMobileNumbers = string.Empty;
            if(sMS.Patients == Models.Enums.SMSToPatients.All)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
                
            }
            else if(sMS.Patients == Models.Enums.SMSToPatients.VisitingToday)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where pr.FollowDate == DateTime.Today.Date
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.VisitingTomorow)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where pr.FollowDate == DateTime.Today.Date.AddDays(1)
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.SelectVisitDates)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where sMS.FromDate < pr.FollowDate && pr.FollowDate <= sMS.ToData
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.EnterManually)
            {
                targetMobileNumbers = sMS.MobileNumber;
            }

            if (ModelState.IsValid)
            {
                var result = SMSHelper.sendMessage(targetMobileNumbers, sMS.Message);
                sMS.Status = result;
                sMS.Date = DateTime.Now.Date;
                db.ShortMessages.Add(sMS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sMS);
        }

        // GET: SMS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MobileNumber,Message,Status,Date,FromDate,ToData,Patients")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sMS);
        }

        // GET: SMS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMS sMS = db.ShortMessages.Find(id);
            db.ShortMessages.Remove(sMS);
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

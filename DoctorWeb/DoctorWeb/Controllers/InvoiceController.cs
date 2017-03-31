using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using System.Data.Entity.Core.Objects;

namespace DoctorWeb.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoice Daily
        public ActionResult Daily()
        {
            var prescriptions = db.Prescriptions.Where(p => DbFunctions.TruncateTime(p.Date) == DateTime.Today.Date).Include(p => p.Doctor).Include(p => p.Instructions).Include(p => p.Patient).Include(p => p.PatientType);
            return View(prescriptions.ToList());
        }

        // GET: Invoice Weekly
        public ActionResult Weekly()
        {
            var dateBefore7days = DateTime.Today.AddDays(-7).Date;
            var prescriptions = db.Prescriptions.Where(p => DbFunctions.TruncateTime(p.Date) >= dateBefore7days).Include(p => p.Doctor).Include(p => p.Instructions).Include(p => p.Patient).Include(p => p.PatientType);
            return View(prescriptions.ToList());
        }


        // GET: Invoice Monthly
        public ActionResult Monthly(int month, int year)
        {
            //var dateBefore30Days = DateTime.Today.AddDays(-30).Date;
            var prescriptions = db.Prescriptions.Include(p => p.Doctor).Where(p => p.Date.Year == year && p.Date.Month == month).Include(p => p.Instructions).Include(p => p.Patient).Include(p => p.PatientType);
            return View(prescriptions.ToList());
        }


        //// GET: Invoice
        //public ActionResult Monthly()
        //{
        //    var dateBefore30Days = DateTime.Today.AddDays(-30).Date;
        //    var prescriptions = db.Prescriptions.Include(p => p.Doctor).Where(p => DbFunctions.TruncateTime(p.Date) >= dateBefore30Days).Include(p => p.Instruction).Include(p => p.Patient).Include(p => p.PatientType);
        //    return View(prescriptions.ToList());
        //}

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Invoice/Create
        //public ActionResult Create()
        //{
        //    ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
        //    ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name");
        //    ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");
        //    ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName");
        //    return View();
        //}

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Days,Diagnosis,Procedure,Date,FollowDate,M,Percent,Less,Rs,Received,Pending,DoctorID,PatientID,InstructionID,PatientTypeID")] Prescription prescription)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Prescriptions.Add(prescription);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", prescription.DoctorID);
        //    ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name", prescription.InstructionID);
        //    ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", prescription.PatientID);
        //    ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName", prescription.PatientTypeID);
        //    return View(prescription);
        //}

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", prescription.DoctorID);
            //ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name", prescription.InstructionID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", prescription.PatientID);
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName", prescription.PatientTypeID);
            return View(prescription);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Days,Diagnosis,Procedure,Date,FollowDate,M,Percent,Less,Rs,Received,Pending,DoctorID,PatientID,InstructionID,PatientTypeID")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Daily");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", prescription.DoctorID);
            //ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name", prescription.InstructionID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", prescription.PatientID);
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName", prescription.PatientTypeID);
            return View(prescription);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescription);
            db.SaveChanges();
            return RedirectToAction("/Daily");
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

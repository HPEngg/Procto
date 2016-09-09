using System;
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
    public class PrescriptionMasterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrescriptionMaster
        public ActionResult Index()
        {
            return View(db.Prescriptions.ToList());
        }

        // GET: PrescriptionMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMaster prescriptionMaster = db.Prescriptions.Find(id);
            if (prescriptionMaster == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionMaster);
        }

        // GET: PrescriptionMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrescriptionMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Days,Diagnosis,Procedure,N,O,P,D,KS,M,Percent,Less,Rs")] PrescriptionMaster prescriptionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescriptionMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prescriptionMaster);
        }

        // GET: PrescriptionMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMaster prescriptionMaster = db.Prescriptions.Find(id);
            if (prescriptionMaster == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionMaster);
        }

        // POST: PrescriptionMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Days,Diagnosis,Procedure,N,O,P,D,KS,M,Percent,Less,Rs")] PrescriptionMaster prescriptionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescriptionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescriptionMaster);
        }

        // GET: PrescriptionMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMaster prescriptionMaster = db.Prescriptions.Find(id);
            if (prescriptionMaster == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionMaster);
        }

        // POST: PrescriptionMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrescriptionMaster prescriptionMaster = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescriptionMaster);
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

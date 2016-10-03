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
    public class PrescriptionMedicineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrescriptionMedicine
        public ActionResult Index()
        {
            var prescriptionMedicines = db.PrescriptionMedicines.Include(p => p.Prescription);
            return View(prescriptionMedicines.ToList());
        }

        // GET: PrescriptionMedicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMedicine prescriptionMedicine = db.PrescriptionMedicines.Find(id);
            if (prescriptionMedicine == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionMedicine);
        }

        // GET: PrescriptionMedicine/Create
        public ActionResult Create()
        {
            ViewBag.PrescriptionID = new SelectList(db.Prescriptions, "ID", "Diagnosis");
            return View();
        }

        // POST: PrescriptionMedicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OINT,OINTMore,Morning,Noon,Night,DozTiming,Quantity,PrescriptionID")] PrescriptionMedicine prescriptionMedicine)
        {
            if (ModelState.IsValid)
            {
                db.PrescriptionMedicines.Add(prescriptionMedicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrescriptionID = new SelectList(db.Prescriptions, "ID", "Diagnosis", prescriptionMedicine.PrescriptionID);
            return View(prescriptionMedicine);
        }

        // GET: PrescriptionMedicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMedicine prescriptionMedicine = db.PrescriptionMedicines.Find(id);
            if (prescriptionMedicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrescriptionID = new SelectList(db.Prescriptions, "ID", "Diagnosis", prescriptionMedicine.PrescriptionID);
            return View(prescriptionMedicine);
        }

        // POST: PrescriptionMedicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OINT,OINTMore,Morning,Noon,Night,DozTiming,Quantity,PrescriptionID")] PrescriptionMedicine prescriptionMedicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescriptionMedicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrescriptionID = new SelectList(db.Prescriptions, "ID", "Diagnosis", prescriptionMedicine.PrescriptionID);
            return View(prescriptionMedicine);
        }

        // GET: PrescriptionMedicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionMedicine prescriptionMedicine = db.PrescriptionMedicines.Find(id);
            if (prescriptionMedicine == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionMedicine);
        }

        // POST: PrescriptionMedicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrescriptionMedicine prescriptionMedicine = db.PrescriptionMedicines.Find(id);
            db.PrescriptionMedicines.Remove(prescriptionMedicine);
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

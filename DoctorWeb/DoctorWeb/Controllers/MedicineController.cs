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
    public class MedicineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medicine
        public ActionResult Index()
        {
            var medicines = db.Medicines.Include(m => m.Dosage).Include(m => m.Morning).Include(m => m.Night).Include(m => m.Noon).Include(m => m.OINT).Include(m => m.PrescriptionCategory);
            return View(medicines.ToList());
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicine/Create
        public ActionResult Create()
        {
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name");
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes, "ID", "Name");
            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories, "ID", "Name");
            return View();
        }

        // POST: Medicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OINTTypeID,OINTMore,MorningDozID,NoonDozID,NightDozID,DosageID,IsDayAffected,Quantity,PrescriptionCategoryID")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes, "ID", "Name", medicine.OINTTypeID);
            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories, "ID", "Name", medicine.PrescriptionCategoryID);
            return View(medicine);
        }

        // GET: Medicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes, "ID", "Name", medicine.OINTTypeID);
            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories, "ID", "Name", medicine.PrescriptionCategoryID);
            return View(medicine);
        }

        // POST: Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OINTTypeID,OINTMore,MorningDozID,NoonDozID,NightDozID,DosageID,IsDayAffected,Quantity,PrescriptionCategoryID")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes, "ID", "Name", medicine.OINTTypeID);
            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories, "ID", "Name", medicine.PrescriptionCategoryID);
            return View(medicine);
        }

        // GET: Medicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
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

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
    [Authorize]
    public class PatientHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PatientHistory
        public ActionResult Index()
        {
            var patientHistories = db.PatientHistories.Include(p => p.Patient);
            return View(patientHistories.ToList());
        }

        // GET: PatientHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            if (patientHistory == null)
            {
                return HttpNotFound();
            }
            return View(patientHistory);
        }

        // GET: PatientHistory/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");
            return View();
        }

        // POST: PatientHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RP,KCO,CO,ComplainForm,Constipation,ConstipationMore,Pain,PainMore,Burning,BurningMore,Bleeding,BleedingMore,Itching,ItchingMore,PusDrainage,PusDrainageMore,Swelling,SwellingMore,SCO,ACO,Allergy,History,Weight,Height,T,PR,BP,SPO2,PRR,Proctoscopy,LightOnOff,Other,PatientID")] PatientHistory patientHistory)
        {
            if (ModelState.IsValid)
            {
                db.PatientHistories.Add(patientHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", patientHistory.PatientID);
            return View(patientHistory);
        }

        // GET: PatientHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            if (patientHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", patientHistory.PatientID);
            return View(patientHistory);
        }

        // POST: PatientHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RP,KCO,CO,ComplainForm,Constipation,ConstipationMore,Pain,PainMore,Burning,BurningMore,Bleeding,BleedingMore,Itching,ItchingMore,PusDrainage,PusDrainageMore,Swelling,SwellingMore,SCO,ACO,Allergy,History,Weight,Height,T,PR,BP,SPO2,PRR,Proctoscopy,LightOnOff,Other,PatientID")] PatientHistory patientHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", patientHistory.PatientID);
            return View(patientHistory);
        }

        // GET: PatientHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            if (patientHistory == null)
            {
                return HttpNotFound();
            }
            return View(patientHistory);
        }

        // POST: PatientHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            db.PatientHistories.Remove(patientHistory);
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

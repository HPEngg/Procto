using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using DoctorWeb.Models.CustomModels;

namespace DoctorWeb.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Patient
        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.Doctor);
            return View(patients.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Status,Name,Age,Gender,Address,ReferredBy,DepartmentID,DOB,Contact,Email,Occupation,Habit,FoodPreference,RemindMeAbout,DoctorID")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", patient.DoctorID);
            return View(patient);
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", patient.DoctorID);
            return View(patient);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,Name,Age,Gender,Address,ReferredBy,DepartmentID,DOB,Contact,Email,Occupation,Habit,FoodPreference,RemindMeAbout,DoctorID")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", patient.DoctorID);
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Patient
        public ActionResult Refered(int? id)
        {
            IEnumerable<PatientRefByDoctor> patients = null;
            ViewBag.Values = new SelectList(db.Doctors, "ID", "Name");
            if (id == null)
                patients = db.Patients.Where(p => p.ReferredBy == Models.Enums.ReferredBy.Doctor).Select(o =>  new PatientRefByDoctor() { ID = o.ID, Name = o.Name, Age = o.Age.ToString(), Address = o.Address, Sex = o.Gender.ToString(), Status = o.Status.ToString(), Department = o.DepartmentID.ToString(), Ammount = db.Prescriptions.Where(p => p.PatientID == o.ID).Sum(s => s.Rs) });
            else
                patients = db.Patients.Where(p => p.ReferredBy == Models.Enums.ReferredBy.Doctor && p.DoctorID == id).Select(o => new PatientRefByDoctor() { ID = o.ID, Name = o.Name, Age = o.Age.ToString(), Address = o.Address, Sex = o.Gender.ToString(), Status = o.Status.ToString(), Department = o.DepartmentID.ToString(), Ammount = db.Prescriptions.Where(p => p.PatientID == o.ID).Sum(s => s.Rs) });

            return View(patients.ToList());
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

using DoctorWeb.Models;
using DoctorWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DoctorWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Search(String PatientName)
        {
            var model = db.Patients.Where(p => p.Name.Contains(PatientName)).Select(p => new PatientSearch() { DepartmentName = p.DepartmentID.ToString(), Name = p.Name, No = p.ID, RefferalName = "test" });

            return PartialView(model.ToList());
            //return View(model.ToList());
        }

        public ActionResult Index()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");

            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string MedicineName)
        {
            var model = db.Medicines.Where(m => m.OINTMore.Contains(MedicineName));
            return Json(model.ToList());
        }

        static int patient_success = 0;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PatientHome model)
        {
            var patient = model.Patient;
            //This line is temporary fix
            Patient p = new Patient();
            patient.DoctorID = model.DoctorID;
            if (ModelState.IsValid) 
            {
                p = db.Patients.Add(patient);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", patient.DoctorID);

            var patientHistory = model.PatientHistory;
            patientHistory.PatientID = p.ID;
            if (ModelState.IsValid)
            {
                db.PatientHistories.Add(patientHistory);
                db.SaveChanges();
                //return RedirectToAction("Index");              
            }

            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", patientHistory.PatientID);
            patient_success = 1;
            return RedirectToAction("Edit", new { id = p.ID });
            //return RedirectToAction("Index");
        }

        static int prescr_success = 0;
        public ActionResult Prescription(int patientID)
        {
            //ViewBag.Message = "Your application description page.";
            if (prescr_success == 1) {
                ViewBag.Message = "Patient Prescription Created Successfully";
                prescr_success = 0;
            }
            var model = new PrescriptionHome();
            model.Categories = db.PrescriptionCategories;
            model.PaymentTypes = db.PaymentTypes;

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name");
            ViewBag.PatientID = patientID;
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName");

            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name");
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes, "ID", "Name");
            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories, "ID", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prescription(PrescriptionHome model)
        {
            var prescription = new Prescription()
            {
                Date = DateTime.Now,
                Diagnosis = model.Diagnosis,
                Procedure = model.Procedure,
                Days = model.Days,
                DoctorID = model.DoctorID,
                InstructionID = model.InstructionID,
                PatientID = model.PatientID,
                PatientTypeID = model.PatientTypeID,
                FollowDate = (DateTime) model.FollowDate,
                Less = model.Less,
                Pending = model.Pending,
                Percent = model.Percent,
                Received = model.Received,
                Rs = model.Rs,
                M = model.M,
            };
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
              //  ViewBag.Message = "Patient Prescription Created Successfully";
                prescr_success = 1;
                return RedirectToAction("Prescription", new { patientID = model.PatientID });
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", prescription.DoctorID);
            ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name", prescription.InstructionID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", prescription.PatientID);
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes, "ID", "PatientTypeName", prescription.PatientTypeID);

            return RedirectToAction("Prescription", new { patientID = model.PatientID });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(int id)
        {
            if (patient_success == 1)
            {
                ViewBag.Message = "Patient Added Successfully";
                patient_success = 0;
            }
            var model = new PatientHome();
            model.Patient = db.Patients.Find(id);
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");
            return View(model);
        }
    }
}
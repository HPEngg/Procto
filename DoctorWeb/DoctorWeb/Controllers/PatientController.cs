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
using System.IO;
using System.ComponentModel;
using System.Web.Script.Serialization;
using PagedList;
using DoctorWeb.Extension;
using System.Web.Configuration;

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
        public ActionResult Refered(int? id, string currentFilter, string searchString, int? page, DateTime? fromDate, DateTime? toDate, string search, string export)
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
            ViewBag.Page = page;
            ViewBag.DoctorID = id;

            IEnumerable<PatientRefByDoctor> patients = null;

            int? docId = db.ReferredBy.Where(w => w.ID == 1).Select(s => s.ID).FirstOrDefault();

            if (id == null && docId != null)
                patients = db.Patients.Where(p => p.ReferredByID == docId).Select(o => new PatientRefByDoctor() { CreatedDate = o.CreatedDate, ID = o.ID, Name = o.Name, Age = o.Age.ToString(), Address = o.Address, Sex = o.Gender.ToString(), Status = o.Status.ToString(), Department = o.Department.Name, Ammount = db.Prescriptions.Where(p => p.PatientID == o.ID).Sum(s => (decimal?)s.Rs) ?? 0 });
            else if (id != null && docId != null)
                patients = db.Patients.Where(p => p.DoctorID == id && p.ReferredByID == docId).Select(o => new PatientRefByDoctor() { CreatedDate = o.CreatedDate, ID = o.ID, Name = o.Name, Age = o.Age.ToString(), Address = o.Address, Sex = o.Gender.ToString(), Status = o.Status.ToString(), Department = o.Department.Name, Ammount = db.Prescriptions.Where(p => p.PatientID == o.ID).Sum(s => (decimal?)s.Rs) ?? 0 });

            if (fromDate != null && toDate != null)
            {
                patients = patients.Where(w => fromDate <= w.CreatedDate && w.CreatedDate <= toDate);
                ViewBag.FromDate = fromDate.Value.Date.ToString("yyyy-MM-dd");
                ViewBag.ToDate = toDate.Value.Date.ToString("yyyy-MM-dd");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(s => s.Name.CaseInsensitiveContains(searchString));
            }

            ViewBag.Values = new SelectList(db.Doctors, "ID", "Name");

            if(!string.IsNullOrEmpty(export))
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Patients.xls");
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                WriteTsv(patients, Response.Output);
                Response.End();
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(patients.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        //[HttpPost]
        //public ActionResult UploadPatientImage(string jsonData)
        //{
        //    bool result = true;
        //    PatientPhoto personData;
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    personData = jss.Deserialize<PatientPhoto>(jsonData);
        //    Patient patient = db.Patients.Find(personData.Id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        patient.Photo = personData.Photo;
        //    }
        //    db.Entry(patient).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Content(result.ToString());
        //}

        [HttpPost]
        public ActionResult UploadPatientImage(PatientPhoto personData1)
        {
            bool result = true;
            PatientPhoto personData = personData1;
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //personData = jss.Deserialize<PatientPhoto>(jsonData);
            Patient patient = db.Patients.Find(personData.Id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            else
            {
                patient.Photo = personData.Photo;
            }
            db.Entry(patient).State = EntityState.Modified;
            db.SaveChanges();
            return Content(result.ToString());
        }
        [HttpPost]
        public ActionResult UploadPrescriptionImage(PatientPrescriptionPhoto personData1)
        {
            bool result = true;
            PatientPrescriptionPhoto personData = personData1;
            PrescriptionMobileImage objData = new Models.PrescriptionMobileImage();
            Patient patient = db.Patients.Find(personData.Id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            else
            {
                objData.Patient = patient;
                objData.PatientID = personData.Id;
                objData.UploadedImage1 = personData.Photo;
                objData.UploadedImage2 = personData.Photo2;
                objData.DateCreated = DateTime.Now;
            }

            db.Entry(objData).State = EntityState.Added;
            db.SaveChanges();
            return Content(result.ToString());
        }
    }

    public class PatientPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
    }
    public class PatientPrescriptionPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Photo2 { get; set; }
    }
}

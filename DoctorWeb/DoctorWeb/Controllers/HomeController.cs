using DoctorWeb.Models;
using DoctorWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Web.Services;

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

        public ActionResult DrawImage()
        {
            return PartialView();
            //return View(model.ToList());
        }

        [HttpPost]
        public ActionResult DrawImage(string data)
        {
            byte[] imgarr = Convert.FromBase64String(data);
            Image image;
            using (MemoryStream ms = new MemoryStream(imgarr))
            {
                image = Image.FromStream(ms);
            }
            image.Save("~/Content/Images/PatientImage.png");
            ViewBag.Logo = Server.MapPath("~") + @"Content\Images\PatientImage.png";
            return PartialView();
            //return View(model.ToList());
        }


        public ActionResult Webcam(int id)
        {
            ViewBag.PatientID = id;
            return PartialView();
            //return View(model.ToList());
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
        public ActionResult CaptureImage(int id)
        {
            var stream = Request.InputStream;
            string dump;

            if (ModelState.IsValid)
            {
                var patient = db.Patients.Find(id);
                using (var reader = new StreamReader(stream))
                {
                    dump = reader.ReadToEnd();
                    patient.Photo = String_To_Bytes2(dump);

                    ////DateTime nm = DateTime.Now;

                    ////string date = nm.ToString("yyyymmddMMss");

                    //var path = Server.MapPath("~/Content/PatientImages/test.png");

                    //System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

                    ////ViewData["path"] = date + "test.jpg";

                    ////Session["val"] = date + "test.jpg";
                }
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Prescription", new { id = id });
                return View();
            }


            
            //string url = Session["val"].ToString();
            //Session["val"] = null;
            //return PartialView("Webcam");
            return View();
        }


        public JsonResult Rebind()
        {
            string path = "http://localhost:50409/Content/PatientImages/test.png";
            return Json(path, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");
            ViewBag.PatientCount = db.Patients.Count() + 1;
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string MedicineName)
        {
            var model = db.Medicines.Where(m => m.OINTMore.Contains(MedicineName)).Select(m => new { m.OINTMore, m.IsDayAffected }).ToList();
            //var model = db.Medicines.Where(m => m.OINTMore.Contains(MedicineName)).Select(m => m.OINTMore).ToList();
            return Json(model);
        }

        static int patient_success = 0;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PatientHome model)
        {
            var patient = model.Patient;
            //This line is temporary fix
            Patient p = new Patient();
            patient.DoctorID = model.DoctorID == 0 ? null : model.DoctorID;
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
            if (prescr_success == 1)
            {
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

            model.PrescriptionImages = db.PreImages.Select(o => new SelectListItem() { Text = o.Label, Value = o.ID.ToString(), Selected = false });

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
                FollowDate = model.FollowDate,
                Less = model.Less,
                Pending = model.Pending,
                Percent = model.Percent,
                Received = model.Received,
                Rs = model.Rs,
                M = model.M,
                PrescriptionImage = Convert.FromBase64String(model.PatientImage.Remove(0, 22)),
                Investigation = model.Investigation
            };
            if (ModelState.IsValid)
            {
                if (model.SelectedPrescriptionImages != null)
                    prescription.PreImages = db.PreImages.Where(m => model.SelectedPrescriptionImages.Contains(m.ID)).ToList();

                var prescroptionObj = db.Prescriptions.Add(prescription);
                db.SaveChanges();

                for (int i = 0; i < model.OINTTypeID.Length; i++)
                {
                    var prescriptionMedicine = new PrescriptionMedicine()
                    {
                        PrescriptionID = prescroptionObj.ID,
                        OINTMore = model.Medicine_OINTMore[i],
                        //MorningDozID = model.MorningDozID[i],
                        Morning = db.Dozes.Find(model.MorningDozID[i]),
                        //NoonDozID = model.NoonDozID[i],
                        Noon = db.Dozes.Find(model.NoonDozID[i]),
                        //NightDozID = model.NightDozID[i],
                        Night = db.Dozes.Find(model.NightDozID[i]),
                        DosageID = model.DosageID[i],
                        Quantity = model.Medicine_Quantity[i],
                        OINTTypeID = model.OINTTypeID[i]
                    };

                    db.PrescriptionMedicines.Add(prescriptionMedicine);
                }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientHome model)
        {
            model.PatientHistory.PatientID = model.Patient.ID;
            model.Patient.DoctorID = model.DoctorID;
            if (ModelState.IsValid)
            {
                db.Entry(model.Patient).State = EntityState.Modified;
                db.PatientHistories.Add(model.PatientHistory);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.Patient.ID });
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", model.Patient.DoctorID);
            return View(model);
        }

        public ActionResult Print(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient != null)
            {
                var patientHistory = db.PatientHistories.Where(p => p.PatientID == id).OrderByDescending(q => q.ID).FirstOrDefault();
                var prescription = db.Prescriptions.Where(p => p.PatientID == id).OrderByDescending(q => q.ID).FirstOrDefault();

                var model = new PrintModel();
                model.PatientID = patient.ID;
                model.PatientName = patient.Name;
                model.Age = patient.Age;
                model.Gender = patient.Gender.ToString();
                model.Address = patient.Address;
                model.Occuption = patient.Occupation;
                model.Habbits = patient.Habit.ToString();
                model.RefBy = patient.ReferredBy.ToString();
                model.DateToday = DateTime.Today.Date.ToShortDateString();
                if(patient.Doctor != null)
                {
                    model.RefDoctorName = patient.Doctor.Name;
                }

                if (patientHistory != null)
                {
                    model.KCO = patientHistory.KCO;
                    model.CO = patientHistory.CO;
                    model.ComplainForm = patientHistory.ComplainForm;
                    model.Constipation = patientHistory.Constipation;
                    model.ConstipationMore = patientHistory.ConstipationMore;
                    model.Pain = patientHistory.Pain;
                    model.PainMore = patientHistory.PainMore;
                    model.Burning = patientHistory.Burning;
                    model.BurningMore = patientHistory.BurningMore;
                    model.Bleeding = patientHistory.Bleeding;
                    model.BleedingMore = patientHistory.BleedingMore;
                    model.Itching = patientHistory.Itching;
                    model.ItchingMore = patientHistory.ItchingMore;
                    model.PusDrainage = patientHistory.PusDrainage;
                    model.PusDrainageMore = patientHistory.PusDrainageMore;
                    model.Swelling = patientHistory.Swelling;
                    model.SwellingMore = patientHistory.SwellingMore;
                    model.SCO = patientHistory.SCO;
                    model.ACO = patientHistory.ACO;
                    model.Allergy = patientHistory.Allergy;
                    model.History = patientHistory.History;
                    model.Weight = patientHistory.Weight;
                    model.Height = patientHistory.Height;
                    model.T = patientHistory.T;
                    model.PR = patientHistory.PR;
                    model.BP = patientHistory.BP;
                    model.SPO2 = patientHistory.SPO2;
                    model.PRR = patientHistory.PRR;
                    model.Proctoscopy = patientHistory.Proctoscopy;
                    model.Other = patientHistory.Other;
                }

                if(prescription != null)
                {
                    model.PrescriptionImage = prescription.PrescriptionImage;
                    model.Diagnosis = prescription.Diagnosis;
                    model.FollowDate = prescription.FollowDate == null ? string.Empty : prescription.FollowDate.Value.ToShortDateString();
                    model.Instruction = prescription.Instruction.Description;
                    model.Rs = prescription.Rs.ToString();
                    model.Less = prescription.Less;
                    model.Total = Convert.ToString( prescription.Rs - Convert.ToDecimal(prescription.Less));
                    model.Medicines = db.PrescriptionMedicines.Where(p => p.PrescriptionID == prescription.ID).ToList();
                }

                return View(model);
            }
            return View();
        }

        public ActionResult MedicineList(string id)
        {
            var model = db.Medicines.Where(p => p.PrescriptionCategories.Any(q => q.Name == id)).ToList();
            foreach(var med in model)
            {

            }
            ViewBag.DosageIDL = new SelectList(db.Dosages, "ID", "Name");
            ViewBag.MorningDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeIDL = new SelectList(db.OINTTypes, "ID", "Name");
            ViewBag.PrescriptionCategoryIDL = new SelectList(db.PrescriptionCategories, "ID", "Name");
            return PartialView(model);
        }
    }
}
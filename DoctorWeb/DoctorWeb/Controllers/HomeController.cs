﻿using DoctorWeb.Models;
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
using System.Web.Configuration;
using DoctorWeb.Models.Tools;
using System.Text;

namespace DoctorWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Search(String PatientName)
        {
            int invoiceNum = 0;
            List<PatientSearch> model;
            if (int.TryParse(PatientName, out invoiceNum))
            {
                var patientID = db.Prescriptions.Where(w => w.ID == invoiceNum).Select(s => s.PatientID).FirstOrDefault();
                model = db.Patients.Where(p => p.ID == patientID || p.ID == invoiceNum).Select(p => new PatientSearch()
                {
                    Status = p.Status.ToString(),
                    Name = p.Name,
                    MobileNo = p.Contact,
                    Address = p.Address,
                    DepartmentName = p.Department.Name.ToString(),
                    Reference = p.ReferredBy.Name,
                    ID = p.ID,
                    RefferalName = p.DoctorID != null ? db.Doctors.Where(w => w.ID == p.DoctorID).Select(s => s.Name).FirstOrDefault() : "Other"//"test"
                }).ToList();

                foreach (var patient in model)
                {
                    var latestPrescription = db.Prescriptions.Where(p => p.PatientID == patient.ID).OrderByDescending(d => d.Date).FirstOrDefault();
                    if (latestPrescription != null)
                    {
                        patient.InvoiceNo = latestPrescription.ID;
                        patient.Date = latestPrescription.Date;
                        patient.Diagnosis = latestPrescription.Diagnosis;
                        patient.Procedure = latestPrescription.Procedure;
                        //patient.PatientType = latestPrescription.PatientType.PatientTypeName;
                        patient.PatientType = latestPrescription.PatientType == null ? string.Empty : latestPrescription.PatientType.PatientTypeName;
                        patient.NewPatientOrFollowUp = "FollowUP";
                        //patient.ChargesType = latestPrescription.Charges.FirstOrDefault();
                    }
                    else
                    {
                        patient.NewPatientOrFollowUp = "New patient";
                    }
                }
                return PartialView(model.ToList());
            }
            else
            {

                PatientName = PatientName.Replace('_', ' ');
                model = db.Patients.Where(p => p.Name.Contains(PatientName) || p.Address.Contains(PatientName) || p.Department.Name.Contains(PatientName) || p.Status.ToString().Contains(PatientName) || p.Contact.Contains(PatientName)).Select(p => new PatientSearch()
                {
                    Status = p.Status.ToString(),
                    Name = p.Name,
                    MobileNo = p.Contact,
                    Address = p.Address,
                    DepartmentName = p.Department.Name.ToString(),
                    Reference = p.ReferredBy.Name,
                    ID = p.ID,
                    RefferalName = p.DoctorID != null ? db.Doctors.Where(w => w.ID == p.DoctorID).Select(s => s.Name).FirstOrDefault() : "Other"//"test"
                }).ToList();

                foreach (var patient in model)
                {
                    var latestPrescription = db.Prescriptions.Where(p => p.PatientID == patient.ID).OrderByDescending(d => d.Date).FirstOrDefault();
                    if (latestPrescription != null)
                    {
                        patient.InvoiceNo = latestPrescription.ID;
                        patient.Date = latestPrescription.Date;
                        patient.Diagnosis = latestPrescription.Diagnosis;
                        patient.Procedure = latestPrescription.Procedure;
                        patient.PatientType = latestPrescription.PatientType == null ? string.Empty : latestPrescription.PatientType.PatientTypeName;
                        patient.NewPatientOrFollowUp = "FollowUP";
                        //patient.ChargesType = latestPrescription.Charges.FirstOrDefault();
                    }
                    else
                    {
                        patient.NewPatientOrFollowUp = "New patient";
                    }
                }
                return PartialView(model.ToList());
            }
        }
        [Authorize]
        public ActionResult FollowupList_Intermediate()
        {
            return View();
        }
        [Authorize]
        public ActionResult FollowupList(DateTime fdate)
        {
            List<PatientSearch> model;
            var patientIDs = db.Prescriptions.Where(w => DbFunctions.TruncateTime(w.FollowDate) == fdate.Date).Select(s => s.PatientID).ToList();
            model = db.Patients.Where(p => patientIDs.Contains(p.ID)).Select(p => new PatientSearch()
            {
                Status = p.Status.ToString(),
                Name = p.Name,
                MobileNo = p.Contact,
                Address = p.Address,
                DepartmentName = p.DepartmentID.ToString(),
                Reference = p.ReferredBy.Name,
                ID = p.ID,
                RefferalName = p.DoctorID != null ? db.Doctors.Where(w => w.ID == p.DoctorID).Select(s => s.Name).FirstOrDefault() : "Other"//"test"
            }).ToList();

            foreach (var patient in model)
            {
                var latestPrescription = db.Prescriptions.Where(p => p.PatientID == patient.ID).OrderByDescending(d => d.Date).FirstOrDefault();
                if (latestPrescription != null)
                {
                    patient.InvoiceNo = latestPrescription.ID;
                    patient.Date = latestPrescription.Date;
                    patient.Diagnosis = latestPrescription.Diagnosis;
                    patient.Procedure = latestPrescription.Procedure;
                    patient.PatientType = latestPrescription.PatientType == null ? string.Empty : latestPrescription.PatientType.PatientTypeName;
                    patient.NewPatientOrFollowUp = "FollowUP";
                    //patient.ChargesType = latestPrescription.Charges.FirstOrDefault();
                }
                else
                {
                    patient.NewPatientOrFollowUp = "New patient";
                }
            }
            return PartialView(model.ToList());
        }
        [Authorize]
        public ActionResult PatientToday()
        {
            var todayDate = DateTime.Now.Date;
            var query = from p in db.Patients.Where(w => DbFunctions.TruncateTime(w.LastUpdatedDate) == todayDate)
                       where !db.Prescriptions.Where(w => DbFunctions.TruncateTime(w.Date) == todayDate).Any(a => p.ID == a.PatientID)
                       select p;
            var model = new List<PatientToday>();

            foreach (var p in query.ToList())
            {
                var pt = new PatientToday()
                {
                    Status = p.Status.ToString(),
                    Name = p.Name,
                    Age = p.Age,
                    MobileNo = p.Contact,
                    Address = p.Address,
                    DOB = p.DOB.Value == null ? string.Empty : p.DOB.Value.ToShortDateString(),
                    DepartmentName = p.Department.Name.ToString(),
                    Reference = p.ReferredBy.Name,
                    ID = p.ID,
                    RefferalName = p.DoctorID != null ? db.Doctors.Where(w => w.ID == p.DoctorID).Select(s => s.Name).FirstOrDefault() : "Other"//"test"
                };
                var latestPatientHistory = db.PatientHistories.Where(q => q.PatientID == q.ID).FirstOrDefault();
                if (latestPatientHistory != null)
                {
                    pt.RP = latestPatientHistory.RP;
                    pt.KCO = latestPatientHistory.KCO;
                    pt.ComplainForm = latestPatientHistory.ComplainForm;
                    pt.Constipation = latestPatientHistory.Constipation;
                    pt.ConstipationMore = latestPatientHistory.ConstipationMore;
                }
                model.Add(pt);
            }
            return View(model.ToList());
        }
        [Authorize]
        public ActionResult ImageLightbox(int id)
        {
            ViewBag.PatientID = id;
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ImageLightbox(string data, int patientID, int imageID)
        {
            Patient p = db.Patients.Find(patientID);
            if(p != null)
            {
                byte[] imgarr = Convert.FromBase64String(data.Remove(0, 22));
                Image image;
                using (MemoryStream ms = new MemoryStream(imgarr))
                {
                    image = Image.FromStream(ms);
                    string imageFileName = "PatientDrawnImage" + imageID + ".png";
                    string path = Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patientID + "/"  + imageFileName));
                    System.IO.FileInfo file = new System.IO.FileInfo(path);
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    image.Save(path);
                }
            }
            return PartialView();
        }

        //public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        //{
        //    var ratioX = (double)maxWidth / image.Width;
        //    var ratioY = (double)maxHeight / image.Height;
        //    var ratio = Math.Min(ratioX, ratioY);

        //    var newWidth = (int)(image.Width * ratio);
        //    var newHeight = (int)(image.Height * ratio);

        //    var newImage = new Bitmap(newWidth, newHeight);

        //    using (var graphics = Graphics.FromImage(newImage))
        //        graphics.DrawImage(image, 0, 0, newWidth, newHeight);

        //    return newImage;
        //}
        [Authorize]
        public ActionResult DrawImage()
        {
            return PartialView();
            //return View(model.ToList());
        }

        [Authorize]
        [HttpPost]
        public ActionResult DrawImage(string data)
        {
            byte[] imgarr = Convert.FromBase64String(data.Remove(0, 22));
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

        [Authorize]
        public ActionResult Webcam(int id)
        {
            ViewBag.PatientID = id;
            return PartialView();
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
        [Authorize]
        // Captures patient.photo from webcam
        public ActionResult CaptureImage(int id)
        {
            var stream = Request.InputStream;
            string dump;

            if (ModelState.IsValid)
            {
                using (var reader = new StreamReader(stream))
                {
                    var path = Server.MapPath(Url.Content("~/Content/Images/PatImages/" + id + "/PatientImage.jpg"));

                    System.IO.FileInfo file = new System.IO.FileInfo(path);
                    file.Directory.Create(); // If the directory already exists, this method does nothing.

                    dump = reader.ReadToEnd();
                    System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
                }

                //using (var fileStream = new FileStream(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + id + "/PatientImage.jpg")), FileMode.Create, FileAccess.Write))
                //{
                //    Request.InputStream.Seek(0, SeekOrigin.Begin);
                //    Request.InputStream.CopyTo(fileStream);
                //}

                //var patient = db.Patients.Find(id);
                //using (var reader = new StreamReader(stream))
                //{
                //    dump = reader.ReadToEnd();
                //    patient.Photo = String_To_Bytes2(dump);


                //}
                //db.Entry(patient).State = EntityState.Modified;
                //db.SaveChanges();
                return View();
            }

            return View();
        }


        public JsonResult Rebind()
        {
            string path = "http://localhost:50409/Content/PatientImages/test.png";
            return Json(path, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"] ?? string.Empty;
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name");

            var refbyDefault = db.ReferredBy.OrderBy(o => o.ID).Skip(1).Take(1).FirstOrDefault();
            if(refbyDefault != null)
                ViewBag.ReferredByID = new SelectList(db.ReferredBy, "ID", "Name", refbyDefault.ID);
            else
                ViewBag.ReferredByID = new SelectList(db.ReferredBy, "ID", "Name");

            ViewBag.DepartmentID = new SelectList(db.Departments.OrderBy(i => i.SortOrder), "ID", "Name");
            ViewBag.PatientCount = db.Patients.Count() + 1;
            return View();
        }
        [Authorize]
        [HttpPost]
        public JsonResult AutoComplete(string MedicineName)
        {
            var model = db.Medicines.Where(m => m.OINTMore.Contains(MedicineName)).Select(m => new { m.OINTTypeID, m.OINTMore, m.IsDayAffected, m.Morning, m.Noon, m.Night, m.Dosage, m.Quantity, m.Unit }).ToList();
            return Json(model);
        }

        static int patient_success = 0;
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PatientHome model)
        {
            var patient = model.Patient;
            //This line is temporary fix
            Patient p = new Patient();
            patient.CreatedDate = DateTime.Now.Date;
            patient.LastUpdatedDate = patient.CreatedDate;
            patient.DoctorID = model.DoctorID == 0 ? null : model.DoctorID;
            patient.ReferredByID = model.ReferredByID;
            patient.DepartmentID = model.DepartmentID;
            if (ModelState.IsValid)
            {
                p = db.Patients.Add(patient);
                db.SaveChanges();

                if(patient.DoctorID != null)
                {
                    string hDoc_web = WebConfigurationManager.AppSettings["HDoctorName"];
                    string hospitalName = WebConfigurationManager.AppSettings["HospitalName"];
                    var doctor = db.Doctors.Where(w => w.ID == patient.DoctorID).FirstOrDefault();
                    string patientDetails = patient.Name + "," + patient.Age + ", " + patient.Gender + ", " + patient.Address;
                    string message = "Dear "+ doctor.Name +" your referred  patient " + patientDetails + " was examined by "+ hDoc_web + " at " + hospitalName + " on " + DateTime.Now.Date + ". Thanks for your reference.";
                    SMSHelper.sendMessage(doctor.Contact, message);
                }
                //return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", patient.DoctorID);
            ViewBag.ReferredByID = new SelectList(db.ReferredBy, "ID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments.OrderBy(i => i.SortOrder), "ID", "Name");

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
        [Authorize]
        public ActionResult Prescription(int patientID)
        {
            //ViewBag.Message = "Your application description page.";
            if (prescr_success > 0)
            {
                ViewBag.Message = "Patient Prescription Created Successfully: ID=" + prescr_success;
                prescr_success = 0;
            }
            var model = new PrescriptionHome();
            //model.Categories = db.PrescriptionCategories.OrderBy(o => o.Name);
            model.PaymentTypes = db.PaymentTypes.OrderBy(o => o.ID);

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name");
            //ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name");
            ViewBag.PatientID = patientID;

            int? prescriptionID = db.Prescriptions.Where(p => p.PatientID == patientID).OrderByDescending(o => o.Date).Select(s => s.ID).FirstOrDefault();
            ViewBag.PrescriptionID = prescriptionID == null ? 0 : prescriptionID;
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes.OrderBy(o => o.ID), "ID", "PatientTypeName");


            ViewBag.PrescriptionCategories = new SelectList(db.PrescriptionCategories, "ID", "Name");
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name");
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name");
            //ViewBag.InvestigationID = new SelectList(db.Investigations, "ID", "Name");

            ViewBag.PrescriptionCategoryID = new SelectList(db.PrescriptionCategories.OrderBy(o => o.Name), "ID", "Name");

            model.PrescriptionImages = db.PreImages.OrderBy(o => o.Label).Select(o => new SelectListItem() { Text = o.Label, Value = o.ID.ToString(), Selected = false });
            model.Instructions = db.Instructions.Select(p => new SelectListItem() { Text = p.Name, Value = p.ID.ToString(), Selected = false });
            model.Investigations = db.Investigations.OrderBy(o => o.Name).Select(p => new SelectListItem() { Text = p.Name, Value = p.ID.ToString(), Selected = false });

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prescription(PrescriptionHome model, HttpPostedFileBase preImage1, HttpPostedFileBase preImage2)
        {
            var prescription = new Prescription()
            {
                Date = DateTime.Now,
                Diagnosis = model.Diagnosis,
                Procedure = model.Procedure,
                Days = model.Days,
                DoctorID = model.DoctorID,
                //InstructionID = model.InstructionID,
                PatientID = model.PatientID,
                PatientTypeID = model.PatientTypeID,
                FollowDate = model.FollowDate,
                Less = model.Less,
                Pending = model.Pending,
                Percent = model.Percent,
                Received = model.Received,
                Rs = model.Rs,
                M = model.M,
                UploadedImage1 = Encoding.ASCII.GetBytes("UploadedPatientPrescriptionImage1.png"),
                UploadedImage2 = Encoding.ASCII.GetBytes("UploadedPatientPrescriptionImage2.png"),
                PrescriptionImage1 = Encoding.ASCII.GetBytes("PatientDrawnImage1.png"),
                PrescriptionImage2 = Encoding.ASCII.GetBytes("PatientDrawnImage2.png"),
                //PrescriptionImage2 = Convert.FromBase64String(model.PatientImage2.Remove(0, 22)),
                //Investigation = model.Investigation
                //InvestigationID = model.InvestigationID,
                Other = model.Other
            };

            if (ModelState.IsValid)
            {
                if (model.SelectedPrescriptionImages != null)
                    prescription.PreImages = db.PreImages.OrderBy(o => o.Label).Where(m => model.SelectedPrescriptionImages.Contains(m.ID)).ToList();

                if (model.SelectedInstructionsIDs != null)
                    prescription.Instructions = db.Instructions.Where(m => model.SelectedInstructionsIDs.Contains(m.ID)).ToList();

                if (model.SelectedInvestigationIDs != null)
                    prescription.Investigations = db.Investigations.OrderBy(o => o.Name).Where(m => model.SelectedInvestigationIDs.Contains(m.ID)).ToList();

                var prescroptionObj = db.Prescriptions.Add(prescription);
                // ReaderExecuted method code commented due to below line blocks exicution while adding prescription record
                db.SaveChanges();

                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/" + prescroptionObj.ID + "/UploadedPatientPrescriptionImage1.png")));
                file.Directory.Create(); // If the directory already exists, this method does nothing.

                if (preImage1 != null && preImage1.ContentLength > 0)
                {
                    //using (var reader = new System.IO.BinaryReader(preImage1.InputStream))
                    //{
                    //    prescription.UploadedImage1 = reader.ReadBytes(preImage1.ContentLength);
                    //}
                    preImage1.SaveAs(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/" + prescroptionObj.ID + "/UploadedPatientPrescriptionImage1.png")));
                }

                if (preImage2 != null && preImage2.ContentLength > 0)
                {
                    //using (var reader = new System.IO.BinaryReader(preImage2.InputStream))
                    //{
                    //    prescription.UploadedImage2 = reader.ReadBytes(preImage2.ContentLength);
                    //}
                    preImage2.SaveAs(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/" + prescroptionObj.ID + "/UploadedPatientPrescriptionImage2.png")));
                }

                string imageFile1 = Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/PatientDrawnImage1.png"));
                string imageFile2 = Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/PatientDrawnImage2.png"));

                if (System.IO.File.Exists(imageFile1))
                {
                    //prescription.PrescriptionImage1 = Encoding.ASCII.GetBytes(imageFile1);
                    //System.IO.File.Delete(imageFile1);
                    System.IO.File.Move(imageFile1, Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/" + prescroptionObj.ID + "/UploadedPatientPrescriptionImage1.png")));
                }
                if (System.IO.File.Exists(imageFile2))
                {
                    //prescription.PrescriptionImage2 = Encoding.ASCII.GetBytes(imageFile2);
                    //System.IO.File.Delete(imageFile2);
                    System.IO.File.Move(imageFile2, Server.MapPath(Url.Content("~/Content/Images/PatImages/" + model.PatientID + "/" + prescroptionObj.ID + "/UploadedPatientPrescriptionImage2.png")));
                }

                if (model.pm_type != null)
                {
                    // Adding charges
                    for (int pt = 0; pt < model.pm_type.Length; pt++)
                    {
                        var preCharge = new Charge() { PaymentTypeID = model.pm_type[pt], PrescriptionID = prescription.ID };
                        db.Charges.Add(preCharge);
                        db.SaveChanges();
                    }
                }

                if (model.OINTTypeID != null)
                {
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
                            Unit = model.Medicine_Unit[i],
                            Total = model.Medicine_Total[i],
                            OINTTypeID = model.OINTTypeID[i]
                        };
                        db.PrescriptionMedicines.Add(prescriptionMedicine);
                        db.SaveChanges();
                    }
                }

                ViewBag.Message = "Patient Prescription Created Successfully";
                prescr_success = prescroptionObj.ID;
                return RedirectToAction("Prescription", new { patientID = model.PatientID });
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", prescription.DoctorID);
            //ViewBag.InstructionID = new SelectList(db.Instructions, "ID", "Name", prescription.InstructionID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", prescription.PatientID);
            ViewBag.PatientTypeID = new SelectList(db.PatientTypes.OrderBy(o => o.ID), "ID", "PatientTypeName", prescription.PatientTypeID);
            ViewBag.InvestigationID = new SelectList(db.Investigations.OrderBy(o => o.Name), "ID", "Name");

            return RedirectToAction("Prescription", new { patientID = model.PatientID });
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (patient_success == 1)
            {
                ViewBag.Message = "Patient Added Successfully";
                patient_success = 0;
            }
            var model = new PatientHome();
            model.Patient = db.Patients.Find(id);
            model.PatientHistory = db.PatientHistories.Where(w => w.PatientID == id).OrderByDescending(o => o.ID).FirstOrDefault();
            ViewBag.DoctorID = new SelectList(db.Doctors, "ID", "Name", model.Patient.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "ID", "Name", model.Patient.ID);
            ViewBag.ReferredByID = new SelectList(db.ReferredBy, "ID", "Name", model.Patient.ReferredByID);
            ViewBag.DepartmentID = new SelectList(db.Departments.OrderBy(i => i.SortOrder), "ID", "Name", model.Patient.DepartmentID);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientHome model)
        {
            model.PatientHistory.PatientID = model.Patient.ID;
            model.Patient.DoctorID = model.DoctorID;
            model.Patient.ReferredByID = model.ReferredByID;
            model.Patient.DepartmentID = model.DepartmentID;
            model.Patient.LastUpdatedDate = DateTime.Now.Date;
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
        [Authorize]
        public ActionResult PrintPreview(int id)
        {
            int prescriptionID = db.Prescriptions.Where(p => p.PatientID == id).OrderByDescending(o => o.Date).Select(s => s.ID).FirstOrDefault();
            var model = GetPatientPriscription(prescriptionID);
            model.FooterRequired = true;
            model.HeaderRequired = true;
            model.InvoiceRequired = true;
            model.PatientRequired = true;
            model.UploadedImagesRequired = true;
            model.RXRequired = true;
            ViewBag.PatientID = id;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult PrintInvoice(PrintInvoice model)
        {
            var printData = new PrintInvoice();
            printData.IsHeaderPhotoRequired = model.IsHeaderPhotoRequired;
            var prescription = db.Prescriptions.Where(p => p.ID == model.PrescriptionID).FirstOrDefault();
            if (prescription != null)
            {
                printData.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();
                Patient patient = db.Patients.Find(prescription.PatientID);
                if (patient != null)
                {
                    printData.Name = patient.Name;
                    printData.Age = patient.Age;
                    printData.Gender = patient.Gender.ToString();
                    printData.TodayDate = DateTime.Now.Date.ToShortDateString();
                    printData.Address = patient.Address;
                }
                printData.InvoiceNo = prescription.ID;
                printData.Medicines = db.PrescriptionMedicines.Where(p => p.PrescriptionID == prescription.ID).ToList();
                var paymentTypeIDs = db.Charges.Where(w => w.PrescriptionID == prescription.ID).Select(s => s.PaymentTypeID).ToList();
                printData.PaymentTypes = db.PaymentTypes.OrderBy(o => o.ID).Where(w => paymentTypeIDs.Contains(w.ID)).ToList();
                ViewBag.PatientID = patient.ID;
                printData.Total = prescription.Rs;
                printData.Other = prescription.Other;
                printData.Medicine = prescription.M;
                printData.Less = prescription.Less;
                //string tot = prescription.Rs.ToString("N0");
                //printData.Total = tot;
                printData.Total = prescription.Rs;
            }

            return View(printData);

        }
        [Authorize]
        public ActionResult PrintInvoicePreview(int id)
        {
            var model = new PrintInvoice();
            var prescription = db.Prescriptions.Where(p => p.ID == id).FirstOrDefault();
            if (prescription != null)
            {
                model.PrescriptionID = prescription.ID;
                model.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();
                Patient patient = db.Patients.Find(prescription.PatientID);
                if (patient != null)
                {
                    model.Name = patient.Name;
                    model.Age = patient.Age;
                    model.Gender = patient.Gender.ToString();
                    model.TodayDate = DateTime.Now.Date.ToShortDateString();
                    model.Address = patient.Address;
                }
                model.InvoiceNo = prescription.ID;
                model.Medicines = db.PrescriptionMedicines.Where(p => p.PrescriptionID == prescription.ID).ToList();
                var paymentTypeIDs = db.Charges.Where(w => w.PrescriptionID == prescription.ID).Select(s => s.PaymentTypeID).ToList();
                model.PaymentTypes = db.PaymentTypes.OrderBy(o => o.ID).Where(w => paymentTypeIDs.Contains(w.ID)).ToList();
                model.Total = prescription.Rs;
                model.Other = prescription.Other;
                model.Medicine = prescription.M;
                model.Less = prescription.Less;
            }
            model.IsHeaderPhotoRequired = true;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Print(PrintModel model)
        {
            int prescriptionID = db.Prescriptions.Where(p => p.PatientID == model.PatientID).OrderByDescending(o => o.Date).Select(s => s.ID).FirstOrDefault();
            var printData = GetPatientPriscription(prescriptionID);
            printData.HeaderRequired = model.HeaderRequired;
            printData.PatientRequired = model.PatientRequired;
            printData.RXRequired = model.RXRequired;
            printData.FooterRequired = model.FooterRequired;
            printData.InvoiceRequired = model.InvoiceRequired;
            printData.UploadedImagesRequired = model.UploadedImagesRequired;
            return View(printData);
        }
        [Authorize]
        public ActionResult FollowUp(int patientID, int id)
        {
            var model = GetPatientPriscription(id);
            ViewBag.PatientID = patientID;
            ViewBag.Values = new SelectList(db.Prescriptions.Where(p => p.PatientID == patientID), "ID", "Date");
            return View(model);
        }
        [Authorize]
        public ActionResult MedicineList(string id)
        {
            var model = db.Medicines.Where(p => p.PrescriptionCategories.Any(q => q.Name == id)).ToList();
            foreach(var med in model)
            {

            }
            ViewBag.DosageIDL = new SelectList(db.Dosages.OrderBy(o => o.Name), "ID", "Name");
            ViewBag.MorningDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozIDL = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeIDL = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name");
            ViewBag.PrescriptionCategoryIDL = new SelectList(db.PrescriptionCategories.OrderBy(o => o.Name), "ID", "Name");
            return PartialView(model);
        }
        [Authorize]
        private PrintModel GetPatientPriscription(int prescriptionID)
        {
            var model = new PrintModel();

            var prescription = db.Prescriptions.Where(p => p.ID == prescriptionID).FirstOrDefault();
            if (prescription != null)
            {
                model.Patient.Diagnosis = prescription.Diagnosis;
                //model.Patient.Advice =
                model.Patient.Procedure = prescription.Procedure;
                model.Patient.Type = prescription.PatientType == null ? string.Empty : prescription.PatientType.PatientTypeName.ToString();
                model.Patient.Investigations = db.Investigations.OrderBy(o => o.Name).Where(p => p.Prescriptions.Any(q => q.ID == prescription.ID)).ToList();
                //model.Patient.DrawenImage1 = prescription.PrescriptionImage1;
                //model.Patient.DrawenImage2 = prescription.PrescriptionImage2;
                //model.Patient.UploadedImage1 = prescription.UploadedImage1;
                //model.Patient.UploadedImage2 = prescription.UploadedImage2;
                model.Patient.PrescriptionImages = db.PreImages.OrderBy(o => o.Label).Where(p => p.Prescriptions.Any(q => q.ID == prescription.ID)).Select(t => t.Image).ToList();

                model.RX.Medicines = db.PrescriptionMedicines.Where(p => p.PrescriptionID == prescription.ID).ToList();

                //model.Compulsory.FollowDate = prescription.FollowDate == null ? string.Empty : prescription.FollowDate.Value.ToShortDateString();
                model.Compulsory.FollowDate = prescription.FollowDate == null ? string.Empty : prescription.FollowDate.Value.ToString();
                if(model.Compulsory.FollowDate != string.Empty)
                { 
                    model.Compulsory.Day = Convert.ToDateTime(model.Compulsory.FollowDate).DayOfWeek.ToString();
                }
                model.Compulsory.Instructions = db.Instructions.Where(p => p.Prescriptions.Any(q => q.ID == prescription.ID)).ToList();
            }


            model.Header.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();

            int patientID = prescription.PatientID;
            Patient patient = db.Patients.Find(patientID);
            if (patient != null)
            {
                model.Patient.ID = patient.ID;
                //model.Patient.Photo = patient.Photo;
                model.PatientPhotoPath = System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/PatientImage.jpg"))) ? Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/PatientImage.jpg")) : string.Empty;
                model.PatientUploadedImage1 = System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/UploadedPatientPrescriptionImage1.png"))) ? Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/UploadedPatientPrescriptionImage1.png")) : string.Empty;
                model.PatientUploadedImage2 = System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/UploadedPatientPrescriptionImage2.png"))) ? Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/UploadedPatientPrescriptionImage2.png")) : string.Empty;
                model.PatientDrawnImage1 = System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/PatientDrawnImage1.png"))) ? Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/PatientDrawnImage1.png")) : string.Empty;
                model.PatientDrawnImage1 = System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/PatientDrawnImage1.png"))) ? Server.MapPath(Url.Content("~/Content/Images/PatImages/" + patient.ID + "/" + prescriptionID + "/PatientDrawnImage2.png")) : string.Empty;
                model.Patient.Name = patient.Name;
                model.Patient.Age = patient.Age;
                model.Patient.Gender = patient.Gender.ToString();
                model.Patient.TodayDate = DateTime.Now.Date.ToShortDateString();
                model.Patient.No = patient.ID.ToString();
                model.Patient.Department = patient.Department.Name.ToString();
                model.Patient.Contact = patient.Contact;
                model.Patient.Email = patient.Email;
                model.Patient.Address = patient.Address;
                model.Patient.Habbit = patient.Habit.ToString();
                model.Patient.Diet = patient.FoodPreference.ToString();

                var patientHistory = db.PatientHistories.Where(p => p.PatientID == patientID).OrderByDescending(q => q.ID).FirstOrDefault();
                if (patientHistory != null)
                {
                    model.Patient.Weight = patientHistory.Weight.ToString();
                    model.Patient.KCO = patientHistory.KCO;
                    model.Patient.ComplainOf = patientHistory.CO;
                    model.Patient.Since = patientHistory.ComplainForm;
                    model.Patient.Constipation = patientHistory.Constipation;
                    model.Patient.ConstipationMore = patientHistory.ConstipationMore;
                    model.Patient.GAS = patientHistory.Gas;
                    model.Patient.GASMore = patientHistory.GasMore;
                    model.Patient.Acidity = patientHistory.Acidity;
                    model.Patient.AcidityMore = patientHistory.AcidityMore;
                    model.Patient.Pain = patientHistory.Pain;
                    model.Patient.PainMore = patientHistory.PainMore;
                    model.Patient.Burning = patientHistory.Burning;
                    model.Patient.BurningMore = patientHistory.BurningMore;
                    model.Patient.Bleeding = patientHistory.Bleeding;
                    model.Patient.BleedingMore = patientHistory.BleedingMore.ToString();
                    model.Patient.Swelling = patientHistory.Swelling;
                    model.Patient.SwellingMore = patientHistory.SwellingMore;
                    model.Patient.SCO = patientHistory.SCO;
                    model.Patient.ACO = patientHistory.ACO;
                    model.Patient.Allergy = patientHistory.Allergy;
                    model.Patient.History = patientHistory.History;
                    model.Patient.Height = patientHistory.Height.ToString();
                    model.Patient.Temprature = patientHistory.T.ToString();
                    model.Patient.Pulse = patientHistory.PR;
                    model.Patient.BP = patientHistory.BP;
                    model.Patient.SPO2 = patientHistory.SPO2;
                    model.Patient.PR = patientHistory.PRR;
                    model.Patient.Proctoscopy = patientHistory.Proctoscopy;
                    model.Patient.Others = patientHistory.Other;
                }

                var paymentTypeIDs = db.Charges.Where(w => w.PrescriptionID == prescription.ID).Select(s => s.PaymentTypeID).ToList();
                model.Invoice.PaymentTypes = db.PaymentTypes.OrderBy(o => o.ID).Where(w => paymentTypeIDs.Contains(w.ID)).ToList();

                model.Invoice.Medicine = prescription.M;
                model.Invoice.OtherFromTextbox = prescription.Other;
                model.Invoice.Less = prescription.Less;
                model.Invoice.Total = Convert.ToString(prescription.Rs);
                model.Invoice.CashRecived = prescription.Received.ToString();
                model.Invoice.PendingAmount = prescription.Pending.ToString();

                model.Footer.FooterPhoto = db.Pictures.Select(p => p.Footer).FirstOrDefault();
            }

            return model;
        }
    }
}
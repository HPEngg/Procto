using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using DoctorWeb.Models.Tools;
using System.Web.Configuration;
using System.Text.RegularExpressions;

namespace DoctorWeb.Controllers
{
   
    public class SMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMS
        
        public ActionResult Index()
        {
            return View(db.ShortMessages.OrderByDescending(o => o.Date).ToList());
        }

        // GET: SMS/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }
        
        // GET: SMS/Create
        public ActionResult Create()
        {
            var model = new SMS();
            model.FromDate = DateTime.Now.Date;
            model.ToData = DateTime.Now.Date;
            return View(model);
        }

        // POST: SMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
         
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MobileNumber,Message,Status,Date,FromDate,ToData,FromHolidayDate,ToHolidayDate,FromHolidayDate2,ToHolidayDate2,Patients,SMSTypes")] SMS sMS)
        {
            string targetMobileNumbers = string.Empty;
            string message = string.Empty;
            if (sMS.Patients == Models.Enums.SMSToPatients.All)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
                
            }
            else if(sMS.Patients == Models.Enums.SMSToPatients.VisitingToday)
            {
                var todayDate = DateTime.Today.Date;
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where DbFunctions.TruncateTime(pr.FollowDate) == DbFunctions.TruncateTime(todayDate)
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.VisitingTomorow)
            {
                var tomorowDate = DateTime.Today.Date.AddDays(1);
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where DbFunctions.TruncateTime(pr.FollowDate) == DbFunctions.TruncateTime(tomorowDate)
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.SelectVisitDates)
            {
                var query = from pt in db.Patients
                            join pr in db.Prescriptions on pt.ID equals pr.PatientID
                            where DbFunctions.TruncateTime(sMS.FromDate) <= DbFunctions.TruncateTime(pr.FollowDate) && DbFunctions.TruncateTime(pr.FollowDate) <= DbFunctions.TruncateTime(sMS.ToData)
                            select pt.Contact;
                targetMobileNumbers = string.Join(",", query);
            }
            else if (sMS.Patients == Models.Enums.SMSToPatients.EnterManually)
            {
                targetMobileNumbers = sMS.MobileNumber;
            }

            if(sMS.SMSTypes == Models.Enums.SMSTypes.Personal)
            {
                message = sMS.Message;
            }
            else if(sMS.SMSTypes == Models.Enums.SMSTypes.Holiday1)
            {
                message = sMS.Message.Replace("FROM", "From " + sMS.FromHolidayDate.Value.ToShortDateString()).Replace("TO", " to " + sMS.ToHolidayDate.Value.ToShortDateString());
                message = Regex.Replace(message, @"(?:(?:\r?\n)+ +){2,}", @" ");
            }
            else if (sMS.SMSTypes == Models.Enums.SMSTypes.Holiday2)
            {
                message = sMS.Message.Replace("FROM", "From " + sMS.FromHolidayDate2.Value.ToShortDateString()).Replace("TO", " to " + sMS.ToHolidayDate2.Value.ToShortDateString());
                message = Regex.Replace(message, @"(?:(?:\r?\n)+ +){2,}", @" ");
            }

            if (ModelState.IsValid)
            {
                var result = SMSHelper.sendMessage(targetMobileNumbers, message);
                sMS.Status = result;
                sMS.Date = DateTime.Now;
                db.ShortMessages.Add(sMS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sMS);
        }

         
        [AllowAnonymous]
        public ActionResult FollowUpMessage()
        {
            var tomorowDate = Extension.CultureDate.ConvertUTCBasedOnCuture(DateTime.UtcNow.Date).Date.AddDays(1);
            var patients = from pt in db.Patients
                        join pr in db.Prescriptions on pt.ID equals pr.PatientID
                        where DbFunctions.TruncateTime(pr.FollowDate) == DbFunctions.TruncateTime(tomorowDate)
                        select pt;

            foreach(Patient p in patients)
            {
                if (!string.IsNullOrEmpty(p.Contact))
                {
                    string hDoc_web = WebConfigurationManager.AppSettings["HDoctorName"];
                    string hospitalName = WebConfigurationManager.AppSettings["HospitalName"];
                    string mobileNumber = WebConfigurationManager.AppSettings["HDocMobile"];
                    var visitDate = db.Prescriptions.Where(w => w.PatientID == p.ID).Max(m => m.Date);
                    string FollowupString = "";
                    if (CheckDBNull.ToStr(WebConfigurationManager.AppSettings["MessageLanguag"]) == "Hindi")
                    {
                        if (p.Gender == Models.Enums.Gender.Male)
                        {
                            FollowupString = "श्रीमान ";
                        }
                        else
                        {
                            FollowupString = "श्रीमंती ";
                        }
                        string webConfigStrig = CheckDBNull.ToStr(WebConfigurationManager.AppSettings["FollowupMessageText"]);
                        webConfigStrig = webConfigStrig.Replace("@FollowupDate", visitDate.Date.ToShortDateString());
                        string messageGuj = FollowupString + p.Name + ", " + webConfigStrig  + ". Call " + mobileNumber + " for any query.";
                        SMSHelper.sendMessage(p.Contact, messageGuj);
                    }
                    else if (CheckDBNull.ToStr(WebConfigurationManager.AppSettings["MessageLanguag"]) == "Gujarati")
                    {
                        string messageGuj = "Dear " + p.Name + ", Your Appointment has been confirmed with " + hDoc_web + "  at " + hospitalName + "  on " + visitDate.Date.ToShortDateString() + " at " + visitDate.Date.ToShortTimeString() + ". Call " + mobileNumber + " for any query.";
                        SMSHelper.sendMessage(p.Contact, messageGuj);
                    }
                    else
                    {
                        string message = "Dear " + p.Name + ", Your Appointment has been confirmed with " + hDoc_web + "  at " + hospitalName + "  on " + visitDate.Date.ToShortDateString() + " at " + visitDate.Date.ToShortTimeString() + ". Call " + mobileNumber + " for any query.";
                        SMSHelper.sendMessage(p.Contact, message);
                    }
                }
            }

            TempData["Message"] = "SMS Sent to " + patients.Count() + " Patient(s)";
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult DOB()
        {

            var tomorowDate = Extension.CultureDate.ConvertUTCBasedOnCuture(DateTime.UtcNow.Date).Date;
            var patients = from pt in db.Patients
                           where DbFunctions.TruncateTime(pt.DOB) == DbFunctions.TruncateTime(tomorowDate)
                           select pt;

            foreach (Patient p in patients)
            {
                if (!string.IsNullOrEmpty(p.Contact))
                {
                    string hDoc_web = WebConfigurationManager.AppSettings["DRName"];
                    string hospitalName = WebConfigurationManager.AppSettings["HName"];
                    string City = WebConfigurationManager.AppSettings["City"];
                    
                    string birthdayMessage = CheckDBNull.ToStr(WebConfigurationManager.AppSettings["BirthdayMessage"])  + hDoc_web  + hospitalName + City;

                    SMSHelper.sendMessage(p.Contact, birthdayMessage);
                   
                }
            }

            TempData["Message"] = "SMS Sent to " + patients.Count() + " Patient(s)";
            return RedirectToAction("Index", "Home");
        }


        // GET: SMS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MobileNumber,Message,Status,Date,FromDate,ToData,Patients")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sMS);
        }

        
        // GET: SMS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.ShortMessages.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Delete/5
        [HttpPost, ActionName("Delete")]
        
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMS sMS = db.ShortMessages.Find(id);
            db.ShortMessages.Remove(sMS);
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

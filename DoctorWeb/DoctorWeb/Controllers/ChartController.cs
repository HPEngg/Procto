using DoctorWeb.Models;
using DoctorWeb.Models.Chart;
using DoctorWeb.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWeb.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewOldPatient(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();
            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            //var newPatients = from pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
            //                  join pr in db.Prescriptions on pt.ID equals pr.PatientID into ptpr
            //                 group ptpr by pt.ID into grouped
            //                 where grouped.Count() <= 1
            //                 select grouped.Key;

            var newPatients = from p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                              let cCount =
                             (
                               from pr in db.Prescriptions
                               where p.ID == pr.PatientID
                               select pr
                             ).Count()
                              where cCount <= 1
                              select p;

            //var oldPatients = from pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
            //                  join pr in db.Prescriptions on pt.ID equals pr.PatientID into ptpr
            //                  group ptpr by pt.ID into grouped
            //                  where grouped.Count() > 1
            //                  select grouped.Key;

            var oldPatients = from p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                              let cCount =
                              (
                                from pr in db.Prescriptions
                                where p.ID == pr.PatientID
                                select pr
                              ).Count()
                              where cCount > 1
                              select p;

            dataPoints.Add(new StringDataPoint() { label = "New", y = newPatients.Count() });
            dataPoints.Add(new StringDataPoint() { label = "Old", y = oldPatients.Count() });


            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult StatusWisePatient(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();
            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }


            var data1 = from pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                        group pt by pt.Status into g
                        select new StringDataPoint() { label = g.Key.ToString(), y = g.Count() };

            dataPoints = data1.ToList();

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
            //return View();
        }

        public ActionResult DepartmentWisePatient(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            IQueryable<StringDataPoint> dataPoints = null;
            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if(query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            //dataPoints = from d in db.Departments
            //             join p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate) on d.ID equals p.DepartmentID into dp
            //             group dp by d.Name into grouped
            //             select new StringDataPoint() { label = grouped.Key, y = grouped.Count() };

            dataPoints = from d in db.Departments
                         let pCount =
                         (
                           from p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                           where d.ID == p.DepartmentID
                           select p
                         ).Count()
                         select new StringDataPoint() { label = d.Name, y = pCount };

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult PatientTypeWisePatient(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();

            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            var data1 = from pr in db.Prescriptions
                        join pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate) on pr.PatientID equals pt.ID into all
                        group all by pr.PatientType into g
                        select new StringDataPoint() { label = g.Key.PatientTypeName.ToString(), y = g.Count() };

            dataPoints = data1.ToList();

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult ReferenceWisePatient(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            IQueryable<StringDataPoint> dataPoints = null;
            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            var patients = db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate).ToList();

            //dataPoints = from r in db.ReferredBy
            //             join p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate) on r.ID equals p.ReferredByID into rp
            //             group rp by r.Name into grouped
            //             select new StringDataPoint() { label = grouped.Key, y = grouped.Count() };

            dataPoints = from r in db.ReferredBy
                         let pCount =
                         (
                           from p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                           where r.ID == p.ReferredByID
                           select p
                         ).Count()
                         select new StringDataPoint() { label = r.Name, y = pCount };

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult Income(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();

            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            var medicineIncome = db.Prescriptions.Where(w => FromDate <= w.Date && w.Date <= ToDate).Sum(s => s.M);
            dataPoints.Add(new StringDataPoint() { label = "Medicine", y = Convert.ToDouble(medicineIncome) });

            var otherIncome = db.Prescriptions.Where(w => FromDate <= w.Date && w.Date <= ToDate).Sum(s => s.Other);
            dataPoints.Add(new StringDataPoint() { label = "Other", y = Convert.ToDouble(otherIncome) });

            var data1 = from pt in db.PaymentTypes
                        join ch in db.Charges on pt.ID equals ch.PaymentTypeID //into all
                        join pr in db.Prescriptions.Where(w => FromDate <= w.Date && w.Date <= ToDate) on ch.PrescriptionID equals pr.ID
                        group pt by pt.PaymentTypeName into g
                        select new StringDataPoint() { label = g.Key.ToString(), y = g.Sum(s => (double?)s.Rupees) ?? 0 };

            dataPoints.AddRange(data1.ToList());

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult CategoryWiseExpanse(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();

            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            var data1 = from ex in db.ExpanseCategories
                        join ec in db.Expanses.Where(w => FromDate <= w.Date && w.Date <= ToDate) on ex.ID equals ec.ExpanseCategoryID //into all
                        group ec by ex.Name into g
                        select new StringDataPoint() { label = g.Key.ToString(), y = g.Sum(s => (double?)s.Amount) ?? 0 };

            dataPoints = data1.ToList();

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }

        public ActionResult IncomeExpanse(ChartQuery? query, DateTime? FromDate, DateTime? ToDate)
        {
            List<StringDataPoint> dataPoints = new List<StringDataPoint>();

            if (query == ChartQuery.DateRange)
            {
                if (FromDate == null || ToDate == null)
                    return View();
            }
            else if (query == ChartQuery.ThisWeek)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-7);
            }
            else if (query == ChartQuery.ThisMonth)
            {
                ToDate = DateTime.Now.Date;
                FromDate = DateTime.Now.Date.AddDays(-30);
            }
            else if (query == null)
            {
                ToDate = DateTime.MaxValue;
                FromDate = DateTime.MinValue;
            }

            var income = db.Prescriptions.Where(w => FromDate <= w.Date && w.Date <= ToDate).Sum(s => s.Rs);
            dataPoints.Add(new StringDataPoint() { label = "Income", y = Convert.ToDouble(income) });

            var expanse = db.Expanses.Where(w => FromDate <= w.Date && w.Date <= ToDate).Sum(s => s.Amount);
            dataPoints.Add(new StringDataPoint() { label = "Expanse", y = Convert.ToDouble(expanse) });

            dataPoints.Add(new StringDataPoint() { label = "Profit/Loss", y = Convert.ToDouble(income - expanse) });

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            var model = new ChartModel();
            model.FromDate = DateTime.Now.Date;
            model.ToDate = DateTime.Now.Date;
            return View(model);
        }
    }
}
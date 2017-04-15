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

            dataPoints = from d in db.Departments
                         join p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate) on d.ID equals p.DepartmentID into dp
                         group dp by d.Name into grouped
                         select new StringDataPoint() { label = grouped.Key, y = grouped.Count() };

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;
            
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

            var newPatients = from pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                              join pr in db.Prescriptions on pt.ID equals pr.PatientID into ptpr
                             group ptpr by pt.ID into grouped
                             where grouped.Count() <= 1
                             select grouped.Key;

            var oldPatients = from pt in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate)
                              join pr in db.Prescriptions on pt.ID equals pr.PatientID into ptpr
                              group ptpr by pt.ID into grouped
                              where grouped.Count() > 1
                              select grouped.Key;

            dataPoints.Add(new StringDataPoint() { label = "New", y = newPatients.Count() });
            dataPoints.Add(new StringDataPoint() { label = "Old", y = oldPatients.Count() });


            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            return View();
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

            dataPoints = from r in db.ReferredBy
                         join p in db.Patients.Where(w => FromDate <= w.CreatedDate && w.CreatedDate <= ToDate) on r.ID equals p.ReferredByID into rp
                         group rp by r.Name into grouped
                         select new StringDataPoint() { label = grouped.Key, y = grouped.Count() };

            string output = "[";
            dataPoints.ToList().ForEach((data) => output = output + "{label:\'" + data.label + "\'," + "y:" + data.y + "},");
            output = output + "]";
            ViewBag.DataPoints = output;

            return View();
        }
    }
}
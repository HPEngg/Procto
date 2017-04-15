using DoctorWeb.Models;
using DoctorWeb.Models.Chart;
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

        public ActionResult DepartmentWisePatient()
        {
            var dataPoints = db.Patients.GroupBy(g => g.DepartmentID).Select(s => new DataPoint() { x = s.Key, y = s.Count() });
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints.ToList()).Replace("\"", "");
            return View();
        }
    }
}
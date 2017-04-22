using DoctorWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWeb.Controllers
{
    public class CertificateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Certificate
        public ActionResult Blank()
        {
            ViewBag.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();

            return View();
        }

        public ActionResult Unfit()
        {
            ViewBag.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();

            return View();
        }

        public ActionResult Fit()
        {
            ViewBag.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();
            return View();
        }

        public ActionResult Discharge()
        {
            ViewBag.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();

            return View();
        }

        public ActionResult Bill()
        {
            ViewBag.HeaderPhoto = db.Pictures.Select(p => p.Header).FirstOrDefault();

            return View();
        }
    }
}
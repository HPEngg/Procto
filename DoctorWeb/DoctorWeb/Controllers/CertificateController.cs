using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWeb.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Certificate
        public ActionResult Blank()
        {
            return View();
        }

        public ActionResult Unfit()
        {
            return View();
        }

        public ActionResult Fit()
        {
            return View();
        }

        public ActionResult Discharge()
        {
            return View();
        }

        public ActionResult Bill()
        {
            return View();
        }
    }
}
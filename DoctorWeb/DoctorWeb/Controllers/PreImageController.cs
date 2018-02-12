using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using System.Data.Entity.Infrastructure;
using PagedList;
using System.Web.Configuration;
using System.Text;

namespace DoctorWeb.Controllers
{
    [Authorize]
    public class PreImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PreImage
        public ActionResult Index(string currentFilter, string searchString, int? page)
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

            //var prescriptionimages = from s in db.PreImages
            //               select s;

            var prescriptionimages = from s in db.PreImages
                                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                prescriptionimages = prescriptionimages.Where(s => s.Label.Contains(searchString));
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(prescriptionimages.OrderBy(i => i.Label).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Select()
        {
            return View(db.PreImages.OrderBy(o => o.Label).ToList());
        }

        // GET: PreImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreImage preImage = db.PreImages.Find(id);
            if (preImage == null)
            {
                return HttpNotFound();
            }
            return View(preImage);
        }

        // GET: PreImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Label")] PreImage preImg, HttpPostedFileBase preImage)
        {
            if (ModelState.IsValid)
            {
                if (preImage != null && preImage.ContentLength > 0)
                {
                    //using (var reader = new System.IO.BinaryReader(preImage.InputStream))
                    //{
                    //    preImg.Image = reader.ReadBytes(preImage.ContentLength);
                    //}
                    preImg.Image = Encoding.ASCII.GetBytes(preImage.FileName);
                    db.PreImages.Add(preImg);
                    db.SaveChanges();
                    preImage.SaveAs(Server.MapPath(Url.Content("~/Content/Images/PreImages/" + preImage.FileName)));
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Please select image to upload";
                }
            }

            return View(preImg);
        }

        // GET: PreImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreImage preImage = db.PreImages.Find(id);
            if (preImage == null)
            {
                return HttpNotFound();
            }
            return View(preImage);
        }

        // POST: PreImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image")] PreImage preImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preImage);
        }

        // GET: PreImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreImage preImage = db.PreImages.Find(id);
            if (preImage == null)
            {
                return HttpNotFound();
            }
            return View(preImage);
        }

        // POST: PreImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PreImage preImage = db.PreImages.Find(id);
                string fileName = Server.MapPath(Url.Content("~/Content/Images/PreImages/" + Encoding.ASCII.GetString(preImage.Image))) ;
                db.PreImages.Remove(preImage);
                db.SaveChanges();

                if ((System.IO.File.Exists(fileName)))
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: This Prescription Image is used in one or more existing Prescription, so it can not be deleted.";
                return RedirectToAction("Index");
            }
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

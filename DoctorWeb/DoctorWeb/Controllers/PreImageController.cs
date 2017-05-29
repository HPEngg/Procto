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

namespace DoctorWeb.Controllers
{
    public class PreImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PreImage
        public ActionResult Index()
        {
            return View(db.PreImages.ToList());
        }

        public ActionResult Select()
        {
            return View(db.PreImages.ToList());
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
                    using (var reader = new System.IO.BinaryReader(preImage.InputStream))
                    {
                        preImg.Image = reader.ReadBytes(preImage.ContentLength);
                    }
                    db.PreImages.Add(preImg);
                    db.SaveChanges();
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
                db.PreImages.Remove(preImage);
                db.SaveChanges();
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using System.IO;

namespace DoctorWeb.Controllers
{
    public class PicturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pictures
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Picture picture, HttpPostedFileBase hed, HttpPostedFileBase fot)
        {
            if (ModelState.IsValid)
            {
                Picture current = db.Pictures.FirstOrDefault();

                if(current == null)
                {
                    if (hed != null && hed.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(hed.InputStream))
                        {
                            picture.Header = reader.ReadBytes(hed.ContentLength);
                        }
                    }

                    if (fot != null && fot.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(fot.InputStream))
                        {
                            picture.Footer = reader.ReadBytes(fot.ContentLength);
                        }
                    }

                    db.Pictures.Add(picture);
                }
                else
                {
                    if (hed != null && hed.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(hed.InputStream))
                        {
                            current.Header = reader.ReadBytes(hed.ContentLength);
                        }
                    }

                    if (fot != null && fot.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(fot.InputStream))
                        {
                            current.Footer = reader.ReadBytes(fot.ContentLength);
                        }
                    }

                    db.Entry(current).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(picture);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
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

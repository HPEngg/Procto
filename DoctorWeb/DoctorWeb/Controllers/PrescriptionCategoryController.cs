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

namespace DoctorWeb.Controllers
{
    public class PrescriptionCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrescriptionCategory
        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.Page = page;

            var prescriptioncategories = from s in db.PrescriptionCategories
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                prescriptioncategories = prescriptioncategories.Where(s => s.Name.Contains(searchString));
            }

            ViewBag.First = prescriptioncategories.Min(m => m.SortOrder);
            ViewBag.Last = prescriptioncategories.Max(m => m.SortOrder);

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(prescriptioncategories.OrderBy(i => i.SortOrder).ToPagedList(pageNumber, pageSize));

            //return View(db.PrescriptionCategories.ToList());
        }
        [Authorize]
        public ActionResult Up(int? id, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptioncategory = db.PrescriptionCategories.Find(id);

            var prescriptioncategories = from s in db.PrescriptionCategories
                              select s;

            if (!String.IsNullOrEmpty(currentFilter))
            {
                prescriptioncategories = prescriptioncategories.Where(s => s.Name.Contains(currentFilter));
            }

            var secondLastId = prescriptioncategories.Where(w => w.SortOrder < prescriptioncategory.SortOrder).Max(m => m.SortOrder);
            PrescriptionCategory secondLast = prescriptioncategories.Where(w => w.SortOrder == secondLastId).FirstOrDefault();
            if (secondLast != null)
            {
                long tempId = prescriptioncategory.SortOrder;
                prescriptioncategory.SortOrder = secondLast.SortOrder;
                secondLast.SortOrder = tempId;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = id, page = page, currentFilter = currentFilter, searchString = searchString });
        }
        [Authorize]
        public ActionResult Down(int? id, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptioncategory = db.PrescriptionCategories.Find(id);

            var prescriptioncategories = from s in db.PrescriptionCategories
                              select s;

            if (!String.IsNullOrEmpty(currentFilter))
            {
                prescriptioncategories = prescriptioncategories.Where(s => s.Name.Contains(currentFilter));
            }

            var secondLastId = prescriptioncategories.Where(w => w.SortOrder > prescriptioncategory.SortOrder).Min(m => m.SortOrder);
            PrescriptionCategory secondLast = prescriptioncategories.Where(w => w.SortOrder == secondLastId).FirstOrDefault();
            if (secondLast != null)
            {
                long tempId = prescriptioncategory.SortOrder;
                prescriptioncategory.SortOrder = secondLast.SortOrder;
                secondLast.SortOrder = tempId;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = id, page = page, currentFilter = currentFilter, searchString = searchString });
        }
        [Authorize]
        // GET: PrescriptionCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }
        [Authorize]
        // GET: PrescriptionCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrescriptionCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] PrescriptionCategory prescriptionCategory)
        {
            if (ModelState.IsValid)
            {
                prescriptionCategory.SortOrder = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                db.PrescriptionCategories.Add(prescriptionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prescriptionCategory);
        }
        [Authorize]
        // GET: PrescriptionCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }

        // POST: PrescriptionCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] PrescriptionCategory prescriptionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescriptionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescriptionCategory);
        }

        // GET: PrescriptionCategory/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
            if (prescriptionCategory == null)
            {
                return HttpNotFound();
            }
            return View(prescriptionCategory);
        }

        // POST: PrescriptionCategory/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PrescriptionCategory prescriptionCategory = db.PrescriptionCategories.Find(id);
                db.PrescriptionCategories.Remove(prescriptionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "This category contains medicines, please delete medicines first.";
                return RedirectToAction("Index");
            }
        }
        [Authorize]
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

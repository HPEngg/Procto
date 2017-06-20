using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using PagedList;

namespace DoctorWeb.Controllers
{
    public class ExpanseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expanse
        //public ActionResult Index()
        //{
        //    var expanses = db.Expanses.Include(e => e.ExpanseCategory);
        //    return View(expanses.ToList());
        //}
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

            var expanses = db.Expanses.Include(e => e.ExpanseCategory);

            if (!String.IsNullOrEmpty(searchString))
            {
                expanses = expanses.Where(s => s.ExpanseCategory.Name.Contains(searchString));
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(expanses.OrderBy(i => i.ID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Expanse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expanse expanse = db.Expanses.Find(id);
            if (expanse == null)
            {
                return HttpNotFound();
            }
            return View(expanse);
        }

        // GET: Expanse/Create
        public ActionResult Create()
        {
            ViewBag.ExpanseCategoryID = new SelectList(db.ExpanseCategories, "ID", "Name");
            return View();
        }

        // POST: Expanse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Amount,Description,Date,ExpanseCategoryID")] Expanse expanse)
        {
            if (ModelState.IsValid)
            {
                db.Expanses.Add(expanse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpanseCategoryID = new SelectList(db.ExpanseCategories, "ID", "Name", expanse.ExpanseCategoryID);
            return View(expanse);
        }

        // GET: Expanse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expanse expanse = db.Expanses.Find(id);
            if (expanse == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpanseCategoryID = new SelectList(db.ExpanseCategories, "ID", "Name", expanse.ExpanseCategoryID);
            return View(expanse);
        }

        // POST: Expanse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Amount,Description,Date,ExpanseCategoryID")] Expanse expanse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expanse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpanseCategoryID = new SelectList(db.ExpanseCategories, "ID", "Name", expanse.ExpanseCategoryID);
            return View(expanse);
        }

        // GET: Expanse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expanse expanse = db.Expanses.Find(id);
            if (expanse == null)
            {
                return HttpNotFound();
            }
            return View(expanse);
        }

        // POST: Expanse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expanse expanse = db.Expanses.Find(id);
            db.Expanses.Remove(expanse);
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

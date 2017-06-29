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
    public class DepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Department
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
            ViewBag.Page = page;

            var departments = from s in db.Departments
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(s => s.Name.Contains(searchString));
            }

            ViewBag.First = departments.Min(m => m.SortOrder);
            ViewBag.Last = departments.Max(m => m.SortOrder);

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(departments.OrderBy(i => i.SortOrder).ToPagedList(pageNumber, pageSize));
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Up(int? id, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);

            var departments = from s in db.Departments
                              select s;

            if (!String.IsNullOrEmpty(currentFilter))
            {
                departments = departments.Where(s => s.Name.Contains(currentFilter));
            }

            var secondLastId = departments.Where(w => w.SortOrder < department.SortOrder).Max(m => m.SortOrder);
            Department secondLast = departments.Where(w => w.SortOrder == secondLastId).FirstOrDefault();
            if(secondLast != null)
            {
                long tempId = department.SortOrder;
                department.SortOrder = secondLast.SortOrder;
                secondLast.SortOrder = tempId;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = id, page = page, currentFilter = currentFilter, searchString = searchString });
        }

        public ActionResult Down(int? id, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);

            var departments = from s in db.Departments
                              select s;

            if (!String.IsNullOrEmpty(currentFilter))
            {
                departments = departments.Where(s => s.Name.Contains(currentFilter));
            }

            var secondLastId = departments.Where(w => w.SortOrder > department.SortOrder).Min(m => m.SortOrder);
            Department secondLast = departments.Where(w => w.SortOrder == secondLastId).FirstOrDefault();
            if (secondLast != null)
            {
                long tempId = department.SortOrder;
                department.SortOrder = secondLast.SortOrder;
                secondLast.SortOrder = tempId;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = id, page = page, currentFilter = currentFilter, searchString = searchString });
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.SortOrder = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Department department = db.Departments.Find(id);
                db.Departments.Remove(department);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error: There is one or more Patient exist of this Department, so it can not be deleted.";
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

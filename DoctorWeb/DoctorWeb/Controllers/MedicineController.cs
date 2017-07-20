using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWeb.Models;
using DoctorWeb.Models.CustomModels;
using System.Data.Entity.Infrastructure;
using PagedList;
using System.Web.Configuration;

namespace DoctorWeb.Controllers
{

    public class MedicineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medicine
        //public ActionResult Index()
        //{
        //    if (TempData["ErrorMessage"] != null)
        //        ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

        //    var medicines = db.Medicines.Include(m => m.Dosage).Include(m => m.Morning).Include(m => m.Night).Include(m => m.Noon).Include(m => m.OINT);
        //    return View(medicines.ToList());
        //}
        [Authorize]
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
            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            var medicines = db.Medicines.Include(m => m.Dosage).Include(m => m.Morning).Include(m => m.Night).Include(m => m.Noon).Include(m => m.OINT);

            if (!String.IsNullOrEmpty(searchString))
            {
                medicines = medicines.Where(s => s.OINTMore.Contains(searchString));
            }

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(medicines.OrderBy(i => i.OINTMore).ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult ByCategory(int? id)
        //{
        //    if (TempData["ErrorMessage"] != null)
        //        ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

        //    ViewBag.Values = new SelectList(db.PrescriptionCategories, "ID", "Name");
        //    var medicines = db.Medicines.Where(p => p.PrescriptionCategories.Any(t => t.ID == id)).Include(m => m.Dosage).Include(m => m.Morning).Include(m => m.Night).Include(m => m.Noon).Include(m => m.OINT);
        //    return View(medicines.ToList());
        //}
        [Authorize]
        public ActionResult ByCategory(string currentFilter, string searchString, int? page, int? id)
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
            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            ViewBag.Values = new SelectList(db.PrescriptionCategories.OrderBy(o => o.Name), "ID", "Name");
            var medicines = db.Medicines.Where(p => p.PrescriptionCategories.Any(t => t.ID == id)).Include(m => m.Dosage).Include(m => m.Morning).Include(m => m.Night).Include(m => m.Noon).Include(m => m.OINT);

            if (!String.IsNullOrEmpty(searchString))
            {
                medicines = medicines.Where(s => s.OINTMore.Contains(searchString));
            }
            ViewBag.ID = id;
            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            int pageNumber = (page ?? 1);
            return View(medicines.OrderBy(i => i.OINTMore).ToPagedList(pageNumber, pageSize));
        }

        // GET: Medicine/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicine/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name");
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name");
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name");

            var model = new MedicineViewModel() { };
            var allCategories = db.PrescriptionCategories.OrderBy(o => o.Name).ToList();
            model.PrescriptionsCategories = allCategories.Select(o => new SelectListItem() { Text = o.Name, Value = o.ID.ToString(), Selected = false });
            return View(model);
        }

        // POST: Medicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineViewModel model)//[Bind(Include = "ID,OINTTypeID,OINTMore,MorningDozID,NoonDozID,NightDozID,DosageID,IsDayAffected,Quantity")] Medicine medicine)
        {
            model.Medicine.OINTTypeID = model.OINTTypeID;
            model.Medicine.MorningDozID = model.MorningDozID;
            model.Medicine.NoonDozID = model.NoonDozID;
            model.Medicine.NightDozID = model.NightDozID;
            model.Medicine.DosageID = model.DosageID;
            if (ModelState.IsValid)
            {
                if(model.SelectedPrescriptionCategories != null)
                    model.Medicine.PrescriptionCategories = db.PrescriptionCategories.Where(m => model.SelectedPrescriptionCategories.Contains(m.ID)).ToList();
                db.Entry(model.Medicine).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", model.Medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name", model.Medicine.OINTTypeID);
            return View(model.Medicine);
        }

        // GET: Medicine/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new MedicineViewModel() { Medicine = db.Medicines.Include(i => i.PrescriptionCategories).First(i => i.ID == id) };

            if (model.Medicine == null)
                return HttpNotFound();

            var allCategories = db.PrescriptionCategories.OrderBy(o => o.Name).ToList();

            model.PrescriptionsCategories = allCategories.Select(o => new SelectListItem() { Text = o.Name, Value = o.ID.ToString(), Selected = model.Medicine.PrescriptionCategories.Contains(o) });

            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", model.Medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name", model.Medicine.OINTTypeID);
            return View(model);
        }

        // POST: Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicineViewModel model)//[Bind(Include = "ID,OINTTypeID,OINTMore,MorningDozID,NoonDozID,NightDozID,DosageID,IsDayAffected,Quantity")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                var medicineToUpdate = db.Medicines.Include(i => i.PrescriptionCategories).First(i => i.ID == model.Medicine.ID);

                medicineToUpdate.OINTTypeID = model.OINTTypeID;
                medicineToUpdate.MorningDozID = model.MorningDozID;
                medicineToUpdate.NoonDozID = model.NoonDozID;
                medicineToUpdate.NightDozID = model.NightDozID;
                medicineToUpdate.DosageID = model.DosageID;

                if (TryUpdateModel(medicineToUpdate, "Medicine", new string[] { "ID", "OINTMore", "IsDayAffected", "Unit", "Quantity" }))
                {
                    if(model.SelectedPrescriptionCategories != null)
                    {
                        var updatedCategories = new HashSet<int>(model.SelectedPrescriptionCategories);
                        foreach (PrescriptionCategory cat in db.PrescriptionCategories)
                        {
                            if (!updatedCategories.Contains(cat.ID))
                            {
                                medicineToUpdate.PrescriptionCategories.Remove(cat);
                            }
                            else
                            {
                                medicineToUpdate.PrescriptionCategories.Add(cat);
                            }
                        }
                    }
                    else
                    {
                        foreach (PrescriptionCategory cat in db.PrescriptionCategories)
                        {
                            medicineToUpdate.PrescriptionCategories.Remove(cat);
                        }
                    }
                    
                    db.Entry(medicineToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.DosageID = new SelectList(db.Dosages, "ID", "Name", model.Medicine.DosageID);
            ViewBag.MorningDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.MorningDozID);
            ViewBag.NightDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NightDozID);
            ViewBag.NoonDozID = new SelectList(db.Dozes, "ID", "Name", model.Medicine.NoonDozID);
            ViewBag.OINTTypeID = new SelectList(db.OINTTypes.OrderBy(o => o.Name), "ID", "Name", model.Medicine.OINTTypeID);
            return View(model);
        }

        // GET: Medicine/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicine/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Medicine medicine = db.Medicines.Find(id);
                db.Medicines.Remove(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Remove this medicine from all category before delete";
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

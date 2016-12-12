using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWeb.Models.CustomModels
{
    public class MedicineViewModel
    {
        public Medicine Medicine { get; set; }

        public IEnumerable<SelectListItem> PrescriptionsCategories { get; set; }

        private List<int> selectedPrescriptionCategories;

        public List<int> SelectedPrescriptionCategories
        {
            get
            {
                //if (selectedPrescriptionCategories == null)
                //{
                //    selectedPrescriptionCategories = Medicine.PrescriptionCategories.Select(m => m.ID).ToList();
                //}
                return selectedPrescriptionCategories;
            }
            set { selectedPrescriptionCategories = value; }
        }

        public int OINTTypeID { get; set; }
        public int MorningDozID { get; set; }
        public int NoonDozID { get; set; }
        public int NightDozID { get; set; }
        public int DosageID { get; set; }
    }
}
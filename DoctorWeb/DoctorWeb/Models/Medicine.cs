using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorWeb.Models
{
	public class Medicine
	{
        public int ID { get; set; }

        [Display(Name ="Type")]
        public int OINTTypeID { get; set; }
        public virtual OINTType OINT { get; set; }

        [Display(Name = "Medicine Name")]
        [Required]
        public string OINTMore { get; set; }

        [Display(Name = "Morning")]
        public int MorningDozID { get; set; }

        [ForeignKey("MorningDozID")]
        public virtual Doz Morning { get; set; }

        [Display(Name = "Afternoon")]
        public int NoonDozID { get; set; }

        [ForeignKey("NoonDozID")]
        public virtual Doz Noon { get; set; }

        [Display(Name = "Night")]
        public int NightDozID { get; set; }

        [ForeignKey("NightDozID")]
        public virtual Doz Night { get; set; }

        [Display(Name = "Dosage")]
        public int DosageID { get; set; }
        public virtual Dosage Dosage { get; set; }

        [Display(Name = "Affect to Qty.")]
        public bool IsDayAffected { get; set; }
        [Display(Name = "INR")]
        public float Unit { get; set; }

        public float Quantity { get; set; }

        

        //[Display(Name = "Category")]
        //public int PrescriptionCategoryID { get; set; }
        //public virtual PrescriptionCategory  PrescriptionCategory { get; set; }

        public ICollection<PrescriptionCategory> PrescriptionCategories { get; set; }
    }
}
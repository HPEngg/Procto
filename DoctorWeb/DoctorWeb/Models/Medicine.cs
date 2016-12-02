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

        [Display(Name = "Name")]
        public string OINTMore { get; set; }


        public int MorningDozID { get; set; }

        [ForeignKey("MorningDozID")]
        public virtual Doz Morning { get; set; }

        public int NoonDozID { get; set; }

        [ForeignKey("NoonDozID")]
        public virtual Doz Noon { get; set; }

        public int NightDozID { get; set; }

        [ForeignKey("NightDozID")]
        public virtual Doz Night { get; set; }

        public int DosageID { get; set; }
        public virtual Dosage Dosage { get; set; }

        public bool IsDayAffected { get; set; }

        public float Quantity { get; set; }

        public int PrescriptionCategoryID { get; set; }
        public virtual PrescriptionCategory  PrescriptionCategory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoctorWeb.Models.Enums;

namespace DoctorWeb.Models
{
    public class PrescriptionMedicine
    {
        public int ID { get; set; }
        [Display(Name = "Type")]
        public int OINTTypeID { get; set; }
        public virtual OINTType OINT { get; set; }

        [Display(Name = "Name")]
        public string OINTMore { get; set; }

        public int MorningDozID { get; set; }
        public virtual Doz Morning { get; set; }

        public int NoonDozID { get; set; }
        public virtual Doz Noon { get; set; }

        public int NightDozID { get; set; }
        public virtual Doz Night { get; set; }

        public int DosageID { get; set; }
        public virtual Dosage Dosage { get; set; }

        public float Quantity { get; set; }

        public float Unit { get;  set;}

        public float Total { get; set; }

        public int PrescriptionID { get; set; }
        public virtual Prescription Prescription { get; set; }

    }
}
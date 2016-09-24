using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorWeb.Models.Enums;

namespace DoctorWeb.Models
{
    public class PrescriptionMedicine
    {
        public int ID { get; set; }
        public OINTType OINT { get; set; }
        public string OINTMore { get; set; }
        public Doz Morning { get; set; }
        public Doz Noon { get; set; }
        public Doz Night { get; set; }
        public DozTiming DozTiming { get; set; }
        public float Quantity { get; set; }

        public int PrescriptionID { get; set; }
        public virtual Prescription Prescription { get; set; }

    }
}
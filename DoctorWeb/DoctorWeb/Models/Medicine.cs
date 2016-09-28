using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
	public class Medicine
	{
        public int ID { get; set; }
        public OINTType OINT { get; set; }
        public string OINTMore { get; set; }
        public Doz Morning { get; set; }
        public Doz Noon { get; set; }
        public Doz Night { get; set; }
        public DozTiming DozTiming { get; set; }
        public float Quantity { get; set; }

        public int PrescriptionCategoryID { get; set; }
        public virtual PrescriptionCategory  PrescriptionCategory { get; set; }
    }
}
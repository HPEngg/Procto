using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Charge
    {
        public int ID { get; set; }

        public int PaymentTypeID { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public int PrescriptionID { get; set; }
        public virtual Prescription  Prescription { get; set; }
    }
}
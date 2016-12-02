using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PaymentType
    {
        public int ID { get; set; }

        [Display(Name = "Payment type name")]
        public string PaymentTypeName { get; set; }
        public decimal Rupees { get; set; }

        public IEnumerable<Charge> Charges { get; set; }
    }
}
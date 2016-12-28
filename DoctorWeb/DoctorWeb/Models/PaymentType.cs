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

        [Required]
        [Display(Name = "Charge type name")]
        public string PaymentTypeName { get; set; }

        [Required]
        public decimal Rupees { get; set; }

        public IEnumerable<Charge> Charges { get; set; }
    }
}
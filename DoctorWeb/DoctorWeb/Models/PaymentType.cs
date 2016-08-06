using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PaymentType
    {
        public int ID { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal Rupees { get; set; }
        public bool IsSync { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int InvoceNumber { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public decimal TotalAmmount { get; set; }
        public DateTime FollowDate { get; set; }
    }
}
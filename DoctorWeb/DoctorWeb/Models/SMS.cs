using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class SMS
    {
        public int ID { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [DataType(DataType.MultilineText)]     
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime ToData { get; set; }

        [Display(Name = "Select Numbers")]
        public SMSToPatients Patients { get; set; }
    }
}
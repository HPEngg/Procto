using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PatientType
    {
        public int ID { get; set; }

        [Display(Name = "Patient Type")]
        public string PatientTypeName { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PatientSearch
    {
        public int No { get; set; }

        public string Name { get; set; }

        [Display(Name = "Referral")]
        public string RefferalName { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PatientSearch
    {
        public int No { get; set; }

        public string Name { get; set; }

        public string RefferalName { get; set; }

        public string DepartmentName { get; set; }

    }
}
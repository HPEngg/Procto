using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PatientHome
    {
        public Patient Patient { get; set; }
        public PatientHistory PatientHistory { get; set; }

        public int? DoctorID { get; set; }

        public int DepartmentID { get; set; }
        public int ReferredByID { get; set; }

        public int PatientCount { get; set; }
    }
}
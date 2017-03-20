using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
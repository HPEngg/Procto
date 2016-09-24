using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string DoctorType { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }

    }
}
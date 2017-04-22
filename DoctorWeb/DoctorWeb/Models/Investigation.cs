using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Investigation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
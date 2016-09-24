using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Instruction
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
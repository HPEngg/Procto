using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PrescriptionMaster
    {
        public int ID { get; set; }
        public int Days { get; set; }
        public string Diagnosis { get; set; }
        public string Procedure { get; set; }
        public bool N { get; set; }
        public bool O { get; set; }
        public bool P { get; set; }
        public bool D { get; set; }
        public bool KS { get; set; }
        public string M { get; set; }
        public int Percent { get; set; }
        public string Less { get; set; }
        public string Rs { get; set; }
    }
}
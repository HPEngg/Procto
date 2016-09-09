using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PrescriptionMedicine
    {
        public int ID { get; set; }
        public string OINT { get; set; }
        public int Morning { get; set; }
        public int Noon { get; set; }
        public int Night { get; set; }
        public bool AfterFood { get; set; }
        public float Quantity { get; set; }
        public int PatientTypeID { get; set; }
        public int InstructionID { get; set; }
        public DateTime Date { get; set; }
    }
}
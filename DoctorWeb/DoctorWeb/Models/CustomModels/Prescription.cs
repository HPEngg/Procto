using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class Prescription
    {
        public Prescription PrescriptionMaster { get; set; }
        public List<PrescriptionMedicine> Medicines { get; set; }
    }
}
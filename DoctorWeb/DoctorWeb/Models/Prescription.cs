﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Prescription
    {
        public int ID { get; set; }
        public int Days { get; set; }
        public string Diagnosis { get; set; }
        public string Procedure { get; set; }

        public DateTime Date { get; set; }

        public DateTime FollowDate { get; set; }

        //List of payment type will be added here dynamically
        public IEnumerable<Charge> Charges { get; set; }

        public string M { get; set; }
        public int Percent { get; set; }
        public string Less { get; set; }
        public string Rs { get; set; }
        public Decimal Received { get; set; }
        public Decimal Pending{ get; set; }

        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }

        public int InstructionID { get; set; }
        public virtual Instruction Instruction { get; set; }

        public int PatientTypeID { get; set; }
        public virtual PatientType PatientType { get; set; }

        public IEnumerable<PrescriptionMedicine> Medicines { get; set; }


    }
}
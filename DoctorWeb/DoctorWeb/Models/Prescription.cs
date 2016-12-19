﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Follow Date")]
        public DateTime FollowDate { get; set; }

        //List of payment type will be added here dynamically
        public IEnumerable<Charge> Charges { get; set; }

        public string M { get; set; }
        public int Percent { get; set; }
        public string Less { get; set; }

        [Display(Name = "Invoiced Rs.")]
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

        public ICollection<PreImage> PreImages { get; set; }
    }
}
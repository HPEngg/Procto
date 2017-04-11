using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWeb.Models.CustomModels
{
    public class PrescriptionHome
    {
        public int ID { get; set; }
        public int Days { get; set; }
        public string Diagnosis { get; set; }
        public string Procedure { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? FollowDate { get; set; }

        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        //public int InstructionID { get; set; }

        public IEnumerable<SelectListItem> Instructions { get; set; }

        public List<int> SelectedInstructionsIDs { get; set; }

        public int PatientTypeID { get; set; }

        public string M { get; set; }
        public int Percent { get; set; }
        public string Less { get; set; }
        public Decimal Rs { get; set; }
        public Decimal Received { get; set; }
        public Decimal Pending { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }

        public IEnumerable<PrescriptionCategory> Categories { get; set; }
        //public IEnumerable<Medicine> Medicines { get; set; }
        public Medicine Medicine { get; set; }

        public IEnumerable<SelectListItem> PrescriptionImages { get; set; }

        public List<int> SelectedPrescriptionImages { get; set; }

        public string PatientImage1 { get; set; }
        public string PatientImage2 { get; set; }

        public int InvestigationID { get; set; }

        public int[] OINTTypeID { get; set; }
        public string[] Medicine_OINTMore { get; set; }
        public int[] MorningDozID { get; set; }
        public int[] NoonDozID { get; set; }
        public int[] NightDozID { get; set; }
        public int[] DosageID { get; set; }
        public int[] Medicine_Quantity { get; set; }

    }
}
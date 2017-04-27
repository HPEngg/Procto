using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PatientSearch
    {
        public int ID { get; set; }
        public int No { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Reference { get; set; }
        [Display(Name = "Referral")]
        public string RefferalName { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        public string Diagnosis { get; set; }
        public string Procedure { get; set; }
        public string PatientType { get; set; }
        public string NewPatientOrFollowUp { get; set; }
        public string ChargesType { get; set; }
    }

    public class PatientToday
    {
        public int ID { get; set; }
        public int No { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Reference { get; set; }
        [Display(Name = "Referral")]
        public string RefferalName { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public string RP { get; set; }
        public string KCO { get; set; }

        [Display(Name = "C / O")]
        public string CO { get; set; }
        [Display(Name = "Complain Since")]
        public string ComplainForm { get; set; }
        public int? Constipation { get; set; }
        public string ConstipationMore { get; set; }
        public int? Acidity { get; set; }
        public string AcidityMore { get; set; }
        public int? Gas { get; set; }
        public string GasMore { get; set; }
        public int? Pain { get; set; }
        public string PainMore { get; set; }
        public int? Burning { get; set; }
        public string BurningMore { get; set; }
        public int? Bleeding { get; set; }
        public Bleeding BleedingMore { get; set; }
        public int? Itching { get; set; }
        public string ItchingMore { get; set; }
        public int? PusDrainage { get; set; }
        public string PusDrainageMore { get; set; }
        public int? Swelling { get; set; }
        public string SwellingMore { get; set; }
        public string SCO { get; set; }
        public string ACO { get; set; }
        public string Allergy { get; set; }
        public string History { get; set; }

        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? T { get; set; }
        public string PR { get; set; }
        public string BP { get; set; }
        public string SPO2 { get; set; }
        public string PRR { get; set; }
        public string Proctoscopy { get; set; }
        public bool LightOnOff { get; set; }
        public string Other { get; set; }
        public DateTime? DOA { get; set; }
        public DateTime? DOD { get; set; }
        public string Dignosis { get; set; }
        public string Procedure { get; set; }
        public string Comment { get; set; }
        public int PatientID { get; set; }
    }
}
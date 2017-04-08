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
}
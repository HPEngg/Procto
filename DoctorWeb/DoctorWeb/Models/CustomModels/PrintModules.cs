using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class Header
    {
        public byte[] HeaderPhoto { get; set; }
        //public string HospitalName { get; set; }
        //public string SubName { get; set; }
        //public string TagLine { get; set; }
        //public string Address { get; set; }
        //public string DoctorName { get; set; }
        //public string DoctorTitle { get; set; }
        //public string PhoneNumber { get; set; }
    }
    public class PrintPatient
    {
        public int ID { get; set; }
        public byte[] Photo { get; set; }
        public byte[] UploadedImage1 { get; set; }
        public byte[] UploadedImage2 { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string TodayDate { get; set; }
        public string No { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string KCO { get; set; }
        public string ComplainOf { get; set; }
        public string Since { get; set; }
        public int? Constipation { get; set; }
        public string ConstipationMore { get; set; }
        public int? GAS { get; set; }
        public string GASMore { get; set; }
        public int? Acidity { get; set; }
        public string AcidityMore { get; set; }
        public int? Pain { get; set; }
        public string PainMore { get; set; }
        public int? Burning { get; set; }
        public string BurningMore { get; set; }
        public int? Bleeding { get; set; }
        public string BleedingMore { get; set; }
        public int? Swelling { get; set; }
        public string SwellingMore { get; set; }
        public string SCO { get; set; }
        public string ACO { get; set; }
        public string Allergy { get; set; }
        public string History { get; set; }
        public string Habbit { get; set; }
        public string Diet { get; set; }
        public string Height { get; set; }
        public string Temprature { get; set; }
        public string Pulse { get; set; }
        public string BP { get; set; }
        public string SPO2 { get; set; }
        public string PR { get; set; }

        public string Proctoscopy { get; set; }
        public string Others { get; set; }
        public string Diagnosis { get; set; }
        public string Advice { get; set; }
        public string Procedure { get; set; }
        public List<Investigation> Investigations { get; set; }
        public byte[] DrawenImage1 { get; set; }
        public byte[] DrawenImage2 { get; set; }
        public List<byte[]> PrescriptionImages { get; set; }
    }

    public class RX
    {
        public List<PrescriptionMedicine> Medicines { get; set; }
    }

    public class Compulsory
    {
        [Display(Name = "Follow Date")]
        public string FollowDate { get; set; }
        public string Day { get; set; }
        public List<Instruction> Instructions { get; set; }
    }

    public class Invoice
    {
        //public string Consult { get; set; }
        //public string Proctoscopy { get; set; }
        //public string Dressing { get; set; }
        //public string KSProcedure { get; set; }
        //public string Other { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public Decimal Medicine { get; set; }
        public Decimal Less { get; set; }
        public string Net { get; set; }
        public string Total { get; set; }
        public string CashRecived { get; set; }
        public string PendingAmount { get; set; }
        public Decimal OtherFromTextbox { get; set; }
    }

    public class Footer
    {
        public byte[] FooterPhoto { get; set; }
    }
}
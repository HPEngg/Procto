using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoctorWeb.Models.Enums;

namespace DoctorWeb.Models
{
    public class PatientHistory
    {
        public int ID { get; set; }
        public string RP { get; set; }
        public string KCO { get; set; }

        [Display(Name = "C / O")]
        public string CO { get; set; }
        [Display(Name = "Complain")]
        public string ComplainForm { get; set; }
        public int Constipation { get; set; }
        public string ConstipationMore { get; set; }
        public int Pain { get; set; }
        public string PainMore { get; set; }
        public int Burning { get; set; }
        public string BurningMore { get; set; }
        public int Bleeding { get; set; }
        public Bleeding BleedingMore { get; set; }
        public int Itching { get; set; }
        public string ItchingMore { get; set; }
        [Display(Name = "Pus Drainage")]
        public int PusDrainage { get; set; }
        public string PusDrainageMore { get; set; }
        public int Swelling { get; set; }
        public string SwellingMore { get; set; }
        public string SCO { get; set; }
        [Display(Name = "A/C/O")]
        public string ACO { get; set; }
        public string Allergy { get; set; }
        public string History { get; set; }

        public float Weight { get; set; }
        public float Height { get; set; }
        public int T { get; set; }
        public string PR { get; set; }
        public int BP { get; set; }
        public string SPO2 { get; set; }
        public string PRR { get; set; }
        public string Proctoscopy { get; set; }
        public bool LightOnOff { get; set; }
        public string Other { get; set; }
        public DateTime? DOA { get; set; }
        public DateTime? DOD { get; set; }
        public string Dignosis { get; set; }
        public string Procedure { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
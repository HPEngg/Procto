using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PrintModel
    {
        public string DateToday { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Habbits { get; set; }
        public string RefBy { get; set; }

        public string KCO { get; set; }
        public string CO { get; set; }
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

        public string Diagnosis { get; set; }
        public string FollowDate { get; set; }
        public string Instruction { get; set; }
        public string Rs { get; set; }
        public string Less { get; set; }
        public string Total { get; set; }

    }
}
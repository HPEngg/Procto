﻿using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PrintModel
    {
        [Display(Name = "Date")]
        public string DateToday { get; set; }
        [Display(Name = "OPD No")]
        public int PatientID { get; set; }
        [Display(Name = "Name")]
        public string PatientName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Occuption { get; set; }
        public string Habbits { get; set; }
        [Display(Name = "REF")]
        public string RefBy { get; set; }
        [Display(Name = "REF by Doctor")]
        public string RefDoctorName { get; set; }
        public byte[] PrescriptionImage { get; set; }
        public string KCO { get; set; }
        public string CO { get; set; }
        public string ComplainForm { get; set; }
        public int? Constipation { get; set; }
        public string ConstipationMore { get; set; }
        public int? Pain { get; set; }
        public string PainMore { get; set; }
        public int? Burning { get; set; }
        public string BurningMore { get; set; }
        public int? Bleeding { get; set; }
        public Bleeding BleedingMore { get; set; }
        public int? Itching { get; set; }
        public string ItchingMore { get; set; }
        [Display(Name = "Pus Drainage")]
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

        public string Diagnosis { get; set; }
        [Display(Name = "Follow Date")]
        public string FollowDate { get; set; }
        public string Instruction { get; set; }
        public string Rs { get; set; }
        public string Less { get; set; }
        public string Total { get; set; }

        public List<PrescriptionMedicine> Medicines { get; set; }

    }
}
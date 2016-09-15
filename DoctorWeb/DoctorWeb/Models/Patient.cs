using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public PatientStatus StatusColor { get; set; }
        public string Status { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public Department DepartmentID { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public Habit Habit { get; set; }
        public FoodPreference FoodPreference { get; set; }
        public ReferredBy ReferredBy { get; set; }

        public Doctor Doctor { get; set; }

        public string KCO { get; set; }
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
        public string Other { get; set; }
        public string PileMass { get; set; }
        public string Proctoscopy { get; set; }
        public bool LightOnOff { get; set; }
        public string RemindMeAbout { get; set; }
    }
}
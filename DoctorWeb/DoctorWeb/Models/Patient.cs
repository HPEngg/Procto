using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorWeb.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public PatientStatus Status { get; set; }

        [Display(Name = "Patient's Name")]
        public string Name { get; set; }
        public int Age { get; set; }

        [Display(Name = "Sex")]
        public Gender Gender { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Referred by")]
        public ReferredBy ReferredBy { get; set; }
        public string Relative { get; set; }

        [Display(Name = "Department")]
        public Department DepartmentID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact number must be numeric")]
        public string Contact { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Occupation { get; set; }
        public Habit Habit { get; set; }

        [Display(Name = "Food Preference")]
        public FoodPreference FoodPreference { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Remind me About")]
        public string RemindMeAbout { get; set; }
        public int? DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<PatientHistory> PatientHistories { get; set; }
    }
}
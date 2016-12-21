using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Doctor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Doctor’s Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact number must be numeric")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string DoctorType { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }

    }
}
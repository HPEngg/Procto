using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Instruction
    {
        public int ID { get; set; }

        [Display(Name = "Instruction Name")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
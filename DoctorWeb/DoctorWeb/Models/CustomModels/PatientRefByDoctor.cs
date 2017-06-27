﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PatientRefByDoctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Amount")]
        public decimal Ammount { get; set; }
    }
}
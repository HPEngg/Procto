using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Medicine
    {
        public int ID { get; set; }
        public MedicineType Type { get; set; }
        public string Name { get; set; }
        public string Morning { get; set; }
        public string After { get; set; }
        public string Night { get; set; }
        public string Time { get; set; }
    }
}
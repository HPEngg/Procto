using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Doz
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Medicine> MorningMedicines { get; set; }
        public ICollection<Medicine> NoonMedicines { get; set; }
        public ICollection<Medicine> NightMedicines { get; set; }
    }
}
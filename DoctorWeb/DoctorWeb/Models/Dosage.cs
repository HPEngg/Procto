using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Dosage
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<Medicine> Medicines { get; set; }
    }
}
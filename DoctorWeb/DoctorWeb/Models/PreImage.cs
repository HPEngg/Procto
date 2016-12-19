using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PreImage
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
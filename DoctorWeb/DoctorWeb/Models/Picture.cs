using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Picture
    {
        public int ID { get; set; }
        public byte[] Header { get; set; }
        public byte[] Footer { get; set; }
    }
}
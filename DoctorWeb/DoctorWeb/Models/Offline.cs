using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Offline
    {
        public int ID { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string Query { get; set; }
        public bool IsExecuted { get; set; }
    }
}
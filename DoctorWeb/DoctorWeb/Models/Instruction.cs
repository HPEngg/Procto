using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Instruction
    {
        public int ID { get; set; }
        public string InstructionName { get; set; }
        public string InstructionDescription { get; set; }
        public string Action { get; set; }
        public bool IsSync { get; set; }
    }
}
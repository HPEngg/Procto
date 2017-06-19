using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "bigint")]
        public long SortOrder { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
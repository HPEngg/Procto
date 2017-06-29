using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class Expanse
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}")]
        public DateTime Date { get; set; }
        public int ExpanseCategoryID { get; set; }
        public virtual ExpanseCategory ExpanseCategory { get; set; }
    }
}
using DoctorWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PrintModel
    {
        public bool HeaderRequired { get; set; }
        public Header Header { get; set; }
        public bool PatientRequired { get; set; }
        public PrintPatient Patient { get; set; }
        public bool RXRequired { get; set; }
        public RX RX { get; set; }
        public Compulsory Compulsory { get; set; }
        public bool InvoiceRequired { get; set; }
        public Invoice Invoice { get; set; }
    }
}
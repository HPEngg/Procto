﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.CustomModels
{
    public class PrintInvoice
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }

        public string TodayDate { get; set; }
        public string Address { get; set; }
        public int InvoiceNo { get; set; }

        public List<PrescriptionMedicine> Medicines { get; set; }

        public string Consult { get; set; }
        public string Proctoscopy { get; set; }
        public string Dressing { get; set; }
        public string KSProcedure { get; set; }
        public string Medicine { get; set; }
        public string Other { get; set; }
        public string Less { get; set; }
        public string Total { get; set; }

    }
}
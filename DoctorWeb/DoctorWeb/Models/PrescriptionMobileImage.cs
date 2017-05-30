using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
	public class PrescriptionMobileImage
	{
        public int ID { get; set; }
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
        public byte[] UploadedImage1 { get; set; }

        public byte[] UploadedImage2 { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }
    }
}
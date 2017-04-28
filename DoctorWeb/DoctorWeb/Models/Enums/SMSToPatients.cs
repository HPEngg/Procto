using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.Enums
{
    public enum SMSToPatients
    {
        [Display(Name = "Visiting Today")]
        VisitingToday,
        [Display(Name = "Visiting Tomorrow")]
        VisitingTomorow, All,
        [Display(Name = "Select Visit Dates")]
        SelectVisitDates,
        [Display(Name = "Enter Phone Number")]
        EnterManually
    }

    public enum SMSTypes
    {
        [Display(Name = "Personal Message")]
        Personal,
        [Display(Name = "Holiday Message 1")]
        Holiday1,
        [Display(Name = "Holiday Message 2")]
        Holiday2,
    }
}
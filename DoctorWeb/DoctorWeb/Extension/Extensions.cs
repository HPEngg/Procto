using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWeb.Extension
{
    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
    public static class CultureDate
    {
        public static DateTime ConvertUTCBasedOnCuture(DateTime utcTime)
        {
            //utcTime is 29 Dec 2013, 6:15 A.M
            string TimezoneId = "India Standard Time";

            // if the user changes culture from sv-se to ta-IN, different date is shown
            TimeZoneInfo tZone = TimeZoneInfo.FindSystemTimeZoneById(TimezoneId);

            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tZone);
        }

    }
}
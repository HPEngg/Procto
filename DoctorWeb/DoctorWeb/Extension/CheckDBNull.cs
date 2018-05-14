using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;


/// <summary>
/// Summary description for CheckDBNull
/// </summary>
public class CheckDBNull
{
    public static string ToStr(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                return Convert.ToString(readField);
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }


    public static double ToDbl(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.ToString().Trim().Length == 0)
                {
                    return 0.0;
                }
                else
                {
                    try
                    {
                        return Convert.ToDouble(readField);
                    }
                    catch
                    {
                        double toReturn = 0;
                        double.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
            }
            else
            {
                return 0.0;
            }
        }
        else
        {
            return 0.0;
        }
    }
    public static decimal ToDecimal(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.ToString().Trim().Length == 0)
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return Convert.ToDecimal(readField);
                    }
                    catch
                    {
                        decimal toReturn = 0;
                        decimal.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    public static long ToLong(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.ToString().Trim().Length == 0)
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return Convert.ToInt64(readField);
                    }
                    catch
                    {
                        long toReturn = 0;
                        Int64.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }


    public static bool ToBool(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                try
                {
                    if (readField.ToString() == string.Empty)
                        return false;
                    else
                        return Convert.ToBoolean(readField);
                }
                catch
                {

                    bool isBool;
                    int isInt;
                    if (bool.TryParse(readField.ToString().Trim(), out isBool))
                        return isBool;
                    else if (int.TryParse(readField.ToString().Trim(), out isInt))
                        return Convert.ToBoolean(isInt);
                    else
                        return false;
                    //return isBool;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static int ToInt(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.ToString().Trim().Length == 0)
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return Convert.ToInt32(readField);
                    }
                    catch
                    {
                        int toReturn = 0;
                        int.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }


    public static DateTime ToDate(object readField, int old)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                return Convert.ToDateTime(readField);
            }
        }
        return DateTime.MinValue;
    }


    public static DateTime ToDate(object readField, string CurrentCulture = "en-US")
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.ToString() != string.Empty)
                {
                    CultureInfo culture = new CultureInfo(CurrentCulture);
                    try
                    {
                        return Convert.ToDateTime(readField, culture);
                    }
                    catch
                    {
                        return DateTime.MinValue;
                    }
                }
            }
        }
        return DateTime.MinValue;
    }


    //public static DateTime ToDate(object readField, Microsoft.VisualBasic.DateFormat dateFormat)
    //{
    //    if ((readField != null))
    //    {
    //        if (readField.GetType() != typeof(System.DBNull))
    //        {
    //            if (dateFormat != 0)
    //            {
    //                return Convert.ToDateTime(Microsoft.VisualBasic.Strings.FormatDateTime(Convert.ToDateTime(readField), dateFormat));
    //            }
    //            return Convert.ToDateTime(readField);
    //        }
    //    }
    //    return DateTime.MinValue;
    //}


    //public static object ToObj(object value)
    //{
    //    if (Microsoft.VisualBasic.Information.IsDBNull(value) == false)
    //    {
    //        try
    //        {
    //            if (value == null)
    //            {
    //                return DBNull.Value;
    //            }
    //        }
    //        catch
    //        {
    //        }

    //        try
    //        {
    //            if ((value == null))
    //            {
    //                return DBNull.Value;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //    }
    //    return value;
    //}
    public static TimeSpan ToTime(object readField)
    {
        if ((readField != null))
        {
            if (readField.GetType() != typeof(System.DBNull))
            {
                if (readField.GetType() == typeof(System.TimeSpan))
                {
                    return (TimeSpan)readField;
                }
            }
        }
        return TimeSpan.MinValue;
    }
    public static object dbAllowNull(string value)
    {
        if (value == null | string.IsNullOrEmpty(value))
        {
            return System.DBNull.Value;
        }
        return value;
    }
}
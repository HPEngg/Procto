using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DoctorWeb.Models.Tools
{
    public static class SMSHelper
    {

        //public static string sendMessage(string phoneNo, string message)
        //{

        //    string sUserID = "STL Tester";
        //    string sApikey = "1TZbP0PzfpzrtRuZ71Vt";
        //    string sNumber = phoneNo;
        //    string sSenderid = "639024";
        //    string sMessage = message;
        //    string sType = "txt";
        //    string sURL = "http://smshorizon.co.in/api/sendsms.php?user=" + sUserID + "&apikey=" + sApikey + "&mobile=" + sNumber + "&senderid=" + sSenderid + "&message=" + sMessage + "&type=" + sType + "";
        //    //"http://smshorizon.co.in/api/sendsms.php?user=STL Tester&apikey=1TZbP0PzfpzrtRuZ71Vt&mobile=xxyy&message=xxyy&senderid=xxyy&type=txt";

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL); request.MaximumAutomaticRedirections = 4;
        //    request.Credentials = CredentialCache.DefaultCredentials;
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse(); Stream receiveStream = response.GetResponseStream();
        //        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        //        string sResponse = readStream.ReadToEnd();
        //        response.Close();
        //        readStream.Close();
        //        return sResponse;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        public static string sendMessage(string phoneNo, string message)
        {
            string url = "http://login.bulksmsgateway.in/sendmessage.php";
            string result = "";
            message = HttpUtility.UrlPathEncode(message);
            String strPost = "?user=" + HttpUtility.UrlPathEncode("drhirenpatel") + "&password=" + HttpUtility.UrlPathEncode("9428131284") + "&sender=" + HttpUtility.UrlPathEncode("TestID") + "&mobile=" + HttpUtility.UrlPathEncode(phoneNo) + "&type=" + HttpUtility.UrlPathEncode("3") + "&message=" + message;
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url + strPost);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader sr.Close();
            }
            return result;
        }
    }
}
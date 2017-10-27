using System;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace CoursePlanner.Api
{
    public static class Utilities
    {
        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string FormatTime(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }

        public static void AddAuthentication(ref HttpWebRequest Request, string LoginToken)
        {
            string authorizationCode = String.Format("Bearer {0}", LoginToken);
            Request.Headers.Add("authorization", authorizationCode);
        }

        public static bool ValidateHash(string Request, string SavedHashed)
        {
            return GetHashed(Request) == SavedHashed;
        }

        public static string GetHashed(string Input)
        {
            byte[] data = Encoding.ASCII.GetBytes(Input);
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] sha256data = sha256.ComputeHash(data);
            return Encoding.ASCII.GetString(sha256data);
        }
    }
}

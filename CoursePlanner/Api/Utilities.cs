using System;
using System.Net;

namespace CoursePlanner.Api
{
    public class Utilities
    {
        private Utilities()
        {
        }

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
    }
}

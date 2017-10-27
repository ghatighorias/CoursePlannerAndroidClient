using System;
using System.IO;
using System.Net;
using CoursePlanner.Api;

namespace CoursePlanner
{
    public class CalendarHandler
    {
        public delegate void CalendarCallBack(HttpWebResponse Response, CalendarJson CalendarData);
        public event CalendarCallBack Callback;

        private CalendarJson calendarData;
        public CalendarJson CalendarData
        {
            get { return calendarData; }
        }

        public string Token
        {
            get;
            set;
        }

        private Uri calendarApiLink;
        public Uri CalendarApiLink
        {
            get { return calendarApiLink; }
            private set { calendarApiLink = value; }
        }

        public CalendarHandler(Uri CalendarApiLink, string AuthenticationToken)
        {
            calendarApiLink = CalendarApiLink;
            Token = AuthenticationToken;
        }

        public async void ReadCalendar(DateTime dateTime, bool myClasses)
        {
            HttpWebRequest request = CreateGetRequest(dateTime, myClasses);

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Deserializing response
                using (Stream stream = response.GetResponseStream())
                {
                    CalendarJson.DeserializeFromStream(stream, out calendarData);
                }

                // Triggers the callback event
                Callback?.Invoke((HttpWebResponse)response, calendarData);
            }
        }

        private HttpWebRequest CreateGetRequest(DateTime dateTime, bool myClasses)
        {
            string completeRequestUrl = CreateCalendarApiRequestUrlWithParam(dateTime, myClasses);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(completeRequestUrl);
            request.ContentType = "application/json";
            request.Method = "GET";
            Utilities.AddAuthentication(ref request, Token);
            return request;
        }

        private string CreateCalendarApiRequestUrlWithParam(DateTime dateTime, bool MyClasses)
        {
            string requestParams = CreateCalendarRequestParam(dateTime, MyClasses);
            return String.Format("{0}?{1}", CalendarApiLink, requestParams);
        }

        private string CreateCalendarRequestParam(DateTime dateTime, bool MyClasses)
        {
            string formattedDate = Utilities.FormatDate(dateTime);
            return  String.Format("date={0}", formattedDate);
        }


    }
}

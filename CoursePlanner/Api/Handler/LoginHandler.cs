using System;
using System.Net;
using System.IO;
using CoursePlanner.Api;

namespace CoursePlanner
{
    public class LoginHandler
    {
        
        public delegate void LoginCallBack(HttpWebResponse Response, LoginStatus Status, string LoginToken);
        public event LoginCallBack Callback;

        public LoginStatus Status
        {
            get;
            private set;
        }

        public int LoginAttempts
        {
            get;
            private set;
        }

        private LoginJson loginData;
        public LoginJson LoginData
        {
            get { return loginData; }
        }

        public Uri LoginLink
        {
            get;
            private set;
        }

        public LoginHandler(Uri LoginUrl)
        {
            LoginLink = LoginUrl;
        }

        public async void AttemptLogin(string Email, string Password, bool SaveLoginDetail = false)
        {
            HttpWebRequest request = CreateLoginPostRequest(Email, Password);

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Deserializing response
                using (Stream stream = response.GetResponseStream())
                {
                    LoginJson.DeserializeFromStream(stream, out loginData);
                }

                var loginStatus = LoginData.GetLoginStatus();

                if (loginStatus == LoginStatus.Successful)
                {
                    UpdateLoginDetail(Email, Password, SaveLoginDetail);
                    LoginAttempts = 0;
                }
                else
                {
                    LoginAttempts++;
                }

                // Triggers the callback event
                Callback?.Invoke((HttpWebResponse)response, loginStatus, LoginData.Token);
            }
        }

        private HttpWebRequest CreateLoginPostRequest(string Email, string Password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LoginLink);
            request.ContentType = "application/json";
            request.Method = "POST";
            AddPostParams(ref request, Email, Password);
            return request;
        }

        private void AddPostParams(ref HttpWebRequest Request, string Email, string Password)
        {
            string postString = "{" + string.Format("\"email\":\"{0}\",\"password\":\"{1}\"", Email, Password) + "}";
            Request.ContentLength = postString.Length;
            var stream = Request.GetRequestStream();
            StreamWriter requestWriter = new StreamWriter(stream);
            requestWriter.Write(postString);
            requestWriter.Close();
        }

        public bool UpdateLoginDetail(string Email, string Password, bool SaveLoginDetail = false)
        {
            var setting = UserSettings.LoadSetting();
            if (SaveLoginDetail)
            {
                setting.UserName = Email;
                setting.PasswordHash = Utilities.GetHashed(Password);
            }
            else
            {
                setting.UserName = String.Empty;
                setting.PasswordHash = String.Empty;
            }
            return UserSettings.SaveSetting(setting);
        }
    }
}

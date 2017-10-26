using System;
using System.Net;
using System.IO;
                
namespace CoursePlanner
{
    public class LoginHandler
    {
        
        public delegate void LoginCallBack(HttpWebResponse Response, string LoginToken);
        public event LoginCallBack Callback;

        private int loginAttempts;
        public int LoginAttempts
        {
            get { return loginAttempts; }
            private set { loginAttempts = value; }
        }

        private LoginJson loginData;
        public LoginJson LoginData
        {
            get { return loginData; }
        }

        private Uri loginLink;
        public Uri LoginLink
        {
            get { return loginLink; }
            private set { loginLink = value; }
        }

        public LoginHandler(Uri LoginLink)
        {
            loginLink = LoginLink;
        }

        public async void AttemptLogin(string Email, string Password)
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

                // Triggers the callback event
                Callback?.Invoke((HttpWebResponse)response, LoginData.Token);
            }
        }

        private HttpWebRequest CreateLoginPostRequest(string Email, string Password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginLink);
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
    }
}

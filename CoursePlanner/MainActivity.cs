using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net;
using Android.Content;
using Android.Views;
using CoursePlanner.Api;
             
namespace CoursePlanner
{
    [Activity(Label = "CoursePlanner", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        Uri loginUri = new Uri("http://10.0.2.2:4000/api/sessions");

        LoginHandler loginHandler;
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            loginHandler = new LoginHandler(loginUri);

            loginHandler.Callback += LoginHandler_Callback;

            SetContentView(Resource.Layout.Main);

            ToggleLogginErrorTextView(false);

            Button loginButton = FindViewById<Button>(Resource.Id.button1);
            loginButton.Click += LoginButton_Click;

        }

        void LoginButton_Click(object sender, EventArgs e)
        {
            ToggleLogginErrorTextView(false);

            EditText email = FindViewById<EditText>(Resource.Id.editText2);
            EditText password = FindViewById<EditText>(Resource.Id.editText4);
            loginHandler.AttemptLogin(email.Text, password.Text);
        }

        void LoginHandler_Callback(System.Net.HttpWebResponse Response, LoginStatus Status, string LoginToken)
        {
            if (Status == LoginStatus.Successful)
            {
                var calendarListIntent = new Intent(this, typeof(CalendarList));
                calendarListIntent.PutExtra("Token", loginHandler.LoginData.Token);
                StartActivity(calendarListIntent);
            }
            else
            {
                ToggleLogginErrorTextView(true);
            }
        }

        private void ToggleLogginErrorTextView(bool On)
        {
            if (On)
            {
                FindViewById<TextView>(Resource.Id.LoginErrorLabel).Text = "User name or password is incorrect";
                FindViewById<TextView>(Resource.Id.LoginErrorLabel).Visibility = ViewStates.Visible;
            }
            else
            {
                FindViewById<TextView>(Resource.Id.LoginErrorLabel).Text = "";
                FindViewById<TextView>(Resource.Id.LoginErrorLabel).Visibility = ViewStates.Invisible;
            }
        }
    }
}


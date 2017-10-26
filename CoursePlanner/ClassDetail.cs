    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CoursePlanner
{
    [Activity(Theme = "@android:style/Theme.Dialog")]
    public class ClassDetail : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string Term_name = Intent.GetStringExtra("Term_name") ?? "";
            string Course_name = Intent.GetStringExtra("Course_name") ?? "";
            string Date = Intent.GetStringExtra("Date") ?? "";
            string Starting_at = Intent.GetStringExtra("Starting_at") ?? "";
            string Finishes_at = Intent.GetStringExtra("Finishes_at") ?? "";
            string Classroom = Intent.GetStringExtra("Classroom") ?? "";

            SetContentView(Resource.Layout.ClassDetail);

            FindViewById<TextView>(Resource.Id.ClassDetailTermName).Text = Term_name;
            FindViewById<TextView>(Resource.Id.ClassDetailCourseName).Text = Course_name;
            FindViewById<TextView>(Resource.Id.ClassDetailTime).Text = String.Format("{0} - {1}", Starting_at, Finishes_at);
            FindViewById<TextView>(Resource.Id.ClassDetailClassroom).Text = Classroom;
        }
    }
}

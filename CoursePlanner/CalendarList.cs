
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
using CoursePlanner.Api.Json;
using static Android.Widget.TableRow;

namespace CoursePlanner
{
    [Activity(Label = "CalendarList")]
    public class CalendarList : Activity
    {
        Uri calendarUri = new Uri("http://10.0.2.2:4000/calendar");

        CalendarHandler calendarHandler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string authenticationToken = Intent.GetStringExtra("Token") ?? "";

            calendarHandler = new CalendarHandler(calendarUri, authenticationToken);

            calendarHandler.Callback += CalendarHandler_Callback;

            SetContentView(Resource.Layout.CalendarList);

            calendarHandler.ReadCalendar(DateTime.Now, true);
        }

        void CalendarHandler_Callback(System.Net.HttpWebResponse Response, CalendarJson CalendarData)
        {
            TableLayout table = FindViewById<TableLayout>(Resource.Id.tableLayout1);

            //empty the table
            table.RemoveAllViews();

            //create row layour parameter set
            LayoutParams layoutParams = new TableRow.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);

            //create and add table header row
            var headerColumns = new View[]
            {
                CreateTableLabels("Class"),
                CreateTableLabels("Time")
            };
            TableRow headerRow = CreateRowWithColumns(headerColumns, layoutParams, false, -1);
            table.AddView(headerRow, 0);

            //create and add class rows
            for (int index = 0; index < CalendarData.Classes.Count; index++)
            {
                var classData = CalendarData.Classes[index];
                var classColumns = new View[]
                {
                    CreateTableLabels(classData.Course_name),
                    CreateTableLabels(classData.Starting_at.ToShortTimeString())
                };
                TableRow classRow = CreateRowWithColumns(classColumns, layoutParams, true, index);
                table.AddView(classRow);
            }
        }

        private TableRow CreateRowWithColumns(View[] views, LayoutParams layoutParams, bool BindClickEvent, int classRowIndex)
        {
            TableRow tableRow = new TableRow(this);

            if (BindClickEvent)
            {
                tableRow.Click += TableRow_Click;
                tableRow.Tag = classRowIndex;
            }

            foreach (var item in views)
            {
                tableRow.AddView(item, layoutParams);
            }

            return tableRow;
        }

        void TableRow_Click(object sender, EventArgs e)
        {
            TableRow ClickedRow = (TableRow)sender;
            int classRowIndex = (int)ClickedRow.Tag;
            var ClassData = calendarHandler.CalendarData.Classes[classRowIndex];

            var classDetail = new Intent(this, typeof(ClassDetail));
            classDetail.PutExtra("Term_name", ClassData.Term_name);
            classDetail.PutExtra("Course_name", ClassData.Course_name);
            classDetail.PutExtra("Date", ClassData.Date.ToShortDateString());
            classDetail.PutExtra("Starting_at", ClassData.Starting_at.ToShortTimeString());
            classDetail.PutExtra("Finishes_at", ClassData.Finishes_at.ToShortTimeString());
            classDetail.PutExtra("Classroom", ClassData.Classroom);
            StartActivity(classDetail);
        }

        private TextView CreateTableLabels(string text)
        {
            TextView label = new TextView(this);
            label.Text = text;
            return label;
        }
    }
}

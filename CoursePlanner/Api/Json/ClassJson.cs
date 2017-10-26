using System;
using System.Collections.Generic;

namespace CoursePlanner.Api.Json
{
    public class ClassJson
    {
        public string Course_name
        {
            get;
            set;
        }
        public string Term_name
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public DateTime Starting_at
        {
            get;
            set;
        }
        public DateTime Finishes_at
        {
            get;
            set;
        }
        public string Classroom
        {
            get;
            set;
        }
        public List<UserJson> Teachers
        {
            get;
            set;
        }

        public ClassJson()
        {
        }
    }
}

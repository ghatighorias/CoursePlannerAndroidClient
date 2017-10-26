using System.IO;
using Newtonsoft.Json;
using CoursePlanner.Api.Json;
using System.Collections.Generic;

namespace CoursePlanner
{
    public class CalendarJson
    {
        public List<ClassJson> Classes
        {
            get;
            set;
        }

        public CalendarJson()
        {
        }

        public static void DeserializeFromStream(Stream stream, out CalendarJson loginJson)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                loginJson = serializer.Deserialize<CalendarJson>(jsonTextReader);
            }
        }
    }
}

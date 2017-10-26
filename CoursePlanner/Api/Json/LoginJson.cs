using System.IO;
using Newtonsoft.Json;

namespace CoursePlanner
{
    public class LoginJson
    {
        public string Token
        {
            get;
            set;
        }

        public LoginJson()
        {

        }

        public static void DeserializeFromStream(Stream stream, out LoginJson loginJson)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                loginJson = serializer.Deserialize<LoginJson>(jsonTextReader);
            }
        }
    }
}

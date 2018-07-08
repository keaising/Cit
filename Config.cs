using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Shuxiao.Wang.Cit
{

    public class Config
    {
        public static void SetPath(string path)
        {
            var file = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "cit.config");
            if (!File.Exists(file))
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(new { Path = path }));
                return;
            }
            var content = File.ReadAllText(file);
            var source = JObject.Parse(content);
            if (source.ContainsKey("Path"))
                source["Path"] = path;
            else
                source.Add("Path", path);
            File.WriteAllText(file, JsonConvert.SerializeObject(source));
        }
        public static string GetPath()
        {
            var file = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "cit.config");
            var content = File.ReadAllText(file);
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;
            var source = JObject.Parse(content);
            if (source.ContainsKey("Path"))
                return source.Value<string>("Path");
            else
                return string.Empty;
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Shuxiao.Cit
{
    public class Config
    {
        private static string configName = "cit.json";
        private static string pathName = "Path";
        public static void SetPath(string path)
        {
            var file = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), configName);
            if (path.ToLower() == "Empty".ToLower())
                path = "";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(new { Path = path }));
                return;
            }
            var content = File.ReadAllText(file);
            var source = JObject.Parse(content);
            if (source.ContainsKey(pathName))
                source[pathName] = path;
            else
                source.Add(pathName, path);
            File.WriteAllText(file, JsonConvert.SerializeObject(source));
        }
        public static string GetPath()
        {
            var file = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), configName);
            var content = File.ReadAllText(file);
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;
            var source = JObject.Parse(content);
            if (source.ContainsKey(pathName))
                return source.Value<string>(pathName);
            else
                return string.Empty;
        }
    }
}
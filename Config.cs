using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Config
{
    public static void SetPath(string path)
    {
        string file = System.IO.File.ReadAllText("cit.config");
        if (string.IsNullOrWhiteSpace(file))
        {
            System.IO.File.WriteAllText("cit.config", JsonConvert.SerializeObject(new { Path = path }));
            return;
        }
        var source = JObject.Parse(file);
        if (source.ContainsKey("Path"))
            source["Path"] = path;
        else
            source.Add("Path", path);
        System.IO.File.WriteAllText("cit.config", JsonConvert.SerializeObject(source));
    }
    public static string GetPath()
    {
        string file = System.IO.File.ReadAllText("cit.config");
        if (string.IsNullOrWhiteSpace(file))
            return string.Empty;
        var source = JObject.Parse(file);
        if (source.ContainsKey("Path"))
            return source.Value<string>("Path");
        else
            return string.Empty;
    }
}
using System.Text.RegularExpressions;

namespace Shuxiao.Wang.Cit
{
    public class Url
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Repo { get; set; }
    }

    public static class UrlExtend
    {
        public static Url Resolve(this string url)
        {
            string pattern = $@"[a-zA-Z]+@(?<HostName>[a-zA-Z.]+):(?<UserName>[a-zA-Z\_][a-zA-Z\_0-9]+)/(?<Repo>[a-zA-Z]+).git";

            var matches = Regex.Matches(url, pattern);
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                return new Url
                {
                    HostName = groups[nameof(Url.HostName)].Value,
                    UserName = groups[nameof(Url.UserName)].Value,
                    Repo = groups[nameof(Url.Repo)].Value
                };
            }
            return new Url();
        }
    }
}
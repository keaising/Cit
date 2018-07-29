using System.Text.RegularExpressions;

namespace Shuxiao.Cit
{
    public class Url
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Repo { get; set; }
    }

    public static class UrlExtend
    {
        public static Url ResolveUrl(this string url)
        {
            string pattern = "[a-zA-Z]+(@|://)(?<HostName>[a-zA-Z_][a-zA-Z0-9._]+)(:|/)(?<UserName>[a-zA-Z_][a-zA-Z_0-9]+)/(?<Repo>[a-zA-Z_][a-zA-Z0-9_]+).git";
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
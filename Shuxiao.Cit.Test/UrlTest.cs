using System;
using Xunit;
using Shuxiao.Cit;

namespace Shuxiao.Cit.Test
{
    public class UrlTest
    {
        [Fact]
        public void MatchGit()
        {
            var result = "git@github.com:keaising/cit.git".ResolveUrl();
            Assert.Equal("github.com", result.HostName, ignoreCase: true);
            Assert.Equal("keaising", result.UserName, ignoreCase: true);
            Assert.Equal("cit", result.Repo, ignoreCase: true);
        }

        [Fact]
        public void MatchHttp()
        {
            var result = "https://github.com/keaising/Cit.git".ResolveUrl();
            Assert.Equal("github.com", result.HostName, ignoreCase: true);
            Assert.Equal("keaising", result.UserName, ignoreCase: true);
            Assert.Equal("cit", result.Repo, ignoreCase: true);
        }
    }
}

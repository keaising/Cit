using System;
using System.Collections.Generic;
using CommandLine;

namespace Shuxiao.Wang.Cit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLine.Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                    .WithNotParsed<Options>((errs) => HandleParseError(errs));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex.Message);
            }
            Console.WriteLine("Done!");
        }

        static void RunOptionsAndReturnExitCode(Options opts)
        {
            if (!string.IsNullOrWhiteSpace(opts.Path))
            {
                Config.SetPath(opts.Path);
            }
            var path = Config.GetPath();
            if (string.IsNullOrWhiteSpace(opts.Clone))
            {
                Console.WriteLine("Warning: No Clone url, github repo will be clone to current directory.");
            }
            var names = GetRepoName(opts.Clone);
            var cmd = $"git clone {opts.Clone} {path}\\{names[0]}\\{names[1]}";
            var ret = cmd.Execute();
            if (!string.IsNullOrWhiteSpace(ret))
                Console.WriteLine($"Git Execute result: {ret}");
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var err in errs)
            {
                Console.WriteLine($"Oops! The error message: {err.Tag.ToString()}");
            }
        }

        static string[] GetRepoName(string repoUrl)
        {
            var arr = repoUrl.Split('/');
            var repoName = arr[arr.Length - 1];
            repoName = repoName.Substring(0, repoName.Length - 4);
            var userName = arr[arr.Length - 2];
            if (repoUrl.StartsWith("git"))
            {
                var temp = userName.Split(':');
                userName = temp[temp.Length - 1];
            }
            return new string[] { userName, repoName };
        }
    }
}

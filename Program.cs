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
                ConsoleHelper.WriteError("Error! " + ex.Message);
            }
        }

        static void RunOptionsAndReturnExitCode(Options opts)
        {
            ///path
            var path = string.Empty;
            if (!string.IsNullOrWhiteSpace(opts.Path))
            {
                Config.SetPath(opts.Path);
                path = Config.GetPath();
                ConsoleHelper.WriteInfo($"github.com path is \"{path}\" now");
            }
            else
                path = Config.GetPath();
            if (string.IsNullOrWhiteSpace(path))
            {
                ConsoleHelper.WriteInfo("No Clone url, github repo will be clone to current directory.", ConsoleColor.Yellow);
            }

            ///clone repo
            if (!string.IsNullOrWhiteSpace(opts.Clone))
            {
                var names = GetRepoName(opts.Clone);
                path = string.IsNullOrWhiteSpace(path) ?
                    path :
                    $"{path}\\{names[0]}\\{names[1]}";
                var cmd = $"git clone {opts.Clone} {path}";
                cmd.Execute();
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var err in errs)
            {
                if (!err.Tag.ToString().Equals("UnknownOptionError") &&
                    !err.Tag.ToString().Equals("HelpRequestedError") &&
                    !err.Tag.ToString().Equals("VersionRequestedError") &&
                    !err.Tag.ToString().Equals("MissingValueOptionError"))
                    ConsoleHelper.WriteError($"{err.Tag.ToString()}");
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

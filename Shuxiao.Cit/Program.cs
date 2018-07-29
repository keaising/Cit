using System;
using System.IO;
using System.Collections.Generic;
using CommandLine;

namespace Shuxiao.Cit
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
                ConsoleHelper.WriteInfo($"Git pull destination is \"{path}\" now");
            }
            else
                path = Config.GetPath();
            if (string.IsNullOrWhiteSpace(path))
            {
                ConsoleHelper.WriteInfo("No Clone url, git repo will be clone to current directory.", ConsoleColor.Yellow);
            }

            ///clone repo
            if (!string.IsNullOrWhiteSpace(opts.Clone))
            {
                var url = opts.Clone.ResolveUrl();
                path = string.IsNullOrWhiteSpace(path) ?
                             path : Path.Combine(path, url.HostName, url.UserName, url.Repo);
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
    }
}

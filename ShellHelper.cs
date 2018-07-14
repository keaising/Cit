using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Shuxiao.Wang.Cit
{
    public static class ShellHelper
    {
        public static void Execute(this string cmd)
        {
            var ret = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                Cmd(cmd) :
                Bash(cmd);
            if (!string.IsNullOrWhiteSpace(ret))
                ConsoleHelper.WriteInfo($"Git Execute result: {ret}", ConsoleColor.Yellow);
            else
                ConsoleHelper.WriteInfo("Success! git clone done.", ConsoleColor.Green);
        }
        private static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            ConsoleHelper.WriteInfo($"git command will be execute:");
            ConsoleHelper.WriteWithoutNote(cmd);
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }

        private static string Cmd(string cmd)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ConsoleHelper.WriteInfo($"next git command will be execute:");
            ConsoleHelper.WriteWithoutNote(cmd);
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/C {cmd}",
                }
            };
            process.Start();
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }
    }
}
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ShellHelper
{
    public static string Execute(this string cmd)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            Cmd(cmd) :
            Bash(cmd);
    }
    private static string Bash(string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");
        //System.Console.WriteLine($"cmd:{cmd}");
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
        System.Console.WriteLine($"cmd:{cmd}");
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
using System;

namespace Shuxiao.Wang.Cit
{
    public class ConsoleHelper
    {
        public static void WriteInfo(string info, ConsoleColor frontColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("dnc-git: ");
            Console.ResetColor();
            Console.ForegroundColor = frontColor;
            Console.WriteLine(info);
            Console.ResetColor();
        }

        public static void WriteError(string errMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"dnc-git error: {errMsg}");
            Console.ResetColor();
        }

        public static void WriteWithoutNote(string info, ConsoleColor frontColor = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("     >>> ");
            Console.ForegroundColor = frontColor;
            Console.WriteLine($"{info}");
            Console.ResetColor();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCreditSystem
{
    static class Connect
    {
        public const string path = "Data Source=DESKTOP-G6SLUNJ; Initial catalog=AcademyCreditSystem; Integrated security=true";
    }
    public static class Color
    {
        public static void GetResetColor() => Console.ResetColor();
        public static void GetBlueColor() => Console.ForegroundColor = ConsoleColor.Blue;
        public static void GetRedColor() => Console.ForegroundColor = ConsoleColor.Red;
        public static void GetGreenColor() => Console.ForegroundColor = ConsoleColor.Green;
        public static void GetMagentaColor() => Console.ForegroundColor = ConsoleColor.Magenta;
    }

}

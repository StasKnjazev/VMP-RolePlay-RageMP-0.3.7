using System;
using GTANetworkAPI;

namespace GVMPc
{
    class Log : Script
    {
        public static void Write(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[HawaiiRoleplay]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
        }
    }
}

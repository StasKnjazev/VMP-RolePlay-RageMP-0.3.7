using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc
{
    public class Input : Script
    {
        public static void sendPlayerInput(Client p, string eventname, bool remote, string argument = "")
        {
            p.TriggerEvent("sendPlayerInput", eventname, remote, argument);
        }
    }
}

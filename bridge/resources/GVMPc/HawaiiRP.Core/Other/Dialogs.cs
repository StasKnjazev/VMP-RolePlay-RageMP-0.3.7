using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc
{
    public class Dialogs : Script
    {
        public static void sendPlayerDialog(Client p, string title, string description, string eventname, bool remote, string argument = "")
        {
            p.TriggerEvent("sendPlayerDialog", title, description, eventname, remote, argument);
        }
    }
}

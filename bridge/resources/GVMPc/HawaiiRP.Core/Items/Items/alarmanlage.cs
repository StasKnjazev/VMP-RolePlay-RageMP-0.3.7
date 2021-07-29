using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class alarmanlage : Item
    {

        public alarmanlage()
        {
            Id = 22;
            Name = "alarmanlage";
            ImagePath = "alarmanlage.png";
            WeightInG = 1000;
            MaxStackSize = 5;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
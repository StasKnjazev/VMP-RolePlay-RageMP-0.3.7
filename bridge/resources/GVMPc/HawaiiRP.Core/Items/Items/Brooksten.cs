using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Brooksten : Item
    {

        public Brooksten()
        {
            Id = 85;
            Name = "Malboro Rot";
            ImagePath = "Brooksten.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Griff : Item
    {

        public Griff()
        {
            Id = 138;
            Name = " Waffen Griff";
            ImagePath = "Griff.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
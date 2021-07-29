using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Absperrung : Item
    {

        public Absperrung()
        {
            Id = 15;
            Name = "Absperrung";
            ImagePath = "Absperrung.png";
            WeightInG = 5000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
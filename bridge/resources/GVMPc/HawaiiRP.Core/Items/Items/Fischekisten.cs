using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Fischekisten : Item
    {

        public Fischekisten()
        {
            Id = 123;
            Name = "Fisch Kiste";
            ImagePath = "Fischekisten.png";
            WeightInG = 1000;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
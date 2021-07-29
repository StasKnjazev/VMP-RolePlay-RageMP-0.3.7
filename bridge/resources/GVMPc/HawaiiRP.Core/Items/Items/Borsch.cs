using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Borschtsch : Item
    {

        public Borschtsch()
        {
            Id = 81;
            Name = "Borsch";
            ImagePath = "Borschtsch.png";
            WeightInG = 60;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
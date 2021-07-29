using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Barsch : Item
    {

        public Barsch()
        {
            Id = 60;
            Name = "Barsch";
            ImagePath = "Barsch.png";
            WeightInG = 90;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
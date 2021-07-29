using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Hamburgerpack : Item
    {

        public Hamburgerpack()
        {
            Id = 143;
            Name = "Hamburger Pack";
            ImagePath = "Hamburgerpack.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
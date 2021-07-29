using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Blumentopf : Item
    {

        public Blumentopf()
        {
            Id = 78;
            Name = "Blumentopf";
            ImagePath = "Blumentopf.png";
            WeightInG = 160;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
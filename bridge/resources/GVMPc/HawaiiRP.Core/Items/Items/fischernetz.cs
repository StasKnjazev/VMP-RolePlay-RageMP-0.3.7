using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class fischernetz : Item
    {

        public fischernetz()
        {
            Id = 124;
            Name = "Fischernetz";
            ImagePath = "fischernetz.png";
            WeightInG = 800;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
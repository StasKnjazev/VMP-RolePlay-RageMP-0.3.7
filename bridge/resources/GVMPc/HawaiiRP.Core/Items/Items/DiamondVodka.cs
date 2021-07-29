using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class DiamondVodka : Item
    {

        public DiamondVodka()
        {
            Id = 111;
            Name = "DiamondVodka";
            ImagePath = "DiamondVodka.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
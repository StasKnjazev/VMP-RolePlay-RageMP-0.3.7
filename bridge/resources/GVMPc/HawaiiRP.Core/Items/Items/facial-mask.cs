using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class facialmask : Item
    {
        public facialmask()
        {
            Id = 121;
            Name = "Hamids Maske";
            ImagePath = "facial-mask.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}

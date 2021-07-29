using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class giftbasket : Item
    {

        public giftbasket()
        {
            Id = 133;
            Name = "Geschenk Korp";
            ImagePath = "gift-basket.png";
            WeightInG = 800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
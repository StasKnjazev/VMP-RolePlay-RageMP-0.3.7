using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Beatmungsgearet : Item
    {

        public Beatmungsgearet()
        {
            Id = 64;
            Name = "Beatmungsgerät";
            ImagePath = "Beatmungsgearet.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
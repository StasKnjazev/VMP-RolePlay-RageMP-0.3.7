using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Ephidrinkonzentrat : Item
    {

        public Ephidrinkonzentrat()
        {
            Id = 75;
            Name = "Ephidrinkonzentrat";
            ImagePath = "Ephedrinkonzentrat.png";
            WeightInG = 750;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}

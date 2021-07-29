using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Adipinkonzentrat : Item
    {

        public Adipinkonzentrat()
        {
            Id = 18;
            Name = "Adipinkonzentrat";
            ImagePath = "Adipinkonzentrat.png";
            WeightInG = 120;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
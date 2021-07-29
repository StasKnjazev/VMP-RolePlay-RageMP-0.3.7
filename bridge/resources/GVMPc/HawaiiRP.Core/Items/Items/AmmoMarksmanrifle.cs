using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Marksmanrifle : Item
    {

        public Marksmanrifle()
        {
            Id = 42;
            Name = "Marksmanrifle Ammo";
            ImagePath = "AmmoMarksmanrifle.png";
            WeightInG = 1200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
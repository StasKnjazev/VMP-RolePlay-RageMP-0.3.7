using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoHeavyShotgun : Item
    {

        public AmmoHeavyShotgun()
        {
            Id = 40;
            Name = "Heavyshotgun Ammo";
            ImagePath = "AmmoHeavyShotgun.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
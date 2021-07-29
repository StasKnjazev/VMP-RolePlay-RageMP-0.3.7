using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoCombatPistol : Item
    {

        public AmmoCombatPistol()
        {
            Id = 36;
            Name = "CombatPistol Ammo";
            ImagePath = "AmmoCombatPistol.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
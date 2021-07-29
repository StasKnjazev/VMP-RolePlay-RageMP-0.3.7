using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class MicroSMG : Item
    {
        public MicroSMG()
        {
            Id = 13;
            Name = "MicroSMG";
            ImagePath = "MicroSMG.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
			try
			{
				WeaponHash hash = WeaponHash.MicroSMG;

				Database.changeInventoryItem(p.Name, "MicroSMG", 1, true);
				p.GiveWeapon(hash, 0);
				Database.addLoadout(p.Name, hash);

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}

			return true;
		}

    }
}

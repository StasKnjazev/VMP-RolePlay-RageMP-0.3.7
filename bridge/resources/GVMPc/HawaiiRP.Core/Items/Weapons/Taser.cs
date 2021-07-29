using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
	class Taser : Item
	{
		public Taser()
		{
			Id = 165;
			Name = "Taser";
			ImagePath = "Taser.png";
			WeightInG = 0;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			try
			{
				WeaponHash hash = WeaponHash.StunGun;

				Database.changeInventoryItem(p.Name, "Taser", 1, true);
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

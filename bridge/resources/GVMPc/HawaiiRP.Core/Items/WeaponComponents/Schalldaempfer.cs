using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Schalldaempfer : Item
	{

		public Schalldaempfer()
		{
			Id = 188;
			Name = "Schalldaempfer";
			ImagePath = "AdvancedRifle-Schalldaempfer.png";
			WeightInG = 200;
			MaxStackSize = 2;
		}

		public override bool getItemFunction(Client p)
		{
			if (p.CurrentWeapon == WeaponHash.AdvancedRifle)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtArSupp);
				p.TriggerEvent("addComponent", p, p.CurrentWeapon, 0x837445AA);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.AssaultRifle)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtArSupp02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.MachinePistol)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtPiSupp);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.MicroSMG)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtArSupp02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.SMG)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtPiSupp);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.Pistol)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AtPiSupp02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			return true;
		}
	}
}

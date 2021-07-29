using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Magazin : Item
	{

		public Magazin()
		{
			Id = 188;
			Name = "Magazin";
			ImagePath = "Magazin.png";
			WeightInG = 200;
			MaxStackSize = 2;
		}

		public override bool getItemFunction(Client p)
		{
			if (p.CurrentWeapon == WeaponHash.AdvancedRifle)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AdvancedRifleClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.AssaultRifle)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.AssaultRifleClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.CompactRifle)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.CompactRifleClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.HeavyPistol)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.HeavyPistolClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.MachinePistol)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.MachinePistolClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.MicroSMG)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.MicroSMGClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.SMG)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.SMGClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.CombatPDW)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.CombatPDWClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			else if (p.CurrentWeapon == WeaponHash.Pistol)
			{
				p.SetWeaponComponent(p.CurrentWeapon, WeaponComponent.PistolClip02);
				Notification.SendPlayerNotifcation(p, "Du hast einen erweiterten Lauf an deine Waffe angefügt!", 5000, "grey", "", "");
			}
			return true;
		}
	}
}

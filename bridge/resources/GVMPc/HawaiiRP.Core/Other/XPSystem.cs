using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	class XPSystem : Script
	{
	    public static void changeLevel(Client p)
		{
			try
			{
				if (p.GetSharedData("XP") > 199)
				{
					Database.changeUserXP(p.Name, 200, true);
					Database.changePlaytime(p.SocialClubName, 1, false);
					p.TriggerEvent("updateXP", 0);
					Notification.SendPlayerNotifcation(p, "Du bist ein Level aufgestiegen. Aktuelles Level: " + Database.getPlaytime(p.SocialClubName).ToString(), 4500, "grey", "VISUM", "");
				}

			} catch(Exception ex)
			{
				Log.Write("Failed to Load XP: " + ex.Message);
			}
		} 

	}
}

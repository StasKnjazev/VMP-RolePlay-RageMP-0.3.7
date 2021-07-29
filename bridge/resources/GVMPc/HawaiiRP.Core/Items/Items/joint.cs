using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class joint : Item
    {

        public joint()
        {
			Id = 159;
            Name = "Joint";
            ImagePath = "joint.png";
            WeightInG = 5;
            MaxStackSize = 32;
        }

        public override bool getItemFunction(Client p)
        {
			NAPI.Player.PlayPlayerAnimation(p, 33, "amb@world_human_smoking_pot@male@base", "base", 8);
			Functions.disableAllPlayerControls(p, true);
			NAPI.Task.Run(delegate
			{
				Functions.disableAllPlayerControls(p, false);
				NAPI.Player.StopPlayerAnimation(p);
				NAPI.Player.SetPlayerArmor(p, (int)Database.getUserArmor(p) + 25);
				Database.setUserArmor(p, (int)Database.getUserArmor(p) + 25);
				p.TriggerEvent("setPlayerDrunk", p, true);
				p.TriggerEvent("startScreenEffect", new object[3]
                {
						"DeathFailOut",
						480000,
						false
                });
				Notification.SendPlayerNotifcation(p, "*skrr* *skrr*", 4500, "green", "", "");
				NAPI.Task.Run(delegate
				{
					p.TriggerEvent("setPlayerDrunk", p, false);
					p.TriggerEvent("stopScreenEffect", "DeathFailOut");
				}, 20000);

			}, 10000);

            return true;
        }
    }
}

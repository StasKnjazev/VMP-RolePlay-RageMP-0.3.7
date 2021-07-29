using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class TaskForceWeste : Item
	{
		public TaskForceWeste()
		{
			Id = 234;
			Name = "TaskForce-Weste";
			ImagePath = "Schutzweste.png";
			WeightInG = 500;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client client)
		{
			if (!client.HasData("PLAYER_ISFARMING"))
			{
				client.TriggerEvent("disableAllPlayerActions", new object[1]
				{
					true
				});
				client.TriggerEvent("sendProgressbar", new object[1]
				{
					4000
				});
				client.SetData("PLAYER_ISFARMING", true);
				NAPI.Player.PlayPlayerAnimation(client, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
				NAPI.Task.Run(delegate
				{
					client.Armor = 100;
					client.TriggerEvent("client:respawning");
					client.ResetData("PLAYER_ISFARMING");
					client.SetClothes(9, 7, 0);
					NAPI.Player.StopPlayerAnimation(client);
					client.TriggerEvent("componentServerEvent", new object[2]
					{
						"Progressbar",
						"StopProgressbar"
					});
					client.TriggerEvent("disableAllPlayerActions", new object[1]
					{
						false
					});
				}, 4000L);
				return true;
			}
			return false;
		}
	}
}

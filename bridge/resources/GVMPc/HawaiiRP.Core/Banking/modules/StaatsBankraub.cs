using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Banking
{
	public class StaatsBankraub : Script
	{
		public static bool robbing = false;

		public static int cooldown = 0;

		public static int count = 0;

		public static int count2 = 0;

		public static bool isDrilling = false;


		[ServerEvent(Event.ResourceStart)]
		public void onResourceSart()
		{
			count = new Random().Next(1, 10);
			count2 = new Random().Next(1, 3);

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(253.2657, 225.4324, 100.7758), 2f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openBankTresor"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Bank auszurauben", "", "grey", 4500));

			Database.changeTresorItem("Staatsbank", "Goldbarren", count, false);
			Database.changeTresorItem("Staatsbank", "Banknoten", count2, false);


			robbing = false;
		}

		[RemoteEvent("openBankTresor")]
		public void openBankTresor(Client p)
		{
			if (robbing == true)
				return;

			if (cooldown > 0)
				return;

			try
			{
				if(Database.getItemCount(p.Name, "Sprengstoff") > 0)
				{
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
						15000
					});
					robbing = true;
					Notification.SendPlayerNotifcation(p, "Die Polizei wurde Informiert", 4500, "red", "", "");
					Notification.SendGlobalNotification("Regierungsnarichten: Die Staatssbank wird vorübergehend als Speerzone deklariert! Das Betreten im Radius 300m+ ist für unbefugte Verboten und wird mit eine Festnahme geahndet werden.", 7000, "grey", Notification.icon.gov);
					NAPI.Task.Run(delegate
					{
						NAPI.Explosion.CreateExplosion((ExplosionType)32, new Vector3(253.2657, 225.4324, 100.7758), 1, 0);
						ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(259.4503, 218.0268, 100.5833), 2f, 2f, 0);
						val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTresor"));
						val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Tresor aufzuknacken", "", "grey", 4500));
						ColShape val3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(266.006, 213.4779, 100.5833), 2f, 2f, 0);
						val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTresor"));
						val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Trsor aufzuknacken", "", "grey", 4500));
						Functions.disableAllPlayerControls(p, false);
						Notification.SendPlayerNotifcation(p, "Du hast den Tresor der Bank gesprengt", 4500, "red", "", "");
						Database.changeInventoryItem(p.Name, "Sprengstoff", 1, true);
						cooldown = 100000;
						p.Position = new Vector3(252.9075, 222.6473, 100.5834).Add(new Vector3(0, 0, 1.5));
						NAPI.Player.StopPlayerAnimation(p);
						NAPI.Task.Run(delegate
						{
							NAPI.ColShape.DeleteColShape(val3);
							NAPI.ColShape.DeleteColShape(val2);
							p.Position = new Vector3(254.4147, 226.4742, 100.7757).Add(new Vector3(0, 0, 1.5));
							NAPI.Task.Run(delegate
							{
								cooldown = 0;
								robbing = false;

							}, cooldown * 6000);

						}, 10 * 30000);

					}, 15000);

				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("openTresor")]
		public void openTresor(Client p)
		{
			if (isDrilling == true)
				return;

			try
			{
				if(Database.getItemCount(p.Name, "Bohrer") > 0)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@fleeca_bank@drilling", "drill_straight_start", 8);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
						7500
					});
					isDrilling = true;
					NAPI.Task.Run(delegate
					{
						Database.changeInventoryItem(p.Name, "Bohrer", 1, true);
						Notification.SendPlayerNotifcation(p, "Du hast die ersten Schließfächer aufgeknackt", 4500, "red", "", "");
						p.SetData("LAST_INVENTORY", "STAATSBANK");
						p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getBankSQLId("Staatsbank") + ",\"Name\":\"Tresor\",\"Money\":0,\"Blackmoney\":0,\"Weight\":35,\"MaxWeight\":70000,\"MaxSlots\":22,\"Slots\":" + NAPI.Util.ToJson(Database.getTresorItems("Staatsbank")) + "}]}");
						Functions.disableAllPlayerControls(p, false);
						NAPI.Player.StopPlayerAnimation(p);

					}, 7500);
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
}

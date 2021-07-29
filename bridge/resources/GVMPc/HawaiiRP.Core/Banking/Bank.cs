using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Banking
{
	class Bank : Script
	{
		public static Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			if (points.Count < 1)
			{
				points.Add("atm1", new Vector3(149.5608, -1040.606, 28.27407));
				points.Add("atm2", new Vector3(248.2893, 222.9457, 105.1867));
				points.Add("atm3", new Vector3(246.5683, 223.6441, 105.1867));
				points.Add("atm4", new Vector3(241.3485, 225.4148, 105.1868));
				points.Add("atm5", new Vector3(243.3157, 224.6606, 105.1868));
				points.Add("atm6", new Vector3(251.9483, 221.5802, 105.1865));
				points.Add("atm7", new Vector3(253.8325, 220.8944, 105.1865));
				points.Add("atm8", new Vector3(-717.6123, -915.7966, 18.11559));
				points.Add("atm9", new Vector3(-56.9174, -1752.176, 28.32101));
				points.Add("atm10", new Vector3(-203.8114, -861.3714, 29.16767));
				points.Add("atm11", new Vector3(-254.5766, -692.2152, 32.50418));
				points.Add("atm12", new Vector3(-1315.786, -834.8016, 15.86173));
				points.Add("atm13", new Vector3(-1314.88, -836.0046, 15.86026));
				points.Add("atm14", new Vector3(-867.6718, -185.9259, 36.73486));
				points.Add("atm15", new Vector3(-283.1263, 6226.108, 30.39329));
				points.Add("atm17", new Vector3(-95.18889, 6457.429, 30.36058));
				points.Add("atm18", new Vector3(-97.30619, 6455.313, 30.36579));
				points.Add("atm19", new Vector3(-113.1297, 6470.229, 30.52671));
				points.Add("atm20", new Vector3(-112.0293, 6469.123, 30.52671));
				points.Add("atm21", new Vector3(-111.0149, 6468.107, 30.52671));
				points.Add("atm22", new Vector3(1967.884, 3743.531, 31.24374));
				points.Add("atm23", new Vector3(-2962.572, 482.8979, 14.60311));
				points.Add("atm24", new Vector3(-2962.637, 481.3663, 14.60677));
				points.Add("atm25", new Vector3(-1095.901, -2825.241, 26.60873));
				points.Add("atm26", new Vector3(-1040.52, -2845.705, 26.6174));


				foreach (KeyValuePair<string, Vector3> point in points)
				{
					ColShape val = NAPI.ColShape.CreateCylinderColShape(point.Value, 1.5f, 1.5f, 0);
					val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openUserBank"));
					val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um auf dein Konto zuzugreifen.", "BANK", "green", 3500));
				}
			}
		}

		[RemoteEvent("openUserBank")]
		public static void openUserBankBank(Client p)
		{
			try
			{
				//p.TriggerEvent("openWindow", "Bank", "{\"Bank\":[{\"playername\":\"" + p.Name +"\",\"money\":" + Database.getMoney(p.Name) + "\",\"balance\":\""+ Database.getUserBank(p.Name) + "}]}");
				p.TriggerEvent("openWindow", new object[] { "Bank", "{\"playername\": \"" + p.Name + "\", \"money\": " + Database.getMoney(p.Name) + ",\"balance\": " + Database.getUserBank(p.Name) + ", \"history\":" + JsonConvert.SerializeObject(new AtmHistory("Jack Hawaii", 5000, DateTime.Now)) +"}" });
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("bankDeposit")]
		public void UserbankEinzahlen(Client p, int value)
		{
			if (value == null)
				return;

			try
			{
				if (Database.getMoney(p.Name) >= value)
				{
					Database.changeMoney(p.Name, value, true);
					Database.changeUserBank(p.Name, value, false);
					Notification.SendPlayerNotifcation(p, "Du hast " + value + "$ auf dein Bankkonto eingezahlt.", 5000, "green", "BANK", "");
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genügend Geld.", 5000, "green", "BANK", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("bankPayout")]
		public void UserbankAbheben(Client p, int value)
		{
			if (value == null)
				return;

			try
			{
				if (Database.getUserBank(p.Name) >= value)
				{

					Database.changeMoney(p.Name, value, false);
					Database.changeUserBank(p.Name, value, true);
					Notification.SendPlayerNotifcation(p, "Du hast " + value + "$ von deinem Konto abgehoben.", 5000, "green", "BANK", "");
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Auf deinem Konto ist nicht genügend Geld.", 5000, "green", "BANK", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("bankTransfer")]
		public void bankTransfer(Client p, int amount, string name)
		{
			try
			{
				if(Database.getUserBank(p.Name) >= amount)
				{
					Database.changeUserBank(p.Name, amount, true);
					Database.changeUserBank(name, amount, false);
					Client target = Database.getPlayerFromName(name);
					Notification.SendPlayerNotifcation(p, "Du hast " + amount + " an " + name + " versendet", 4500, "green", "BANK", "");
					Notification.SendPlayerNotifcation(target, "Du hast " + amount + " von " + p.Name + " erhalten", 4500, "green", "BANK", "");
				} else
				{
					Notification.SendPlayerNotifcation(p, "Auf deinem Konto ist nicht genügend Geld.", 5000, "green", "BANK", "");
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	class PoliceAktenSearchApp : Script
	{
		public static List<CategoryModel> categories = new List<CategoryModel>();

		public static List<ReasonModel> reasons = new List<ReasonModel>();

		public static List<AktenModel> akten = new List<AktenModel>();

		[RemoteEvent("requestPlayerResults")]
		public void requestPlayerResults(Client p, string query)
		{
			if (query == "")
				return;

			object JSOnobject = new
			{
				playerName = query
			};

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceAktenSearchApp",
				"responsePlayerResults",
				NAPI.Util.ToJson(JSOnobject)
			});
		}

		[RemoteEvent("savePersonData")]
		public void savePersonData(Client p, string name, string address, string memberShip, int phonenumber, string info)
		{
			Database.createPlayerInfo(name, address, memberShip, phonenumber, info);
			Notification.SendPlayerNotifcation(p, "Neue Personeninfo angelegt", 5000, "brown", "AKTEN", "");
		}

		[RemoteEvent("requestPersonData")]
		public void requestPersonData(Client p, string name)
		{
			List<PersonDataModel> personData = new List<PersonDataModel>();

			personData.Add(new PersonDataModel(name, "Vinewood Street 1", "LSPD", 74293, "Test"));

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPersonApp",
				"responsePersonData",
				JsonConvert.SerializeObject(personData)
			});
		}

		[RemoteEvent("requestWantedCategories")]
		public void requestWantedCategories(Client p)
		{
			categories.Add(new CategoryModel("Geschwindigkeitsüberschreitungen", 0));
			categories.Add(new CategoryModel("Lizenzverstösse", 1));
			categories.Add(new CategoryModel("Normaler Straßenverkehr", 2));
			categories.Add(new CategoryModel("Luftverkehr", 3));
			categories.Add(new CategoryModel("Drogendelikte", 4));
			categories.Add(new CategoryModel("Wirtschaftskriminalität", 5));
			categories.Add(new CategoryModel("Waffendelikte", 6));
			categories.Add(new CategoryModel("Körperliche Integrität", 7));
			categories.Add(new CategoryModel("Umgang mit Beamten", 8));

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditWantedsApp",
				"responseCategories",
				NAPI.Util.ToJson(categories)
			});
		}

		[RemoteEvent("requestCategoryReasons")]
		public void requestCategoryReasons(Client p, int id)
		{
			reasons.Add(new ReasonModel("Rufmord", 0, 3500, 30));
			reasons.Add(new ReasonModel("Fahrlässige Tötung", 1, 3500, 30));
			reasons.Add(new ReasonModel("Tötung eines Beamten", 2, 3500, 30));

			if (id == 0)
			{
				p.TriggerEvent("componentServerEvent", new object[3]
				{
					"PoliceEditWantedsApp",
					"responseCategoryReasons",
					JsonConvert.SerializeObject(reasons)
				});
			}
		}

		[RemoteEvent("addPlayerWanteds")]
		public void addPlayerWanteds(Client p, string name)
		{
			foreach(Client c in NAPI.Pools.GetAllPlayers())
			{
				if(c.GetSharedData("FRAKTION") == "Los Santos Police Department")
				{
					Notification.SendPlayerNotifcation(c, "Die Akte von " + name + " wurde von " + p.Name + " bearbeitet", 5000, "blue", "AKTEN", "");
					requestOpenCrimes(p, name);
				}
			}
		}

		[RemoteEvent("requestJailTime")]
		public void requestJailTime(Client p, string name)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPersonApp",
				"responseJailTime",
				JsonConvert.SerializeObject(Database.getUserJailtime(name))
			});
		}

		[RemoteEvent("requestOpenCrimes")]
		public void requestOpenCrimes(Client p, string name)
		{
			List<ReasonModel> reasons = Database.GetUserWanteds(name);

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPersonApp",
				"responseOpenCrimes",
				NAPI.Util.ToJson(reasons)
			});
			Log.Write(reasons + "");
		}

		[RemoteEvent("requestJailTime")]
		public void responseJailTime(Client p, string name)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPersonApp",
				"responseJailTime",
				NAPI.Util.ToJson((int)Database.getWantedsJailTime(name))
			});
		}

		/*[RemoteEvent("requestLicenses")]
		public void requestLicenses(Client p, string name)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPersonApp",
				"responseLicenses",
				"[{\"license\":\"Waffenschein\"}]"
			});
		} */

		[RemoteEvent("requestAkte")]
		public void savePersonAkte(Client p, string name)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"PoliceEditPerson",
				"responseAktenList",
				"{\"title\":\"Jack\",\"created\":\"Heute\"}"
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	public class FraktionListApp : Script
	{
		[RemoteEvent("requestFraktionMembers")]
		public void requestFraktionMembers(Client p)
		{
			List<FraktionListModel> frakMembers = new List<FraktionListModel>();

         	try
			{
				foreach (FraktionListModel fraktionList in Database.getFraktionOnlinePlayer(Database.getPlayerFraktion(p.Name)))
				{
					if (Database.getUserFraktionRank(p.Name) >= 10)
					{
						frakMembers.Add(new FraktionListModel(fraktionList.rang, fraktionList.title, fraktionList.name, fraktionList.payday, true));
					}
					else
					{

						frakMembers.Add(new FraktionListModel(fraktionList.rang, fraktionList.title, fraktionList.name, fraktionList.payday, false));
					}
				}

				object JSONobject = new
				{
					list = frakMembers,
					manage = true,
					hasDuty = true
				};

				p.TriggerEvent("componentServerEvent", new object[3]
				{
				"FraktionListApp",
				"responseMembers",
				JsonConvert.SerializeObject(JSONobject)
				});
			} catch (Exception e)
			{
				Log.Write(e.ToString());
			}

			Log.Write("Fraktionsmember geladen");
		}
	}
}

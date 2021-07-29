/*using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Ipad
{
	public class BusinessDetailApp : Script
	{
		[RemoteEvent("requestBusinessDetail")]
		public void requestBusinessDetail(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"BusinessDetailApp",
				"responseBusinessDetailLinks",
				"{\"links\":\"Mitglieder\"}"
			});
		}

		[RemoteEvent("requestBusinessDetailMembers")]
		public void requestBusinessDetailMembers(Client p)
		{
			List<object> teamMemberList = new List<object>();

			foreach(Handy.BusinessPlayer businessPlayer in Database.getBusinessOnlinePlayers(p.Name))
			{
				teamMemberList.Add(new
				{
					name = businessPlayer.businessName,
					salary = 0
				});
			}

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"BusinessDetailApp",
				"responseBusinessDetail",
				NAPI.Util.ToJson(teamMemberList)
			});
		}
	}
} */

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using GTANetworkAPI;

namespace GVMPc.Handy
{
	public class HitmanApp : Script
	{
		[RemoteEvent("requestHitmanContracts")]
		public void requestHitmanContracts(Client c)
		{
			try
			{
				c.TriggerEvent("componentServerEvent", new object[3]
				{
					"HitmanContractListApp",
					"responseHitmanContracts",
					"[{\"id\":\"1\",\"target\":\"Struppy\",\"details\":\"Du Hurensohn\",\"phone\":\"696969\",\"bounty\":\"2000\"}]"
				});
			} catch (Exception e)
			{
				Log.Write(e.ToString());
			}
		}
	}
}

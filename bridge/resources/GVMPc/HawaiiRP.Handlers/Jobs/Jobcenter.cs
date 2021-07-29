using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Jobs
{
	class Jobcenter : Script
	{
		public static Vector3 jobcenter = new Vector3(-555.5869, -618.5714, 33.57631);

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(269, jobcenter, 1f, 1, "Arbeitsamt", 255, 0, true, 0, 0);
			NAPI.Marker.CreateMarker(1, jobcenter, new Vector3(), new Vector3(), 1f, new Color(144, 255, 0), false, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(jobcenter, 2f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openJobCenter"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um dir einen Job zu suchen", "JOBCENTER", "red", 4500));
		}

		[RemoteEvent("openJobCenter")]
		public void openJobCenter(Client p)
		{

			//p.TriggerEvent("openWindow", "MethLabor", NAPI.Util.ToJson());

			try
			{
				NativeMenu nativeMenu = new NativeMenu("Arbeitsamt", "Angebote", new List<NativeItem>()
				{
					new NativeItem("Müllmann", "trasher"),
					new NativeItem("Minenarbeiter", "miner"),
					new NativeItem("LKW Transporter", "lkw"),
					new NativeItem("Öl Verarbeiter", "oil")
				});
				nativeMenu.showNativeMenu(p);

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			} 
		}

		[RemoteEvent("nM-Arbeitsamt")]
		public void arbeitsamt(Client p, string selection)
		{
			if(selection == "trasher")
			{
				Database.setUserJob(p.Name, "Müllmann");
				Notification.SendPlayerNotifcation(p, "Du hast den Job Müllmann gewählt", 4500, "red", "JOBCENTER", "");
			} else if(selection == "miner")
			{
				Database.setUserJob(p.Name, "Minenarbeiter");
				Notification.SendPlayerNotifcation(p, "Du hast den Job Minenarbeiter gewählt", 4500, "red", "JOBCENTER", "");
			} else if(selection == "lkw")
			{
				Database.setUserJob(p.Name, "LKWFahrer");
				Notification.SendPlayerNotifcation(p, "Du hast den Job LKW Fahrer gewählt", 4500, "red", "JOBCENTER", "");
			} else if(selection == "oil")
			{
				Database.setUserJob(p.Name, "Oilverarbeiter");
				Notification.SendPlayerNotifcation(p, "Du hast den Job Öl Verarbeiter gewählt", 4500, "red", "JOBCENTER", "");
			}
		}

	}
}

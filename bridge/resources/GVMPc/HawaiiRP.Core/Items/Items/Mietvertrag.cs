using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Mietvertrag : Item
	{

		public Mietvertrag()
		{
			Id = 191;
			Name = "Mietvertrag";
			ImagePath = "leasing.png";
			WeightInG = 20;
			MaxStackSize = 2;
		}

		public override bool getItemFunction(Client p)
		{
			p.TriggerEvent("openWindow", "TextInputBox",
	                "{\"textBoxObject\":{\"Title\":\"Mietvertrag | KFZ\",\"Message\":\"Gebe den Namen des Mieters ein.\",\"Callback\":\"CREATE_LEASING\"}}"
            );
			p.TriggerEvent("componentReady", "TextInputBox");

			return true;
		}

		[RemoteEvent("CREATE_LEASING")]
		public void CREATE_LEASING(Client p, string name)
		{
			if (!name.Contains("_"))
				p.SendNotification("Im Namen muss ein _ vorhanden sein");

			Client target = Database.getPlayerFromName(name);

			target.TriggerEvent("openWindow", new object[2]
			{
				"Confirmation",
				"{\"confirmationObject\":{\"Title\":\"KFZ Mietvertrag | " + p.Name +"\",\"Message\":\"Möchtest du den Mietvertrag von " + p.Name +" annehmen?\",\"Callback\":\"acceptLeasing\",\"Arg1\":\"" + p.Name + "\",\"Arg2\":\"\"}}"
			});
		}

		[RemoteEvent("acceptLeasing")]
		public void acceptLeasing(Client p, string name)
		{
			Notification.SendPlayerNotifcation(p, "Du hast einen Mietvertrag unterschrieben!", 5000, "grey", "Mietvertrag | KFZ", "");
			Ipad.KFZRentApp.renters.Add(new Ipad.RentModel(name, (int)Database.getUserPhoneNumber(name), "BMW M8", "100% Tune"));
		}
	}
}

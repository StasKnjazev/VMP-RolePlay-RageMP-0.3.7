using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Handy
{
	class ContactsApp : Script
	{
		public static List<object> contacts = new List<object>();

		[RemoteEvent("requestPhoneContacts")]
		public void requestPhoneContacts(Client p)
		{
			p.TriggerEvent("responsePhoneContacts", NAPI.Util.ToJson((object)Database.getUserContacts(p.Name)));
			p.TriggerEvent("componentReady", "ContactsApp");
		}

		[RemoteEvent("addPhoneContact")]
		public void addPhoneContact(Client p, string name, int number)
		{
			Notification.SendPlayerNotifcation(p, "Kontakt eingespeichert", 5000, "grey", "KONTAKTE", "");
			Database.changeUserContact(p.Name, name, number, false, false);
		}

		[RemoteEvent("delPhoneContact")]
		public void delPhoneContact(Client p, int phonenumber) 
		{
			Database.changeUserContact(p.Name, "", phonenumber, false, true);
			Notification.SendPlayerNotifcation(p, "Kontakt gelöscht", 5000, "grey", "KONTAKTE", "");
		}
	}
}

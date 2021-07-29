using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Laptop
{
	public class SupportApp : Script
	{
		public static List<Laptop.openTicket> openTickets = new List<Laptop.openTicket>();
		public static List<Laptop.Ticket> tickets = new List<Laptop.Ticket>();
		public static void addTicket(Client p, string des)
		{
			//string time = DateTime.Now;
			Random r = new Random();
			openTickets.Add(new Laptop.openTicket(p.Name, des, DateTime.Now, r.Next(1, 9999)));
		}

		[RemoteEvent("requestOpenSupportTickets")]
		public void requestOpenSupportTickets(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3] {
				"SupportOpenTickets",
				"responseOpenTicketList",
				NAPI.Util.ToJson(openTickets)
			});
		}

		[RemoteEvent("acceptOpenSupportTicket")]
		public void acceptOpenSupportTicket(Client p, string creator)
		{
			Client target = Database.getPlayerFromName(creator);

			foreach (Laptop.openTicket Tickets in openTickets.ToArray())
			{
				if (Tickets.creator == creator)
				{
					tickets.Add(new Laptop.Ticket(creator, Tickets.text, Tickets.id, Tickets.created_at));
					openTickets.Remove(Tickets);
					Notification.SendPlayerNotifcation(target, "Dein Ticket wird nun von " + Database.getPlayerRank(p.Name).rankName + " " +p.Name + " bearbeitet", 5000, "red", "SUPPORT", "");
					Notification.SendPlayerNotifcation(p, "Du bearbeitest nun das Ticket von " + creator, 5000, "red", "SUPPORT", "");
				}
			}
		}
		[RemoteEvent("requestAcceptedTickets")]
		public void requestAcceptedTickets(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3] {
				"SupportAcceptedTickets",
				"responseAcceptedTicketList",
				NAPI.Util.ToJson(tickets)
			});
		}

		[RemoteEvent("closeTicket")]
		public void closeTicket(Client p, string creator)
		{
			Client target = Database.getPlayerFromName(creator);

			foreach (Laptop.Ticket Tickets in tickets.ToArray())
			{
				if (Tickets.creator == creator)
				{
					tickets.Remove(Tickets);
					Notification.SendPlayerNotifcation(target, "Dein Ticket wird nun von " + Database.getPlayerRank(p.Name).rankName + " " + p.Name + " geschlossen", 5000, "red", "SUPPORT", "");
					Notification.SendPlayerNotifcation(p, "Du schließt nun das Ticket von " + creator, 5000, "red", "SUPPORT", "");
				}
			}
		}

		[RemoteEvent("supportTeleportToPlayer")]
		public void supportTeleportToPlayer(Client p, string name)
		{
			Client target = NAPI.Player.GetPlayerFromName(name);
			if (target == null) return;
			Anticheat.Wait(p); p.Position = target.Position;
			Notification.SendPlayerNotifcation(p, "Du hast dich zu " + target.Name + " teleportiert!", 5000, "red", "Support", "");
			Notification.SendPlayerNotifcation(target, Database.getPlayerRank(p.Name).rankName + " " + p.Name + " hat sich zu dir teleportiert!", 5000, "red", "Support", "");
			p.TriggerEvent("closeIpad");
		}
	}
}

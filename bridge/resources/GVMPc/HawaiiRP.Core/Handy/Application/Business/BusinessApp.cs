using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Handy
{
	class BusinessApp : Script
	{
		[RemoteEvent("leaveBusiness")]
		public void leaveBusiness(Client p)
		{
			try
			{
				p.SetSharedData("BUSINESS", "null");
				p.SetSharedData("BUSINESS_RANK", 0);
				Database.setUserBusiness(p.Name, "null");
				Database.setUserBusinessRank(p.Name, 0);
				Notification.SendPlayerNotifcation(p, "Du hast den Business verlassen", 4500, "white", "BUSINESS", "");

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("editBusinessMember")]
		public void editBusinessMember(Client p, string name, int newrank)
		{
			try
			{
				if (p.GetSharedData("BUSINESS_RANK") > 9)
				{
					if (Database.isUserExists(name))
					{
						if (Database.getPlayerBusiness(name) == p.GetSharedData("BUSINESS"))
						{
							if (Database.getUserBusinessRank(name) < p.GetSharedData("BUSINESS_RANK"))
							{
								Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " auf Rang " + newrank + " gestuft.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
								Database.setUserBusinessRank(name, newrank);
								Client target = Database.getPlayerFromName(name);
								if (target != null)
								{
									Notification.SendPlayerNotifcation(target, "Dein Rang wurde auf " + newrank + " gestuft.", 5000, "white", target.GetSharedData("BUSINESS"), "white");
								}
							}
							else
							{
								Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung, um die Rechte für diesen Spieler zu verändern.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
							}
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht in deinem Business.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
					}

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("addPlayerToBusiness")]
		public void addPlayerToBusiness(Client p, string name)
		{
			try
			{
				if (p.GetSharedData("BUSINESS_RANK") > 9)
				{
					if (Database.isUserExists(name))
					{
						if (Database.getPlayerBusiness(name) == p.GetSharedData("BUSINESS"))
						{
							Notification.SendPlayerNotifcation(p, "Der Spieler ist bereits in deinem Business.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
						}
						else
						{
							if (Database.getPlayerBusiness(name) == "null")
							{
								Client target = Database.getPlayerFromName(name);
								if (target != null)
								{
									target.TriggerEvent("openWindow", new object[2]
										{
											"Confirmation",
											"{\"confirmationObject\":{\"Title\":\"" + p.GetSharedData("BUSINESS") + "\",\"Message\":\"Möchtest du die Einladung von " + p.Name + " annehmen?\",\"Callback\":\"acceptInvation\",\"Arg1\":\"" + p.GetSharedData("BUSINESS") + "\",\"Arg2\":\"\"}}"
										});

									// Dialogs.sendPlayerDialog(target, p.GetSharedData("FRAKTION"), "Möchtest du die Einladung von " + p.Name + " annehmen?", "phone:joinfrak", true, p.GetSharedData("FRAKTION"), "white");
									Notification.SendPlayerNotifcation(p, "Du hast " + name + " eine Einladung gesendet.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
								}
								else
								{
									Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht online.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
								}
							}
							else
							{
								Notification.SendPlayerNotifcation(p, "Dieser Spieler ist bereits in einer Business.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
							}
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
					}

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("acceptInvation")]
		public void PhoneJoinfrak(Client p, string frak, object unused)
		{
			try
			{
				p.SetSharedData("BUSINESS", frak);
				p.SetSharedData("BUSINESS_RANK", 0);
				Database.setUserBusiness(p.Name, frak);
				Database.setUserBusinessRank(p.Name, 0);

				foreach (Client target in NAPI.Pools.GetAllPlayers())
				{
					if (target.HasSharedData("BUSINESS"))
					{
						if (target.GetSharedData("BUSINESS") == p.GetSharedData("BUSINESS"))
						{
							Notification.SendPlayerNotifcation(target, "Der Spieler " + p.Name + " hat den Business betreten.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
						}
					}
				}

				Notification.SendPlayerNotifcation(p, "Du bist dem Business " + frak + " beigetreten.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("kickBusinessMember")]
		public void kickBusinessMember(Client p, string name)
		{
			try
			{
				if (p.GetSharedData("BUSINESS_RANK") > 9)
				{
					if (Database.isUserExists(name))
					{
						if (Database.getPlayerBusiness(name) == p.GetSharedData("BUSINESS"))
						{
							if (Database.getUserBusinessRank(name) < p.GetSharedData("BUSINESS_RANK"))
							{
								Database.setUserBusiness(name, "null");
								Database.setUserBusinessRank(name, 0);
								Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " uninvited.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
								Client target = Database.getPlayerFromName(name);
								if (target != null)
								{
									target.SetSharedData("BUSINESS", "null");
									target.SetSharedData("BUSINESS_RANK", 0);
									Notification.SendPlayerNotifcation(target, "Du wurdest aus der Business " + p.GetSharedData("BUSINESS") + " gekickt.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
								}
							}
							else
							{
								Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung, um diesen Spieler zu uninviten.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
							}

						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht in deiner Business.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
					}

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("BUSINESS"), "white");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("saveBusinessMOTD")]
		public void saveBusinessMotd(Client p, string motd)
		{
			try
			{
				if (p.GetSharedData("BUSINESS_RANK") > 9)
				{
					Database.setBusinessMOTD(p.GetSharedData("BUSINESS"), motd);
					Notification.SendPlayerNotifcation(p, "Du hast den MOTD erfolgreich geändert", 4500, "green", "BUSINESS", "");
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung dazu", 4500, "white", "BUSINESS", "");
				}

				object JSONobject = new
				{
					motd = motd
				};

				p.TriggerEvent("componentServerEvent", new object[3]
				{
					"BusinessListApp",
					"responseBusinessMOTD",
					NAPI.Util.ToJson(JSONobject)
				});


			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

	}
}

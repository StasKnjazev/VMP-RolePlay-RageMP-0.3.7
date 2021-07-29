using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Tuner
{
	public class TunerRegister : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(72, new Vector3(-338.1855, -136.1761, 37.90961), 1f, 0, "Los Santos Customs", 255, 0, true, 0, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-338.1855, -136.1761, 37.90961), 4f, 2, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTuner"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Tuner zu öffnen", "TUNER", "white", 4500));
		}

		[RemoteEvent("openTuner")]
		public void openTuner(Client p)
		{
			try
			{
				if (p.IsInVehicle)
				{
					NativeMenu nativeMenu = new NativeMenu("Tuner", "Angebote", new List<NativeItem>()
				    {
					new NativeItem("Farbe", "color"),
					new NativeItem("Sekundäre Farbe", "secondcolor"),
					new NativeItem("Motor - EMS", "ems"),
					new NativeItem("Turbo", "turbo"),
					new NativeItem("Bremsen", "brakes"),
					new NativeItem("Felgen", "trails"),
					new NativeItem("Fensterscheiben", "glasses"),
					new NativeItem("Beleuchtung", "lights"),
					new NativeItem("Hupe", "horn"),
					new NativeItem("Front Lichter", "xenon")
				    });
					nativeMenu.showNativeMenu(p);
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("nM-Tuner")]
		public void tuner(Client p, string selection)
		{
			try
			{
				if(selection == "color")
				{
					NativeMenu nativeMenu = new NativeMenu("Farben", "Angebote", new List<NativeItem>()
					{
						new NativeItem("Schwarz - 7500$", "black"),
						new NativeItem("Gelb - 7500$", "yellow"),
						new NativeItem("Grün - 7500$", "green"),
						new NativeItem("Lila- 7500$", "purple"),
						new NativeItem("Blau - 7500$", "blue"),
						new NativeItem("Rot - 7500$", "red"),
						new NativeItem("Weiß - 7500$", "white"),
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "secondcolor")
				{
					NativeMenu nativeMenu = new NativeMenu("SekundäreFarben", "Angebote", new List<NativeItem>()
					{
						new NativeItem("Schwarz - 7500$", "black"),
						new NativeItem("Gelb - 7500$", "yellow"),
						new NativeItem("Grün - 7500$", "green"),
						new NativeItem("Lila- 7500$", "purple"),
						new NativeItem("Blau - 7500$", "blue"),
						new NativeItem("Rot - 7500$", "red"),
						new NativeItem("Weiß - 7500$", "white"),
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "ems")
				{
					NativeMenu nativeMenu = new NativeMenu("Motor", "Angebote", new List<NativeItem>()
					{
						new NativeItem("EMS Verbesserung 1 - 15000$", "ems1"),
						new NativeItem("EMS Verbesserung 2 - 30000$", "ems2"),
						new NativeItem("EMS Verbesserung 3 - 45000$", "ems3"),
						new NativeItem("EMS Verbesserung 4 - 60000$", "ems4")
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "turbo")
				{
					NativeMenu nativeMenu = new NativeMenu("Turbo", "Angebote", new List<NativeItem>()
					{
						new NativeItem("EMS Verbesserung 1 - 50000$", "turbo1"),
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "brakes")
				{
					NativeMenu nativeMenu = new NativeMenu("Bremsen", "Angebote", new List<NativeItem>()
					{
						new NativeItem("Bremsen Upgrade 1 - 8000$", "brakes1"),
						new NativeItem("Bremsen Upgrade 2 - 16000$", "brakes2"),
						new NativeItem("Bremsen Upgrade 3 - 24000$", "brakes3")
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "horn")
				{
					NativeMenu nativeMenu = new NativeMenu("Hupe", "Angebote", new List<NativeItem>()
					{
						new NativeItem("Hupe 1", "horn1"),
						new NativeItem("Hupe 2", "horn2"),
						new NativeItem("Hupe 3", "horn3"),
						new NativeItem("Hupe 4", "horn4"),
						new NativeItem("Hupe 5", "horn5"),
						new NativeItem("Hupe 6", "horn6")
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "trails")
				{
					NativeMenu nativeMenu = new NativeMenu("Felgen", "Angebote", new List<NativeItem>()
					{
					});
					nativeMenu.showNativeMenu(p);

				} else if(selection == "lights")
				{
					NativeMenu nativeMenu = new NativeMenu("Lichter", "Angebote", new List<NativeItem>()
					{
						new NativeItem("Gelbe Unterbodenbeleuchtung - 20000$", "yellowlight"),
						new NativeItem("Grüne Unterbodenbeleuchtung - 20000$", "greenlight"),
						new NativeItem("Blaue Unterbodenbeleuchtung - 20000$", "bluelight"),
						new NativeItem("Rote Unterbodenbeleuchtung - 20000$", "redlight"),
						new NativeItem("Weiße Unterbodenbeleuchtung - 20000$", "whitelight"),
						new NativeItem("Lilane Unterbodenbeleuchtung - 20000$", "purplelight"),
						new NativeItem("Schwarze Unterbodenbeleuchtung - 20000$", "blacklight"),

					});
					nativeMenu.showNativeMenu(p);
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("nM-Farben")]
		public void farben(Client p, string selection)
		{
			Vehicle veh = p.Vehicle;

			if(selection == "black")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Schwarz gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(0, 0, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 0;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "yellow")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Gelb gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(255, 255, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 89;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "green")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Grün gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(0, 255, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 55;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "purple")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Lila gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(102, 0, 102));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 145;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "blue")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Blau gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(0, 128, 255));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 70;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "red")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Rot gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(255, 0, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 28;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "white")
			{
				if (Database.getMoney(p.Name) > 7499)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Weiß gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehiclePrimaryColor(veh.NumberPlate, new Color(255, 255, 255));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.PrimaryColor = 111;

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}
			}

		}

		[RemoteEvent("nM-SekundäreFarben")]
		public void secondColor(Client p, string selection)
		{
			Vehicle veh = p.Vehicle;

			if (selection == "black")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Schwarz gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(0, 0, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.SecondaryColor = 0;

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			}
			else if (selection == "yellow")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Gelb gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(255, 255, 0));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.SecondaryColor = 89;

				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			}
			else if (selection == "green")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Grün gefärbt", 4500, "black", "TUNER", "");
					Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(0, 255, 0));
					Database.changeMoney(p.Name, 7500, true);
					veh.SecondaryColor = 55;

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genung Geld dabei", 4500, "black", "TUNER", "");
				}


			}
			else if (selection == "purple")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Lila gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(102, 0, 102));
				    Database.changeMoney(p.Name, 7500, true);
				    veh.SecondaryColor = 145;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			}
			else if (selection == "blue")
			{
				Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Blau gefärbt", 4500, "black", "TUNER", "");
				Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(0, 128, 255));
				Database.changeMoney(p.Name, 7500, true);
				veh.SecondaryColor = 70;

			}
			else if (selection == "red")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Rot gefärbt", 4500, "black", "TUNER", "");
				    Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(255, 0, 0));
					Database.changeMoney(p.Name, 7500, true);
					veh.SecondaryColor = 28;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			}
			else if (selection == "white")
			{
				if (Database.getMoney(p.Name) > 7499)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug für 7500$ Weiß gefärbt", 4500, "black", "TUNER", "");
					Database.changeVehicleSecondaryColor(veh.NumberPlate, new Color(255, 255, 255));
					veh.SecondaryColor = 111;
					Database.changeMoney(p.Name, 7500, true);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}
			}
		}

		[RemoteEvent("nM-Motor")]
		public void motor(Client p, string selection)
		{
			Vehicle veh = p.Vehicle;

			if(selection == "ems1")
			{
				if (Database.getMoney(p.Name) > 14999)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dir für 15000$ Motor Upgrade 1 gekauft", 4500, "black", "TUNER", "");
					veh.SetMod(11, 0);
					Database.changeMoney(p.Name, 15000, true);
					if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						Database.changeVehicleTuning(veh.NumberPlate, 11, 0);
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "ems2")
			{
				if (Database.getMoney(p.Name) > 29999)
				{

					Notification.SendPlayerNotifcation(p, "Du hast dir für 30000$ Motor Upgrade 2 gekauft", 4500, "black", "TUNER", "");
					veh.SetMod(11, 1);
					Database.changeMoney(p.Name, 30000, true);
					if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						Database.changeVehicleTuning(veh.NumberPlate, 11, 1);
					}

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "ems3")
			{
				if (Database.getMoney(p.Name) > 44999)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir für 45000$ Motor Upgrade 3 gekauft", 4500, "black", "TUNER", "");
					veh.SetMod(11, 2);
					Database.changeMoney(p.Name, 45000, true);
					if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						Database.changeVehicleTuning(veh.NumberPlate, 11, 2);
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}

			} else if(selection == "ems4")
			{
				if (Database.getMoney(p.Name) > 59999)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir für 60000$ Motor Upgrade 4 gekauft", 4500, "black", "TUNER", "");
					veh.SetMod(11, 2);
					Database.changeMoney(p.Name, 60000, true);
					if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						Database.changeVehicleTuning(veh.NumberPlate, 11, 2);
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}
			}
		}

		[RemoteEvent("nM-Turbo")]
		public void turbo(Client p, string selection)
		{
			Vehicle veh = p.Vehicle;

			if(selection == "turbo1")
			{
				if(Database.getMoney(p.Name) > 49999)
				{
					if(Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						Database.changeVehicleTuning(veh.NumberPlate, 18, 0);
						veh.SetMod(18, 0);
						Notification.SendPlayerNotifcation(p, "Du hast für 50000$ Turbo Upgrade 1 gekauft", 4500, "black", "TUNER", "");
						Database.changeMoney(p.Name, 50000, true);
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
				}
			}
		}

		[RemoteEvent("nM-Bremsen")]
		public void bremsen(Client p, string selection)
		{
			Vehicle veh = p.Vehicle;

			try
			{
				if(selection == "brakes1")
				{
					if(Database.getMoney(p.Name) > 7999)
					{
						if(Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
						{
							Database.changeVehicleTuning(veh.NumberPlate, 12, 0);
							veh.SetMod(12, 0);
							Database.changeMoney(p.Name, 8000, true);
							Notification.SendPlayerNotifcation(p, "Du hast für 8000$ deine Bremsen verbessert", 4500, "black", "TUNER", "");

						} else
						{
							Notification.SendPlayerNotifcation(p, "Dies ist nicht dein Fahrzeug", 4500, "black", "TUNER", "");
						}
					} else
					{
						Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
					}
				}
				else if (selection == "brakes2")
				{
					if (Database.getMoney(p.Name) > 15999)
					{
						if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
						{
							Database.changeVehicleTuning(veh.NumberPlate, 12, 1);
							veh.SetMod(12, 1);
							Database.changeMoney(p.Name, 16000, true);
							Notification.SendPlayerNotifcation(p, "Du hast für 16000$ deine Bremsen verbessert", 4500, "black", "TUNER", "");

						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Dies ist nicht dein Fahrzeug", 4500, "black", "TUNER", "");
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
					}
				}
				else if (selection == "brakes3")
				{
					if (Database.getMoney(p.Name) > 23999)
					{
						if (Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
						{
							Database.changeMoney(p.Name, 24000, true);
							Database.changeVehicleTuning(veh.NumberPlate, 12, 2);
							veh.SetMod(12, 2);
							Notification.SendPlayerNotifcation(p, "Du hast für 24000$ deine Bremsen verbessert", 4500, "black", "TUNER", "");

						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Dies ist nicht dein Fahrzeug", 4500, "black", "TUNER", "");
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld", 4500, "black", "TUNER", "");
					}
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}


	}
}

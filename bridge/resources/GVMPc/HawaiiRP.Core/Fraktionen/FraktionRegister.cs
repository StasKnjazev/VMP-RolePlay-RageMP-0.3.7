using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.ClothingShops;
using GVMPc.Menus;

namespace GVMPc.Fraktionen
{
    public class FraktionRegister : Script
    {
        public static List<Fraktion> fraktionList = new List<Fraktion>();
        public static Vector3 fraklagerPos = new Vector3(1719.57, 4765.67, 14.21);
		public static Vector3 laborPos = new Vector3(996.879, -3200.482, -37.49374);
        public static int GangwarDimension = 8888;

        [ServerEvent(Event.ResourceStart)]
        public void registerFraktions()
        {
            if (fraktionList.Count < 1)
            {
                foreach (Fraktion fraktion in Database.getFraktionList())
                    fraktionList.Add(fraktion);
            }

            NAPI.Marker.CreateMarker(1, new Vector3(1706.0469, 4764.2236, 13.110968), new Vector3(), new Vector3(), 1.0f, new Color(255, 140, 0));
            NAPI.Marker.CreateMarker(1, new Vector3(1706.4445, 4769.4775, 13.110967), new Vector3(), new Vector3(), 1.0f, new Color(255, 140, 0));
            NAPI.Marker.CreateMarker(1, new Vector3(1706.3625, 4774.654, 13.110966), new Vector3(), new Vector3(), 1.0f, new Color(255, 140, 0));
			//NAPI.Marker.CreateMarker(1, new Vector3(996.879, -3200.482, -37.49374), new Vector3(), new Vector3(), 1f, new Color(255, 140, 0));

            ColShape val3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1706.4445, 4769.4775, 13.110967), 1.4f, 1.4f, uint.MaxValue);
            val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("openWaffenschrank"));
            val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um den Waffenschrank zu öffnen.", "WAFFENSCHRANK", "white", 5000));

            ColShape val4 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1706.3625, 4774.654, 13.110966), 1.4f, 1.4f, uint.MaxValue);
            val4.SetData("COLSHAPE_FUNCTION", new FunctionModel("openFraktionsKleiderschrank"));
            val4.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um den Fraktionskleiderschrank zu öffnen.", "KLEIDERSCHRANK", "white", 5000));

            ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1706.0469, 4764.2236, 13.110968), 1.4f, 1.4f, uint.MaxValue);
            val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("enterGangwarDimension"));
            val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um die Gangwar Dimension zu betreten / verlassen.", "GANGWAR", "white", 5000));

            ColShape val = NAPI.ColShape.CreateCylinderColShape(fraklagerPos, 2.4f, 1.5f, uint.MaxValue);
            val.SetData("COLSHAPE_FUNCTION", new FunctionModel("exitFraklager"));
            val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um das Fraklager zu verlassen.", "FRAKTIONSLAGER", "white", 5000));

		    /*ColShape val5 = NAPI.ColShape.CreateCylinderColShape(new Vector3(996.879, -3200.482, -37.49374), 2.4f, 2.4f, uint.MaxValue);
			val5.SetData("COLSHAPE_FUNCTION", new FunctionModel("exitFrakLabor"));
			val5.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um das Labor zu verlassen.", "LABOR", "white", 5000)); */

			foreach (Fraktion fraktion in fraktionList)
                loadFraktion(fraktion);

        }

        public void loadFraktion(Fraktion fraktion)
        {
            NAPI.Marker.CreateMarker(1, fraktion.fraktionsLagerPoint, new Vector3(), new Vector3(), 1f, new Color(0, 0, 0));
            NAPI.Marker.CreateMarker(1, fraktion.garagePoint, new Vector3(), new Vector3(), 1f, new Color(255, 140, 0));
			NAPI.Marker.CreateMarker(1, fraktion.frakLaborPoint, new Vector3(), new Vector3(), 1f, new Color(0, 0, 0));

            ColShape val = NAPI.ColShape.CreateCylinderColShape(fraktion.garagePoint, 1.4f, 1.4f, uint.MaxValue);
            val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openFraktionsGarage", fraktion.fraktionName));
            val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um die Fraktionsgarage zu öffnen.", fraktion.fraktionName, "white", 5000));

			/*ColShape val3 = NAPI.ColShape.CreateCylinderColShape(fraktion.frakLaborPoint, 1.4f, 1.4f, uint.MaxValue);
			val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("enterLabor", fraktion.fraktionName));
			val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um das Labor zu betreten", fraktion.fraktionName, "white", 5000)); */

			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(fraktion.fraktionsLagerPoint, 1.4f, 1.4f, uint.MaxValue);
            val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("enterFraklager", fraktion.fraktionName));
            val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um das Fraklager zu betreten.", fraktion.fraktionName, "white", 5000));

			FraktionsVehicles.list.Add(fraktion.fraktionName, Database.getFraktionVehicles2(fraktion.fraktionName));
        }

        [RemoteEvent("exitFraklager")]
        public void exitFraklager(Client p)
        {
            try
            {
                foreach (Fraktion fraktion in fraktionList)
                {
                    if (Database.isPlayerInFrak(p, fraktion.fraktionName))
                    {
                        Anticheat.Wait(p); p.Position = fraktion.fraktionsLagerPoint.Add(new Vector3(0, 0, 1.5));

                        if (p.Dimension == fraktion.fraktionsDimension)
                            p.Dimension = 0;

                        return;
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		/*[RemoteEvent("exitFrakLabor")]
		public void exitFrakLabor(Client p)
		{
			try
			{
				foreach(Fraktion fraktion in fraktionList)
				{
					if (Database.isPlayerInFrak(p, fraktion.fraktionName))
					{
						Anticheat.Wait(p); p.Position = fraktion.frakLaborPoint.Add(new Vector3(0, 0, 1.5));

						if (p.Dimension == fraktion.fraktionsDimension)
							p.Dimension = 0;

						return;
					}
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		} */

		[RemoteEvent("enterFraklager")]
        public void enterFraklager(Client p, string fraktionname)
        {
            if (fraktionname == null)
                return;

            try
            {
                if (Database.isPlayerInFrak(p, fraktionname))
                {
                    Fraktion fraktion = Database.getFraktionByName(fraktionname);
                    if (p.Position.DistanceTo(fraktion.fraktionsLagerPoint) < 2.0f)
                    {
                        Anticheat.Wait(p); p.Position = fraklagerPos.Add(new Vector3(0, 0, 1.5));
                        p.Dimension = fraktion.fraktionsDimension;
                        return;
                    }
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion.", 5000, "white", fraktionname, "");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		/*[RemoteEvent("enterLabor")]
		public void enterLabor(Client p, string fraktionname)
		{
			if (fraktionname == null)
				return;

			try
			{
				if(Database.isPlayerInFrak(p, fraktionname))
				{
					    Fraktion fraktion = Database.getFraktionByName(fraktionname);
						Anticheat.Wait(p); p.Position = new Vector3(996.879, -3200.482, -37.49374).Add(new Vector3(0, 0, 1.5));
						p.Dimension = fraktion.fraktionsDimension;
					    p.TriggerEvent("loadMethInterior", 1, 2, 1);
						return;
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion.", 5000, "white", fraktionname, "");
				}
			} catch(Exception ex)
			{
				Log.Write("Failed to get in Fraktions Labor:" + ex.Message);
			}
		} */

		[RemoteEvent("openFraktionsGarage")]
        public void openFraktionsGarage(Client p, string fraktionname)
        {

            if (fraktionname == null)
                return;

            try
            {
                if (Database.isPlayerInFrak(p, fraktionname))
                {
                    p.TriggerEvent("fraktionsgarage:opengarage", fraktionname);
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion.", 5000, "white", fraktionname, "");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		[RemoteEvent("enterGangwarDimension")]
        public void enterGangwarDimension(Client p)
        {
            try
            {
                if (p.GetSharedData("FRAKTION") == "Zivilist")
                    return;

                if (p.Dimension != GangwarDimension)
                {
                    p.Dimension = (uint)GangwarDimension;
                    Notification.SendPlayerNotifcation(p, "Du hast die Gangwar Dimension betreten.", 5000, "white", "GANGWAR", "white");
                }
                else
                {
                    foreach (Fraktion fraktion in fraktionList)
                    {
                        if (Database.isPlayerInFrak(p, fraktion.fraktionName))
                        {
                            p.Dimension = fraktion.fraktionsDimension;
                            Notification.SendPlayerNotifcation(p, "Du hast die Gangwar Dimension verlassen.", 5000, "white", "GANGWAR", "white");
                        }
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("openWaffenschrank")]
        public void openWaffenschrank(Client p)
        {
            try
            {
                foreach (Fraktion fraktion in fraktionList)
                {
                    if (Database.isPlayerInFrak(p, fraktion.fraktionName))
                    {
                        if (fraktion.isBadFraktion)
                        {
							if(fraktion.fraktionName == "Los Santos Vagos")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Los Santos Vagos", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);

							} else if(fraktion.fraktionName == "Marabunta Grande 13")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Marabunta Grande 13", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Axt", "axe")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "La Cosa Nostra")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - La Cosa Nostra", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Golfschläger", "golfballer")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "Front Yard Ballas")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Front Yard Ballas", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "Midnight")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Midnight", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "High Rollin Hustlers")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - High Rollin Hustlers", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "Lost MC")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Lost MC", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);


							} else if(fraktion.fraktionName == "Devilish Kartell")
							{
								NativeMenu nativeMenu = new NativeMenu("Waffenkammer - Devilish Kartell", "Waffen", new List<NativeItem>()
								{
									new NativeItem("Machete", "machete")
								});
								nativeMenu.showNativeMenu(p);


							}
                        }
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        } 

        [RemoteEvent("openFraktionsKleiderschrank")]
        public void openFraktionsKleiderschrank(Client p)
        {
            try
            {
                foreach (Fraktion fraktion in fraktionList)
                {
                    if (Database.isPlayerInFrak(p, fraktion.fraktionName))
                    {
                        new NativeMenu("Fraktionskleiderschrank", fraktion.shortName, new List<NativeItem>()
                          {
                            new NativeItem("Maske", "Maske"),
                            new NativeItem("Oberteil", "Oberteil"),
                            new NativeItem("Unterteil", "Unterteil"),
                            new NativeItem("Körper", "Körper"),
                            new NativeItem("Hose", "Hose"),
                            new NativeItem("Schuhe", "Schuhe")
                          }).showNativeMenu(p);
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("nM-Fraktionskleiderschrank")]
        public void Fraktionskleiderschrank(Client p, string selection)
        {
            if (selection == null)
                return;

            try
            {
                foreach (Fraktion fraktion in fraktionList)
                {
                    if (Database.isPlayerInFrak(p, fraktion.fraktionName))
                    {
                        List<NativeItem> Items = new List<NativeItem>();
                        foreach (ClothingModel fraktionsClothe in fraktion.fraktionsClothes)
                        {
                            if (fraktionsClothe.category == selection)
                                Items.Add(new NativeItem(fraktionsClothe.name, selection + "-" + fraktionsClothe.component.ToString() + "-" + fraktionsClothe.drawable.ToString() + "-" + fraktionsClothe.texture.ToString()));
                        }
                        NativeMenu.closeNativeMenu(p);
                        new NativeMenu("Kleidungsauswahl", fraktion.fraktionName + " | " + selection, Items).showNativeMenu(p);
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

            
        }

		[RemoteEvent("nM-Waffenschrank")]
        public void FraktionsWaffenschrank(Client p, string selection)
        {
            if (selection == null)
                return;

            try
            {



            } catch(Exception ex) { Log.Write(ex.Message); }
        } 

	}
}

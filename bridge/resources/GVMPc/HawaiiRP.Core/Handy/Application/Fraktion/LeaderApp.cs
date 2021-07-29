using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Handy
{
    class LeaderApp : Script
    {
        [RemoteEvent("editTeamMember")]
        public void editTeamMember(Client p, string name, int newrank)
        {
            try
            {
                if (p.GetSharedData("FRAKTION_RANK") > 9)
                {
                    if (Database.isUserExists(name))
                    {
                        if (Database.getPlayerFraktion(name) == p.GetSharedData("FRAKTION"))
                        {
                            if (Database.getUserFraktionRank(name) < p.GetSharedData("FRAKTION_RANK"))
                            {
                                Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " auf Rang " + newrank + " gestuft.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                Database.setUserFraktionRank(name, newrank);
                                Client target = Database.getPlayerFromName(name);
                                if (target != null)
                                {
                                    Notification.SendPlayerNotifcation(target, "Dein Rang wurde auf " + newrank + " gestuft.", 5000, "white", target.GetSharedData("FRAKTION"), "white");
                                }
                            }
                            else
                            {
                                Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung, um die Rechte für diesen Spieler zu verändern.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                            }
                        }
                        else
                        {
                            Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht in deiner Fraktion.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                        }
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("addPlayer")]
        public void PhoneInvite(Client p, string name)
        {
            try
            {
                if (p.GetSharedData("FRAKTION_RANK") > 9)
                {
                    if (Database.isUserExists(name))
                    {
                        if (Database.getPlayerFraktion(name) == p.GetSharedData("FRAKTION"))
                        {
                            Notification.SendPlayerNotifcation(p, "Der Spieler ist bereits in deiner Fraktion.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                        }
                        else
                        {
                            if (Database.getPlayerFraktion(name) == "Zivilist")
                            {
                                Client target = Database.getPlayerFromName(name);
                                if (target != null)
                                {
                                    target.TriggerEvent("openWindow", new object[2]
                                        {
                                            "Confirmation",
                                            "{\"confirmationObject\":{\"Title\":\"" + p.GetSharedData("FRAKTION") + "\",\"Message\":\"Möchtest du die Einladung von " + p.Name + " annehmen?\",\"Callback\":\"acceptInvite\",\"Arg1\":\"" + p.GetSharedData("FRAKTION") + "\",\"Arg2\":\"\"}}"
                                        });

                                    // Dialogs.sendPlayerDialog(target, p.GetSharedData("FRAKTION"), "Möchtest du die Einladung von " + p.Name + " annehmen?", "phone:joinfrak", true, p.GetSharedData("FRAKTION"), "white");
                                    Notification.SendPlayerNotifcation(p, "Du hast " + name + " eine Einladung gesendet.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                }
                                else
                                {
                                    Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht online.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                }
                            }
                            else
                            {
                                Notification.SendPlayerNotifcation(p, "Dieser Spieler ist bereits in einer Fraktion.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                            }
                        }
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("kickMember")]
        public void PhoneUninvite(Client p, string name)
        {
            try
            {
                if (p.GetSharedData("FRAKTION_RANK") > 9)
                {
                    if (Database.isUserExists(name))
                    {
                        if (Database.getPlayerFraktion(name) == p.GetSharedData("FRAKTION"))
                        {
                            if (Database.getUserFraktionRank(name) < p.GetSharedData("FRAKTION_RANK"))
                            {
                                Database.setUserFraktion(name, "Zivilist");
                                Database.setUserFraktionRank(name, 0);
                                Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " uninvited.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                Client target = Database.getPlayerFromName(name);
                                if (target != null)
                                {
                                    target.SetSharedData("FRAKTION", "Zivilist");
                                    target.SetSharedData("FRAKTION_RANK", 0);
                                    Notification.SendPlayerNotifcation(target, "Du wurdest aus der Fraktion " + p.GetSharedData("FRAKTION") + " gekickt.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                }
                            }
                            else
                            {
                                Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung, um diesen Spieler zu uninviten.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                            }

                        }
                        else
                        {
                            Notification.SendPlayerNotifcation(p, "Dieser Spieler ist nicht in deiner Fraktion.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                        }
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Dieser Spieler existiert nicht.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("parkFraktionVehicles")]
        public void PhoneParkcars(Client p, object obj)
        {
            
            try
            {
                if (p.GetSharedData("FRAKTION_RANK") > 9)
                {
                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasSharedData("FRAKTION"))
                        {
                            if (target.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                            {
                                Notification.SendPlayerNotifcation(target, "Alle Fraktionsfahrzeuge wurden von " + p.Name + " eingeparkt.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                            }
                        }
                    }

                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (vehicle.HasSharedData("FRAKTION"))
                        {
                            if (vehicle.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                            {
                                vehicle.Delete();
                            }
                        }
                    }
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("acceptInvite")]
        public void PhoneJoinfrak(Client p, string frak, object unused)
        {
            try
            {
                p.SetSharedData("FRAKTION", frak);
                p.SetSharedData("FRAKTION_RANK", 0);
                Database.setUserFraktion(p.Name, frak);
                Database.setUserFraktionRank(p.Name, 0);

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.HasSharedData("FRAKTION"))
                    {
                        if (target.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                        {
                            Notification.SendPlayerNotifcation(target, "Der Spieler " + p.Name + " hat die Fraktion betreten.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                        }
                    }
                }

                Notification.SendPlayerNotifcation(p, "Du bist der Fraktion " + frak + " beigetreten.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        public static void PhoneClearfrak(Client p)
        {
            try
            {
                if (p.GetSharedData("FRAKTION") == "Zivilist")
                    return;

                if (p.GetSharedData("FRAKTION_RANK") == 12)
                {
                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasSharedData("FRAKTION"))
                        {
                            if (target.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                            {
                                Notification.SendPlayerNotifcation(target, "Alle Member der Fraktion " + p.GetSharedData("FRAKTION") + " wurden von " + p.Name + " gecleart.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                                if (p.GetSharedData("FRAKTION_RANK") < 12)
                                {
                                    target.SetSharedData("FRAKTION", "Zivilist");
                                    target.SetSharedData("FRAKTION_RANK", 0);
                                }
                            }
                        }
                    }

                    foreach (Fraktionen.FraktionPlayer fraktionPlayer in Database.getOfflineFraktionUsers(p.GetSharedData("FRAKTION")))
                    {
                        if (fraktionPlayer.fraktionRank < 12)
                        {
                            Database.setUserFraktion(fraktionPlayer.playerName, "Zivilist");
                            Database.setUserFraktionRank(fraktionPlayer.playerName, 0);
                        }
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}

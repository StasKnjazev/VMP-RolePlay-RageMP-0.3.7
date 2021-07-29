using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using GVMPc.Vehicles;
using GVMPc.Other;
using GVMPc.Clothing;
using GVMPc.ClothingShops;
using GVMPc.Fraktionen;
using GTANetworkAPI;
using System.Data.Common;
using GVMPc.Gangwar;
using GVMPc.Items;
using GVMPc.Handy;
using GVMPc.Ipad;

namespace GVMPc
{
	public class Database : Script
	{
		public static int phonenumber = 0;

		public static int money = 0;

		public static int spindId = 0;

		public static bool isInventoryExists(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT name FROM user_inventorys WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.HasRows;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting inv: " + ex.Message);
			}
			return false;
		}

		public static bool isUserExists(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT name FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var newname = reader.GetString(0);
							if (newname.Length > 1 && newname == name)
								return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user name: " + ex.Message);
			}
			return false;
		}

		public static bool isPhoneNumberExists(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT phonenumber FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user phonenumber: " + ex.Message);
			}
			return false;
		}

		public static bool isItemExistsInDatabase(string name, string item)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT count FROM user_inventorys WHERE name = '" + item + "' AND account = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting is item exists in inventory: " + ex.Message);
			}
			return false;
		}

		public static Fraktion getGebietFraktion(string gebiet)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT fraktion FROM gebiete WHERE name = '" + gebiet + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var fraktion = reader.GetString(0);
							if (getFraktionByName(fraktion) != null)
								return getFraktionByName(fraktion);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting gebiet fraktion: " + ex.Message);
			}
			return new Fraktion("Keine", "Keine", new Vector3(), new Vector3(), new Vector3(), 0, new Vector3(), new Vector3(), 0.0f, 0, new Color(0, 0, 0), new List<ClothingModel>(), true);
		}

		public static int getPlayerRights(string name)
		{
			return getPlayerRank(name).rankPermission;
		}

		public static string getBanReason(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT banreason FROM accounts WHERE social = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var reason = reader.GetString(0);

							if (reason != "null")
								return reason;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user banreason: " + ex.Message);
			}
			return "Bannumgehung";
		}

		public static bool isIdentifierBanned(string identifier, string value)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT banned FROM accounts WHERE " + identifier + " = '" + value + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var banned = reader.GetDecimal(0);
							if (banned == 1)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user is banned: " + ex.Message);
			}
			return false;
		}

		public static bool isAccountExists(string social)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT social FROM accounts WHERE social = '" + social + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var newname = reader.GetString(0);
							if (newname.Length > 1 && newname == social)
								return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting account name: " + ex.Message);
			}
			return false;
		}

		public static bool isVehicleOwnedByPlayer(string name, string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT owner FROM user_vehicles WHERE owner = '" + name + "' AND plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var owner = reader.GetString(0);

							return owner == name;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting vehicle is from user: " + ex.Message);
			}
			return false;
		}

		public static bool isHouseOwnedByPlayer(string name, string street)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT owner FROM house WHERE owner = '" + name + "' AND street = '" + street + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var owner = reader.GetString(0);

							return owner == name;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting vehicle is from user: " + ex.Message);
			}
			return false;
		}

		public static bool isVehicleRentedFromPlayer(Client p, string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT renter FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var renter = reader.GetString(0);

							if(renter == p.Name)
							{
								return true;

							} else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting vehicle is from user: " + ex.Message);
			}
			return false;
		}

		public static string getVehicleRenter(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT renter FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting vehicle is from user: " + ex.Message);
			}
			return "";
		}

		public static string getVehicleGarage(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT garage FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting vehicle garage: " + ex.Message);
			}
			return "";
		}

		public static void setUserRenter(string renter, string plate)
		{

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("UPDATE user_vehicles SET renter = '" + renter + "' WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while setting user Renter: " + ex.Message);
			}
		}

		public static bool isMaintenance()
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT maintenance FROM settings", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var maintenance = reader.GetDecimal(0);
                            if (maintenance == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting maintenance: " + ex.Message);
            }
            return true;
        }

        public static string getPlayerFraktion(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT fraktion FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user fraktion: " + ex.Message);
            }
            
            return "Error";
        }

		public static string getPlayerJob(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT job FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user job: " + ex.Message);
			}

			return "Error";
		}

		public static int getPlayerSpind(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT spindId FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var id = reader.GetInt16(0);
							return id;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user spindId: " + ex.Message);
			}
			return -1;
		}

		public static string getPlayerBusiness(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT business FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user business: " + ex.Message);
			}

			return "Error";
		}

		public static decimal getPlayerPlaytime(string social)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT playtime FROM accounts WHERE social = '" + social + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user business: " + ex.Message);
			}

			return -1;
		}

		public static decimal getUserFraktionRank(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT fraktionRank FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user rank: " + ex.Message);
            }
            return -1;
        }

		public static decimal getUserBusinessRank(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT businessRank FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user businessrank: " + ex.Message);
			}
			return -1;
		}

		public static decimal getUserJailtime(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT jailtime FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user jailtime: " + ex.Message);
			}
			return -1;
		}

		public static void updateJailtime(string name, int jailtime)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET jailtime = '" + jailtime + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting Jailtime: " + ex.Message);
			}
		}

		public static void upgradeHouseKeller(string owner, int upgrade)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE house SET isUpgraded = '" + upgrade + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting upgrade: " + ex.Message);
			}
		}

		public static void buyKeller(string owner, int upgrade)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE house SET hasKeller = '" + upgrade + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting Keller: " + ex.Message);
			}
		}

		public static void updateHouseOwner(int id, string oldOwner)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE house SET owner = @name WHERE id = @id";
				val2.Parameters.AddWithValue("@name", oldOwner);
				val2.Parameters.AddWithValue("@id", id);
				val2.ExecuteNonQuery();
			} catch (Exception ex)
			{
				Log.Write(ex.Message);

			} finally
			{
				val.Dispose();
			}
		}

		public static void updateOilOwner(int id, string oldOwner)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE oelpumpe SET owner = @name WHERE id = @id";
				val2.Parameters.AddWithValue("@name", oldOwner);
				val2.Parameters.AddWithValue("@id", id);
				val2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);

			}
			finally
			{
				val.Dispose();
			}
		}

		public static void updateWareHouseOwner(int id, string oldOwner)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE warehouse SET owner = @name WHERE id = @id";
				val2.Parameters.AddWithValue("@name", oldOwner);
				val2.Parameters.AddWithValue("@id", id);
				val2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);

			}
			finally
			{
				val.Dispose();
			}
		}

		public static void updateHouseKey(string name, int id)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE users SET h_key = @id WHERE name = @name";
				val2.Parameters.AddWithValue("@id", id);
				val2.Parameters.AddWithValue("@name", name);
				val2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);

			}
			finally
			{
				val.Dispose();
			}
		}

		public static void updateWareHouseKey(string name, int id)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE users SET w_key = @id WHERE name = @name";
				val2.Parameters.AddWithValue("@id", id);
				val2.Parameters.AddWithValue("@name", name);
				val2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);

			}
			finally
			{
				val.Dispose();
			}
		}

		public static string getBusinessMOTD(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT motd FROM business WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if (val3.HasRows)
					{
						while (val3.Read())
						{
							return val3.GetString("motd");
						}
					}
				}
				finally
				{
					val3.Dispose();
				}


			}
			catch (Exception ex)
			{
				Log.Write("[HawaiiRoleplay] : Fehler beim lesen des Adminranks" + ex.Message + ex.StackTrace);
			}
			return "Error";
		}

		public static decimal getUserPhoneNumber(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using(var cmd = new MySqlCommand("SELECT phonenumber FROM users WHERE name = '" + name + "'", connection))
				{
					using(var reader = cmd.ExecuteReader())
					{
						while(reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting Phonenumber: " + ex.Message);
			}
			return -1;
		}

		public static decimal getHouseId(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT id FROM house WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting Phonenumber: " + ex.Message);
			}
			return -1;
		}

		public static bool isPlayerInFrak(Client p, string frak)
        {
            if (Start.loggedInPlayers.ContainsKey(p))
            {
                return getPlayerFraktion(p.Name) == frak;
            }
            else
            {
                return false;
            }
        }


		public static bool isPlayerDeath(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT death FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var death = reader.GetDecimal(0);
                            if(death == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user deathstatus: " + ex.Message);
            }
            return true;
        }

		public static bool hasPlayerHouse(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT h_key FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var h_key = reader.GetDecimal(0);
							if (h_key > 0)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
			return true;
		}

		public static bool hasPlayerWarehouse(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT w_key FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var w_key = reader.GetDecimal(0);
							if (w_key > 0)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
			return true;
		}

		public static bool isHouseLocked(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT locked FROM house WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var locked = reader.GetDecimal(0);
							if (locked == 1)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
			return true;
		}

		public static bool isWareHouseLocked(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT locked FROM warehouse WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var locked = reader.GetDecimal(0);
							if (locked == 1)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
			return true;
		}

		public static void setHouseLocked(string owner, int value)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("UPDATE house SET locked = '" + value + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
		}

		public static void setWarehouseLocked(string owner, int value)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("UPDATE warehouse SET locked = '" + value + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user deathstatus: " + ex.Message);
			}
		}


		public static Vector3 getPlayerPosition(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT location FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var location = reader.GetString(0);
                            Vector3 loc = NAPI.Util.FromJson<Vector3>(location);

                            return loc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user position: " + ex.Message);
            }
            return new Vector3(0, 0, 0);
        }

        public static bool isPlayerTeam(string name)
        {
            if (getPlayerRights(name) > 0)
                return true;
            else
                return false;
        }

        public static string getPlayerRank2(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT rank FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rank = reader.GetString(0);
                            return rank;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user rank: " + ex.Message);
            }
            return "User";
        }

        public static Players.AdminRank getPlayerRank(string name)
        {
            if (NAPI.Player.GetPlayerFromName(name) == null || !NAPI.Player.GetPlayerFromName(name).Exists || NAPI.Player.GetPlayerFromName(name).GetSharedData("PLAYER_RANK") == null)
                return new Players.AdminRank(0, "User", 0, new Color(0, 0, 0));

            return Players.AdminRanks.getRankFromName(NAPI.Player.GetPlayerFromName(name).GetSharedData("PLAYER_RANK"));
        }

        public static void updateGebietFraktion(string gebiet, string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE gebiete SET fraktion = '" + fraktion + "' WHERE name = '" + gebiet + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while updating fraktion from gebiet: " + ex.Message);
            }
        }

        public static void updateAddress(Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE accounts SET ip = '" + p.Address + "' WHERE social = '" + p.SocialClubName + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while setting ip: " + ex.Message);
            }
        }

        public static void setClothes(Client p, PlayerClothes playerClothes)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET clothes = '" + NAPI.Util.ToJson(playerClothes) + "' WHERE name = '" + p.Name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while setting clothes: " + ex.Message);
            }
        }

        public static PlayerClothes getDBClothing(Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            PlayerClothes playerClothes = new PlayerClothes();

            try
            {

                using (var cmd = new MySqlCommand("SELECT clothes FROM users WHERE name = '" + p.Name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var clothes = reader.GetString(0);

                            if (clothes != "[]")
                            {
                                playerClothes = NAPI.Util.FromJson<PlayerClothes>(clothes);
                                return playerClothes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user outfit: " + ex.Message);
            }

            return playerClothes;
        }

        public static List<ItemModel> getUserItems(string character)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            List<ItemModel> items = new List<ItemModel>();
            try
            {

                using (var cmd = new MySqlCommand("SELECT items FROM user_inventorys WHERE name = '" + character + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var itemsjson = reader.GetString(0);

                                if (itemsjson != "[]")
                                {
                                    return NAPI.Util.FromJson<List<ItemModel>>(itemsjson);
                                }

                            }
                        }
                        else
                        {
                            Notification.SendPlayerNotifcation(NAPI.Player.GetPlayerFromName(character), "Dein Inventar wurde erstellt.", 3500, "green", "INVENTAR", "");

                            createInventory(character);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user items: " + ex.Message);
            }
            return items;
        }

        public static void createInventory(string account)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO user_inventorys (name, items) VALUES ('" + account + "', '" + NAPI.Util.ToJson(new List<ItemModel>()) + "')", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating inventory: " + ex.Message);
            }
        }

		public static void createBusiness(string name)
		{

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("INSERT INTO business (name) VALUES ('" + name + "')", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating business: " + ex.Message);
			}
		}

		public static void createSMS(string sender, string name, string message)
		{

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("INSERT INTO sms (sender, reciver, message) VALUES ('" + sender +"', '" + name + "', '" + message + "')", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating sms: " + ex.Message);
			}
		}

		public static List<ItemModel> getKofferraumItems(string plate)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            List<ItemModel> dict = new List<ItemModel>();
            try
            {

                using (var cmd = new MySqlCommand("SELECT trunk FROM user_vehicles WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var jsonString = reader.GetString(0);

                            if (jsonString != "[]")
                                dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting kofferraum items: " + ex.Message);
            }
            return dict;
        }

		public static List<ItemModel> getOilItems(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT lager FROM oelpumpe WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting oil items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getFraklagerItems(string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            List<ItemModel> dict = new List<ItemModel>();
            try
            {

                using (var cmd = new MySqlCommand("SELECT fraklager FROM fraktionen WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var jsonString = reader.GetString(0);

                            if(jsonString != "[]")
                                dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting fraktionlager items: " + ex.Message);
            }
            return dict;
        }

		public static List<ItemModel> getHouseItems (string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT lager FROM house WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting houseLager items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getWareHouseItems(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT storage FROM warehouse WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting houseLager items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getKellerItems(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT keller FROM house WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting resourcen items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getLabResourceItems(string frak)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT laborresource FROM fraktionen WHERE fraktionName = '" + frak + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting labResource items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getLabProduktItems(string frak)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT laborprodukt FROM fraktionen WHERE fraktionName = '" + frak + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting resourcen items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getSpindItems(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT spind FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting Spind items: " + ex.Message);
			}
			return dict;
		}

		public static List<ItemModel> getTresorItems(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ItemModel> dict = new List<ItemModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT tresor FROM bank WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var jsonString = reader.GetString(0);

							if (jsonString != "[]")
								dict = NAPI.Util.FromJson<List<ItemModel>>(jsonString);

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting Tresor items: " + ex.Message);
			}
			return dict;
		}


		public static List<Fraktionen.FraktionPlayer> getOfflineFraktionUsers(string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<Fraktionen.FraktionPlayer> list = new List<Fraktionen.FraktionPlayer>();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM users WHERE fraktion = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString(2);
                            var rank = reader.GetDecimal(7);

                            if(!Start.loggedInPlayers.ContainsValue(name))
                                list.Add(new FraktionPlayer(fraktion, (int)rank, name));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting offline fraktionusers: " + ex.Message);
            }
            return list;
        }

		public static List<FraktionPlayer> getFraktionOnlinePlayers(string fraktion)
		{
			List<FraktionPlayer> list = new List<FraktionPlayer>();

			foreach (Client p in NAPI.Pools.GetAllPlayers())
			{
				if (p.HasSharedData("FRAKTION"))
				{
					if (p.GetSharedData("FRAKTION") == fraktion)
					{

						if (!list.Contains(new FraktionPlayer(fraktion, (int)Database.getUserFraktionRank(p.Name), p.Name)))
							list.Add(new FraktionPlayer(fraktion, (int)Database.getUserFraktionRank(p.Name), p.Name));
					}
				}
			}

			return list;
		}

		public static List<FraktionListModel> getFraktionOnlinePlayer(string fraktion)
		{
			List<FraktionListModel> list1 = new List<FraktionListModel>();

			foreach (Client p in NAPI.Pools.GetAllPlayers())
			{
				if (p.HasSharedData("FRAKTION"))
				{
					if (p.GetSharedData("FRAKTION") == fraktion)
					{

						if (!list1.Contains(new FraktionListModel((int)getUserFraktionRank(p.Name), "", p.Name, 0, false)))
							list1.Add(new FraktionListModel((int)getUserFraktionRank(p.Name), "", p.Name, 0, false));
					}
				}
			}

			return list1;
		}

		public static List<BusinessPlayer> getBusinessOnlinePlayers(string business)
		{
			List<BusinessPlayer> list1 = new List<BusinessPlayer>();

			foreach (Client p in NAPI.Pools.GetAllPlayers())
			{
				if (p.HasSharedData("BUSINESS"))
				{
					if (p.GetSharedData("BUSINESS") == business)
					{

						if (!list1.Contains(new BusinessPlayer(business, (int)Database.getUserBusinessRank(p.Name), p.Name)))
							list1.Add(new BusinessPlayer(business, (int)Database.getUserBusinessRank(p.Name), p.Name));
					}
				}
			}

			return list1;
		}


		public static List<GarageVehicleModel> getParkedVehicles(string character)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<GarageVehicleModel> list = new List<GarageVehicleModel>();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE owner = '" + character + "' AND parked = 1", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetDecimal(0);
                            var name = reader.GetString(1);
                            var plate = reader.GetString(2);
                            var parked = reader.GetDecimal(3);

                            if(parked == 1)
                                list.Add(new GarageVehicleModel((int)id, (int)Database.getSQLId(character), name, plate));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user parked vehicles: " + ex.Message);
            }
            return list;
        }

		public static List<GarageVehicleModel> getParkedRenterVehicles(string character)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<GarageVehicleModel> list = new List<GarageVehicleModel>();

			try
			{

				using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE renter = '" + character + "' AND parked = 1 ", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var id = reader.GetDecimal(0);
							var name = reader.GetString(1);
							var plate = reader.GetString(2);
							var parked = reader.GetDecimal(3);

							if (parked == 1)
								list.Add(new GarageVehicleModel((int)id, (int)Database.getSQLId(character), name, plate));
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user parked vehicles: " + ex.Message);
			}
			return list;
		}

		public static List<VehicleModel> getFraktionVehicles2(string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<VehicleModel> list = new List<VehicleModel>();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM fraktion_vehicles WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetDecimal(0);
                            var name = reader.GetString(1);
                            var model = reader.GetString(2);

                            if(!list.Contains(new VehicleModel(fraktion, model, getFraktionByName(fraktion).shortName)))
                                list.Add(new VehicleModel(fraktion, model, getFraktionByName(fraktion).shortName));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting fraktion vehicles: " + ex.Message);
            }
            return list;
        }

		public static decimal getVehiclePlate(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT plate FROM user_vehicles WHERE owner = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle plate: " + ex.Message);
			}
			return -1; 
		}

		public static decimal getVehicleFuel(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT fuel FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle fuel: " + ex.Message);
			}
			return -1;
		}

		public static void updateVehicleFuel(string plate, int fuel)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE user_vehicles SET fuel = @fuel WHERE plate = @plate";
				val2.Parameters.AddWithValue("@fuel", fuel);
				val2.Parameters.AddWithValue("@plate", plate);
				val2.ExecuteNonQuery();

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			} finally
			{
				val.Dispose();
			}
		}

		public static decimal getVehicleTax(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT tax FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle tax: " + ex.Message);
			}
			return -1;
		}

		public static decimal getVehicleTaxFromOwner(string owner)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT tax FROM user_vehicles WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle tax: " + ex.Message);
			}
			return -1;
		}


		public static string getVehicleName(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT modelname FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle name: " + ex.Message);
			}
			return "";
		}

		public static decimal getVehicleDistance(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT distance FROM user_vehicles WHERE plate = '" + plate + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicle distance: " + ex.Message);
			}
			return -1;
		}

		public static string getFraktionsVehicleModelById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM fraktion_vehicles WHERE id = " + id + "", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = reader.GetString(2);

                            return model;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle by id: " + ex.Message);
            }
            return "oracle2";
        }

        public static VehicleModel getVehicleById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE id = " + id + "", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = reader.GetString(1);
                            var plate = reader.GetString(2);
                            var parked = reader.GetDecimal(3);
                            var owner = reader.GetString(4);

                            return new VehicleModel(owner, model, plate);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle by id: " + ex.Message);
            }
            return new VehicleModel("", "oracle2", "error");
        }

		public static string getPlayerInfoAddress(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			val.Open();
			try
			{
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT address FROM personinfo WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if(val3.HasRows)
					{
						while(val3.Read())
						{
							return val3.GetString("address");
						}
					}

				} finally
				{
					val3.Dispose();
				}

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return "Keine Information";
		}

		public static string getPlayerInfoMemberShip(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			val.Open();
			try
			{
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT memberShip FROM personinfo WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if (val3.HasRows)
					{
						while (val3.Read())
						{
							return val3.GetString("memberShip");
						}
					}

				}
				finally
				{
					val3.Dispose();
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return "Keine Information";
		}

		public static decimal getPlayerInfoPhoneNumber(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			val.Open();
			try
			{
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT phonenumber FROM personinfo WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if (val3.HasRows)
					{
						while (val3.Read())
						{
							return val3.GetDecimal("phonenumber");
						}
					}

				}
				finally
				{
					val3.Dispose();
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return -1;
		}

		public static string getPlayerInfo(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			val.Open();
			try
			{
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT info FROM personinfo WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if (val3.HasRows)
					{
						while (val3.Read())
						{
							return val3.GetString("info");
						}
					}

				}
				finally
				{
					val3.Dispose();
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return "Keine Information";
		}


		public static bool isVehicleParked(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE id = " + id + "", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var parked = reader.GetDecimal(3);

                            bool val = false;

                            if (parked == 1)
                                val = true;

                            return val;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle parked by id: " + ex.Message);
            }
            return false;
        }

		public static bool isVehParked(string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("SELECT parked FROM user_vehicles WHERE plate = " + plate + "", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while(reader.Read())
						{
							var parked = reader.GetDecimal(3);

							bool val = false;

							if (parked == 1)
								val = true;

							return val;
						}
					}
				}


			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return false;
		}

        public static List<VehicleModel> getFraktionVehicles(string fraktion)
        {
            if(FraktionsVehicles.list.ContainsKey(fraktion))
            {
                return FraktionsVehicles.list[fraktion];
            }

            return new List<VehicleModel>();
        }

        public static Fraktion getFraktionByName(string name)
        {
            foreach(Fraktion fraktion in Fraktionen.FraktionRegister.fraktionList)
            {
                if (fraktion.fraktionName == name)
                    return fraktion;
            }
			return new Fraktion("Zivilist", "Zivilist", new Vector3(), new Vector3(), new Vector3(), 0, new Vector3(), new Vector3(), 0.0f, 0, new Color(0, 0, 0), new List<ClothingModel>(), true);
		}

		public static Fraktion getFraktionByShortName(string name)
        {
            foreach (Fraktion fraktion in Fraktionen.FraktionRegister.fraktionList)
            {
                if (fraktion.shortName == name)
                    return fraktion;
            }
            return new Fraktion("Zivilist", "Zivilist", new Vector3(), new Vector3(), new Vector3(), 0, new Vector3(), new Vector3(), 0.0f, 0, new Color(0, 0, 0), new List<ClothingModel>(), true);
        }

        public static ItemModel getItemModelByName(string name, int count)
        {
            Item item = ItemManager.itemRegisterList.Find((Item i) => i.Name == name);

            if(item == null)
                return new ItemModel(0, 0, 0, "", "", 0);

            return new ItemModel(item.Id, 0, 0, item.Name, item.ImagePath, count);
        }


        public static Gebiet getGebietByName(string name)
        {
            foreach (Gebiet gebiet in GebietRegister.gebietList)
            {
                if (gebiet.name == name)
                    return gebiet;
            }
            return null;
        }

        public static List<VehicleModel> getUserVehicles(string character)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<VehicleModel> list = new List<VehicleModel>();

            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE owner = '" + character + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetDecimal(0);
                            var name = reader.GetString(1);
                            var plate = reader.GetString(2);
                            var parked = reader.GetDecimal(3);

                            if(!list.Contains(new VehicleModel(character, name, plate)))
                                list.Add(new VehicleModel(character, name, plate));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user vehicles: " + ex.Message);
            }
            return list;
        }

		public static List<VehicleModel> getRenterVehicles(string character)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<VehicleModel> list = new List<VehicleModel>();

			try
			{

				using (var cmd = new MySqlCommand("SELECT * FROM user_vehicles WHERE renter = '" + character + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var id = reader.GetDecimal(0);
							var name = reader.GetString(1);
							var owner = reader.GetString("owner");
							var plate = reader.GetString(2);
							var parked = reader.GetDecimal(3);

							if (!list.Contains(new VehicleModel(owner, name, plate)))
								list.Add(new VehicleModel(owner, name, plate));
						}

					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user vehicles: " + ex.Message);
			}
			return list;
		}

		public static decimal getItemCount(string character, string item)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<ItemModel> list = getUserItems(character);

            foreach(ItemModel itemModel in list)
            {
                if(itemModel.Name == item)
                {
                    return itemModel.Amount;
                }
            }
            
            return -1;
        }

		public static decimal getItemsWeight(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<ItemModel> list = getUserItems(name);

			int weigth = 0;

			foreach (ItemModel itemModel in list)
			{
				weigth = + itemModel.Weight * itemModel.Amount;
			}

			return weigth;
		}

		public static decimal getKellerItemCount(string owner, string item)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<ItemModel> list = getKellerItems(owner);

			foreach (ItemModel itemModel in list)
			{
				if (itemModel.Name == item)
				{
					return itemModel.Amount;
				}
			}

			return -1;
		}

		public static decimal getLabResourceItemCount(string owner, string item)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<ItemModel> list = getLabResourceItems(owner);

			foreach (ItemModel itemModel in list)
			{
				if (itemModel.Name == item)
				{
					return itemModel.Amount;
				}
			}

			return -1;
		}

		public static decimal getLabProduktItemCount(string owner, string item)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<ItemModel> list = getLabProduktItems(owner);

			foreach (ItemModel itemModel in list)
			{
				if (itemModel.Name == item)
				{
					return itemModel.Amount;
				}
			}

			return -1;
		}

		public static decimal getFrakBank(string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT frakKontostand FROM fraktionen WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var count = reader.GetDecimal(0);
                            return count;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting fraktion kontostand: " + ex.Message);
            }
            return -1;
        }

		public static decimal getUserBank(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT bank FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var bank = reader.GetDecimal(0);
							return bank;

						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting player kontostand: " + ex.Message);
			}
			return -1;
		}

		public static List<WeaponHash> getLoadout(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<WeaponHash> list = new List<WeaponHash>();

            try
            {

                using (var cmd = new MySqlCommand("SELECT loadout FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.GetString(0) != "[]")
                                list = NAPI.Util.FromJson<List<WeaponHash>>(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting loadout: " + ex.Message);
            }
            return list;
        }

		public static decimal getUserArmor(Client p)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT armor FROM users WHERE name = @name";
				val2.Parameters.AddWithValue("@name", p.Name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if(val3.HasRows)
					{
						while(val3.Read())
						{
							return val3.GetDecimal("armor");
						}
					}

				} finally
				{
					val3.Dispose();
				}

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
			return 0;
		}

		public static void setUserArmor(Client p, int armor)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE users SET armor = @armor WHERE name = @name";
				val2.Parameters.AddWithValue("@armor", armor);
				val2.Parameters.AddWithValue("@name", p.Name);
				val2.ExecuteNonQuery();

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			} finally
			{
				val.Dispose();
			}
		}

        public static decimal getFraktionVehicleSQLId(string model, string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT id FROM fraktion_vehicles WHERE vehicleModel = '" + model + "' AND fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting frak vehicle sql id: " + ex.Message);
            }
            return -1;
        }

        public static decimal getVehicleSQLId(string plate)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT id FROM user_vehicles WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting sql id: " + ex.Message);
            }
            return -1;
        }

        public static ItemModel getItemById(int id)
        {
            Log.Write(id.ToString());

            Item item = ItemManager.itemRegisterList.Find((Item i) => i.Id == id);

            if (item == null)
                return new ItemModel(0, 0, 0, "", "", 0);

            return new ItemModel(item.Id, 0, item.WeightInG, item.Name, item.ImagePath, 1);
        }

        public static decimal getSQLId(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT id FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting sql id: " + ex.Message);
            }
            return -1;
        }

        public static string getFraktionNameBySQLId(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT fraktionName FROM fraktionen WHERE fraktionId = " + id + "", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting frak by sql id: " + ex.Message);
            }
            return "Error";
        }

		public static decimal getFraktionSQLId(string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT fraktionId FROM fraktionen WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting frak sql id: " + ex.Message);
            }
            return -1;
        }

		public static decimal getSpindSQLId(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT id FROM spind WHERE player = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting spind sql id: " + ex.Message);
			}
			return -1;
		}

		public static decimal getBankSQLId(string bank)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT id FROM bank WHERE name = '" + bank + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetDecimal(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting bank sql id: " + ex.Message);
			}
			return -1;
		}

		public static string getNameById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT name FROM users WHERE id = " + id + "", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting name by id: " + ex.Message);
            }
            return "Error";
        }

        public static void removeLoadout(string name, WeaponHash weapon)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<WeaponHash> loadout = getLoadout(name);

            if(loadout.Contains(weapon))
                loadout.Remove(weapon);

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET loadout = '" + NAPI.Util.ToJson(loadout) + "' WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while adding weapon to user: " + ex.Message);
            }

        }

		public static void setRights(string name, int rank)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET rank = '" + rank + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting rank to user: " + ex.Message);
			}

		}

		public static void addLoadout(string name, WeaponHash weapon)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            List<WeaponHash> loadout = getLoadout(name);
            if(!loadout.Contains(weapon))
                loadout.Add(weapon);

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET loadout = '" + NAPI.Util.ToJson(loadout) + "' WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while adding weapon to user: " + ex.Message);
            }
        }

        public static void changeKofferraumItem(string plate, string item, int count, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                List<ItemModel> list = getKofferraumItems(plate);
                List<int> blockedSlots = new List<int>();
                int slot = 0;
                bool found = false;
                ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

                foreach (ItemModel itemModel in list)
                {
                    blockedSlots.Add(itemModel.Slot);
                    if (itemModel.Name == item)
                    {
                        currentModel = itemModel;
                        found = true;
                    }
                }

                if (found)
                {
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
                else
                {
                    bool slotfound = false;

                    while (slotfound == false)
                    {
                        if (blockedSlots.Contains(slot))
                            slot++;
                        else
                            slotfound = true;
                    }

                    if (!remove && !found)
                        list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
                }

                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET trunk = '" + NAPI.Util.ToJson(list) + "' WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while chaning item to trunk: " + ex.Message);
            }


        }

		public static void changeOilItem(string owner, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getOilItems(owner);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE oelpumpe SET lager = '" + NAPI.Util.ToJson(list) + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning item to trunk: " + ex.Message);
			}


		}

		public static void changeWareHouseItem(string owner, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getWareHouseItems(owner);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE warehouse SET storage = '" + NAPI.Util.ToJson(list) + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning item to trunk: " + ex.Message);
			}


		}

		public static void changeSpindItem(string name, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getSpindItems(name);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE users SET spind = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning item to spind: " + ex.Message);
			}


		}

		public static void changeTresorItem(string name, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getTresorItems(name);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE bank SET tresor = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning item to tresor: " + ex.Message);
			}
		}

		public static void changeHouseItem(string owner, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getHouseItems(owner);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE house SET lager = '" + NAPI.Util.ToJson(list) + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning item to tresor: " + ex.Message);
			}


		}

		public static void changeKellerItem(string owner, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getKellerItems(owner);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE house SET keller = '" + NAPI.Util.ToJson(list) + "' WHERE owner = '" + owner + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing Item to keller: " + ex.Message);
			}


		}

		public static void changeLabResourceItem(string frak, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getLabResourceItems(frak);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE fraktionen SET laborresource = '" + NAPI.Util.ToJson(list) + "' WHERE fraktionName = '" + frak + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing Item to labx: " + ex.Message);
			}
		}

		public static void changeLabProduktItem(string frak, string item, int count, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ItemModel> list = getLabProduktItems(frak);
				List<int> blockedSlots = new List<int>();
				int slot = 0;
				bool found = false;
				ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

				foreach (ItemModel itemModel in list)
				{
					blockedSlots.Add(itemModel.Slot);
					if (itemModel.Name == item)
					{
						currentModel = itemModel;
						found = true;
					}
				}

				if (found)
				{
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
				else
				{
					bool slotfound = false;

					while (slotfound == false)
					{
						if (blockedSlots.Contains(slot))
							slot++;
						else
							slotfound = true;
					}

					if (!remove && !found)
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
				}

				using (var cmd = new MySqlCommand("UPDATE fraktionen SET laborprodukt = '" + NAPI.Util.ToJson(list) + "' WHERE fraktionName = '" + frak + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing Item to labory: " + ex.Message);
			}


		}

		public static string getWallpaper(string name)
		{
			using MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT Wallpaper FROM users WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if(val3.HasRows)
					{
						while(val3.Read())
						{
							return val3.GetString("Wallpaper");
						}
					}
				} finally
				{
					val3.Dispose();
				}

			} catch (Exception ex)
			{

			} finally
			{
				val.Dispose();
			}
			return "";
		}

		public static void changeWallpaper(string name, int id)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			string file = "";
			switch (id)
			{
				case 0:
					file = "Army.png";
					break;
				case 1:
					file = "Ballas.png";
					break;
				case 2:
					file = "Bratwa.png";
					break;
				case 3:
					file = "Brigada.png";
					break;
				case 4:
					file = "FIB.png";
					break;
				case 5:
					file = "DPOS.png";
					break;
				case 6:
					file = "Fahrschule.png";
					break;
				case 7:
					file = "Grove.png";
					break;
				case 8:
					file = "Hustlers.jpg";
					break;
				case 9:
					file = "ICA.jpg";
					break;
				case 10:
					file = "LCN.png";
					break;
				case 11:
					file = "LOST.png";
					break;
				case 12:
					file = "LSC.png";
					break;
				case 13:
					file = "LSMC.png";
					break;
				case 14:
					file = "LSPD.png";
					break;
				case 15:
					file = "Marabunta.png";
					break;
				case 16:
					file = "Rednecks.jpg";
					break;
				case 17:
					file = "Regierung.png";
					break;
				case 18:
					file = "Triaden.png";
					break;
				case 19:
					file = "Vagos.png";
					break;
				case 20:
					file = "Weazel.png";
					break;
				case 21:
					file = "YakuZa.png";
					break;
				case 22:
					file = "black.jpg";
					break;
				case 23:
					file = "boy.png";
					break;
				case 24:
					file = "bubblebutt.gif";
					break;
				case 25:
					file = "danerwin.png";
					break;
				case 26:
					file = "devgirl.gif";
					break;
				case 27:
					file = "landscape.png";
					break;
				case 28:
					file = "landschaft.jpg";
					break;
				case 29:
					file = "meer.jpg";
					break;
				case 30:
					file = "milakunis.gif";
					break;
				case 31:
					file = "park.jpg";
					break;
				case 32:
					file = "pferd.jpg";
					break;
				case 33:
					file = "pier.jpg";
					break;
				case 34:
					file = "regierung.jpg";
					break;
				case 35:
					file = "staudamm.jpg";
					break;
				case 36:
					file = "starnd.jpg";
					break;
			}

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET Wallpaper = '" + file + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex) { Log.Write("Error while changing wallpaper " + ex.Message); }
		}

		public static void changeInventoryItem(string account, string item, int count, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
				Client target = getPlayerFromName(account);
                List<ItemModel> list = getUserItems(account);
                List<int> blockedSlots = new List<int>();
                int slot = 0;
                bool found = false;
                ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

                foreach(ItemModel itemModel in list)
                {
                    blockedSlots.Add(itemModel.Slot);
                    if(itemModel.Name == item)
                    {
						currentModel = itemModel;
						found = true; 
                    }
                }

                if (found)
                {
                    if (list.Contains(currentModel))
                        list.Remove(currentModel);

                    if (remove)
                    {
                        currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
                    }
                    else
                    {
                        currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = + getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
						

                }
                else
                {
                    bool slotfound = false;

                    while(slotfound == false)
                    {
                        if (blockedSlots.Contains(slot))
                            slot++;
                        else
                            slotfound = true;
                    }

					if (!remove && !found)
					{
						list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
					}
                }


                if (!isInventoryExists(account))
                    return;

                using (var cmd = new MySqlCommand("UPDATE user_inventorys SET items = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + account + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing item count: " + ex.Message);
            }


        }

        public static void changeInventoryItemSlot(string account, string item, int newslot)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                List<ItemModel> list = getUserItems(account);
                bool found = false;
                ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

                foreach (ItemModel itemModel in list)
                {
                    if (itemModel.Name == item)
                    {
                        currentModel = itemModel;
                        found = true;
                    }
                }

                if (found)
                {
                    if (list.Contains(currentModel))
                        list.Remove(currentModel);

                    currentModel.Slot = newslot;

                    list.Add(currentModel);
                }

                using (var cmd = new MySqlCommand("UPDATE user_inventorys SET items = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + account + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing item slot: " + ex.Message);
            }


        }

        public static void changeFraklagerItem(string fraktion, string item, int count, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                List<ItemModel> list = getFraklagerItems(fraktion);
                List<int> blockedSlots = new List<int>();
                int slot = 0;
                bool found = false;
                ItemModel currentModel = new ItemModel(0, 0, 0, "", "", 0);

                foreach (ItemModel itemModel in list)
                {
                    blockedSlots.Add(itemModel.Slot);
                    if (itemModel.Name == item)
                    {
                        currentModel = itemModel;
                        found = true;
                    }
                }

                if (found)
                {
					if (list.Contains(currentModel))
						list.Remove(currentModel);

					if (remove)
					{
						currentModel.Amount = currentModel.Amount - count;
						currentModel.Weight = currentModel.Weight - getItemWeight(item);
					}
					else
					{
						currentModel.Amount = currentModel.Amount + count;
						currentModel.Weight = +getItemWeight(item);
					}

					if (currentModel.Amount > 0)
						list.Add(currentModel);
				}
                else
                {
                    bool slotfound = false;

                    while (slotfound == false)
                    {
                        if (blockedSlots.Contains(slot))
                            slot++;
                        else
                            slotfound = true;
                    }

                    if (!remove && !found)
                        list.Add(new ItemModel(1, slot, getItemWeight(item), item, getItemURL(item), count));
                }

                using (var cmd = new MySqlCommand("UPDATE fraktionen SET fraklager = '" + NAPI.Util.ToJson(list) + "' WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing fraktion item count: " + ex.Message);
            }
        }

        public static void setUserFraktion(string user, string fraktion)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET fraktion = '" + fraktion + "' WHERE name = '" + user + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while setting user fraktion: " + ex.Message);
            }
        }

		public static void setUserJob(string user, string job)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET job = '" + job + "' WHERE name = '" + user + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting user job: " + ex.Message);
			}
		}

		public static void setUserBusiness(string user, string business)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET business = '" + business + "' WHERE name = '" + user + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting user business: " + ex.Message);
			}
		}

		public static void setBusinessMOTD(string name, string motd)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE business SET motd = '" + motd + "' WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting business motd: " + ex.Message);
			}
		}

		public static Client getPlayerFromName(string name)
        {
            foreach (Client p in NAPI.Pools.GetAllPlayers())
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }

		public static Vehicle getVehicleFromPlate(string plate)
		{
			foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
			{
				if(vehicle.NumberPlate == plate)
				{
					return vehicle;
				}
			}
			return null;
		}

		public static Client getPlayerFromPhonenumber(int phonenumber)
		{
			foreach (Client p in NAPI.Pools.GetAllPlayers())
			{
				if (phonenumber == Database.getUserPhoneNumber(p.Name))
				{
					return p;
				}
			}
			return null;
		}

		public static void setUserFraktionRank(string user, int rank)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET fraktionRank = '" + rank + "' WHERE name = '" + user + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while setting user ranks: " + ex.Message);
            }
        }

		public static void setUserBusinessRank(string user, int rank)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET businessRank = '" + rank + "' WHERE name = '" + user + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while setting user businessrank: " + ex.Message);
			}
		}

		public static void parkAllVehicles()
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET parked = 1 WHERE 1", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while parking all vehicles: " + ex.Message);
            }

            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                vehicle.Delete();

        }

        public static void changeVehicleState(string plate, decimal state)
        {
            MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET parked = " + state + " WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while chaning vehicle state: " + ex.Message);
            }
        }

		public static void changeVehiclePlate(string name, string plate)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE user_vehicles SET plate = " + plate + " WHERE owner = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while chaning vehicle state: " + ex.Message);
			}
		}

		public static void removeAllItems(string accountname)
        {
            List<ItemModel> list = getUserItems(accountname);

            list.Clear();

            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_inventorys SET items = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + accountname + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user inv: " + ex.Message);
            }
        }

        public static void savePlayerClothes(Client p, int componentId, int drawableId, int textureId, bool accessory)
        {
            PlayerClothes playerClothes = Database.getDBClothing(p);

            if (componentId == 1)
            {
                playerClothes.Maske = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 1, playerClothes.Maske.drawable, playerClothes.Maske.texture);
            }
            else if (componentId == 0 && accessory)
            {
                if (Convert.ToInt32(drawableId) == 500)
                {
                    playerClothes.Hut = new clothingPart()
                    {
                        drawable = -1,
                        texture = 0
                    };
                    p.SetAccessories(0, playerClothes.Hut.drawable, playerClothes.Hut.texture);
                }
                else
                {
                    playerClothes.Hut = new clothingPart()
                    {
                        drawable = Convert.ToInt32(drawableId),
                        texture = Convert.ToInt32(textureId)
                    };
                    p.SetAccessories(0, playerClothes.Hut.drawable, playerClothes.Hut.texture);
                }
            }
            else if (componentId == 11)
            {
                playerClothes.Oberteil = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 11, playerClothes.Oberteil.drawable, playerClothes.Oberteil.texture);
            }
            else if (componentId == 8)
            {
                playerClothes.Unterteil = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 8, playerClothes.Unterteil.drawable, playerClothes.Unterteil.texture);
            }
            else if (componentId == 3)
            {
                playerClothes.Koerper = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 3, playerClothes.Koerper.drawable, playerClothes.Koerper.texture);
            }
            else if (componentId == 4)
            {
                playerClothes.Hose = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 4, playerClothes.Hose.drawable, playerClothes.Hose.texture);
            }
            else if (componentId == 6)
            {
                playerClothes.Schuhe = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 6, playerClothes.Schuhe.drawable, playerClothes.Schuhe.texture);
            }
            else if (componentId == 2)
            {
                playerClothes.Haare = new clothingPart()
                {
                    drawable = Convert.ToInt32(drawableId),
                    texture = Convert.ToInt32(textureId)
                };
                Clothing.PlayerClothes.setClothes(p, 2, playerClothes.Haare.drawable, playerClothes.Haare.texture);
            }

            Database.setClothes(p, playerClothes);
        }

        public static void restoreCustomization(Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT customization, clothes FROM users WHERE name = '" + p.Name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data2 = reader.GetString(0);
                            var data3 = reader.GetString(1);

                            if (data2 != "[]")
                            {

                                PlayerClothes playerClothe = null;

                                CustomizeModel customizeModel = NAPI.Util.FromJson<CustomizeModel>(data2);
                                Dictionary<int, HeadOverlay> nums = new Dictionary<int, HeadOverlay>()
                                {
                                    { 2, CreateHeadOverlay(2, (byte)customizeModel.customization.Appearance[2].Value, 0, customizeModel.customization.Appearance[2].Opacity) },
                                    { 3, CreateHeadOverlay(3, (byte)customizeModel.customization.Appearance[3].Value, 0, customizeModel.customization.Appearance[3].Opacity) },
                                    { 4, CreateHeadOverlay(4, (byte)customizeModel.customization.Appearance[4].Value, 0, customizeModel.customization.Appearance[4].Opacity) },
                                    { 5, CreateHeadOverlay(5, (byte)customizeModel.customization.Appearance[5].Value, 0, customizeModel.customization.Appearance[5].Opacity) },
                                    { 8, CreateHeadOverlay(8, (byte)customizeModel.customization.Appearance[8].Value, 0, customizeModel.customization.Appearance[8].Opacity) }
                                };

                                HeadBlend headBlend = new HeadBlend()
                                {
                                    ShapeFirst = (byte)customizeModel.customization.Parents.MotherShape,
                                    ShapeSecond = (byte)customizeModel.customization.Parents.FatherShape,
                                    ShapeThird = 0,
                                    SkinFirst = (byte)customizeModel.customization.Parents.MotherSkin,
                                    SkinSecond = (byte)customizeModel.customization.Parents.FatherSkin,
                                    SkinThird = 0,
                                    ShapeMix = customizeModel.customization.Parents.Similarity,
                                    SkinMix = customizeModel.customization.Parents.SkinSimilarity,
                                    ThirdMix = 0f
                                };
                                HeadBlend headBlend1 = headBlend;
                                bool gender = customizeModel.customization.Gender == 0;
                                p.SetCustomization(gender, headBlend1, (byte)customizeModel.customization.EyeColor, (byte)customizeModel.customization.Hair.Color, (byte)customizeModel.customization.Hair.HighlightColor, customizeModel.customization.Features.ToArray(), nums, new Decoration[0]);

                                if (data3 == "[]")
                                    return;

                                playerClothe = NAPI.Util.FromJson<PlayerClothes>(data3);
                                p.SetAccessories(0, playerClothe.Hut.drawable, playerClothe.Hut.texture);
                                Clothing.PlayerClothes.setClothes(p, 2, playerClothe.Haare.drawable, playerClothe.Haare.texture);
                                Clothing.PlayerClothes.setClothes(p, 1, playerClothe.Maske.drawable, playerClothe.Maske.texture);
                                Clothing.PlayerClothes.setClothes(p, 11, playerClothe.Oberteil.drawable, playerClothe.Oberteil.texture);
                                Clothing.PlayerClothes.setClothes(p, 8, playerClothe.Unterteil.drawable, playerClothe.Unterteil.texture);
                                Clothing.PlayerClothes.setClothes(p, 3, playerClothe.Koerper.drawable, playerClothe.Koerper.texture);
                                Clothing.PlayerClothes.setClothes(p, 4, playerClothe.Hose.drawable, playerClothe.Hose.texture);
                                Clothing.PlayerClothes.setClothes(p, 6, playerClothe.Schuhe.drawable, playerClothe.Schuhe.texture);

                            }
                            else
                            {
                                NAPI.Task.Run(() => 
                                {
                                    p.TriggerEvent("openWindow", new object[] { "CharacterCreator", "{\"customization\":{\"Gender\":0,\"Parents\":{\"FatherShape\":0,\"MotherShape\":0,\"FatherSkin\":0,\"MotherSkin\":0,\"Similarity\":1,\"SkinSimilarity\":1},\"Features\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"Hair\":{\"Hair\":0,\"Color\":0,\"HighlightColor\":0},\"Appearance\":[{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1},{\"Value\":1,\"Opacity\":1},{\"Value\":5,\"Opacity\":0.4},{\"Value\":0,\"Opacity\":0},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1}],\"EyebrowColor\":0,\"BeardColor\":0,\"EyeColor\":0,\"BlushColor\":0,\"LipstickColor\":0,\"ChestHairColor\":0},\"level\":0}" });
                                    p.Position = new Vector3(402.8664, -996.4108, -99.00027);
                                    p.TriggerEvent("client:respawning");
                                    p.Eval("mp.players.local.setHeading(-185);");
                                    p.Dimension = (uint)new Random().Next(10000, 99999);
                                }, 500);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }

        private static HeadOverlay CreateHeadOverlay(Byte index, Byte color, Byte secondaryColor, float opacity)
        {
            return new HeadOverlay
            {
                Index = index,
                Color = color,
                SecondaryColor = secondaryColor,
                Opacity = opacity
            };
        }

        public static void registerUser(string social, string name, string password, string hwid)
        {
			phonenumber = new Random().Next(11111, 99999);

			money = 75000;

			spindId = new Random().Next(1, 200);

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO users (social, name, password, business, spindId, money, hwid, phonenumber) VALUES ('" + social + "', '" + name + "', '" + BCrypt.Net.BCrypt.HashPassword(password, 12) + "', '" + "null" + "', '" + spindId + "', '" + money + "', '" + hwid + "', '" + phonenumber + "')", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating user: " + ex.Message);
            }
        }

        public static void createAccount(Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO accounts (social, hwid) VALUES ('" + p.SocialClubName + "', '" + p.Serial + "')", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating account: " + ex.Message);
            }
        }

		public static void createPlayerInfo(string name, string address, string memberShip, int phonenumber, string info)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("INSERT INTO personinfo (name, address, memberShip, phonenumber, info) VALUES ('" + name + "', '" + address + "', '" + memberShip + "', '" + phonenumber + "', '" + info + "')", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating personinfo: " + ex.Message);
			}
		}

		public static decimal getWantedsJailTime(string name)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT jailtime FROM wanteds WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if(val3.HasRows)
					{
						while(val3.Read())
						{
							return val3.GetDecimal("jailtime");
						}
					}

				} finally
				{
					val3.Dispose();
				}

			} finally
			{
				val.Dispose();
			}
			return -1;
		}

		public static void createPlayerWanteds(string name, string categorie, string reason, int jailcost, int jailtime)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("INSERT INTO wanteds (name, categorie, reason, jailCost, jailTime) VALUES ('" + name + "', '" + categorie + "', '" + reason + "', '" + jailcost + "', '" + jailtime + "')", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating wanteds: " + ex.Message);
			}
		}

		public static List<Ipad.ReasonModel> GetUserWanteds(string character)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<Ipad.ReasonModel> list = new List<Ipad.ReasonModel>();

			try
			{
				using (var cmd = new MySqlCommand("SELECT * from wanteds WHERE name = '" + character + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var id = reader.GetDecimal(0);
							var name = reader.GetString(1);
							var category = reader.GetString(2);
							var reason = reader.GetString(3);
							var jailcost = reader.GetDecimal(4);
							var jailtime = reader.GetDecimal(5);

							if (!list.Contains(new Ipad.ReasonModel(reason, (int)id, (int)jailcost, (int)jailtime)))
								list.Add(new Ipad.ReasonModel(reason, (int)id, (int)jailcost, (int)jailtime));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating wanteds: " + ex.Message);
			}
			return list;
		}

		public static List<Ipad.PersonDataModel> GetUserInfo(string character)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			List<Ipad.PersonDataModel> list = new List<Ipad.PersonDataModel>();

			try
			{
				using (var cmd = new MySqlCommand("SELECT * from personinfo WHERE name = '" + character + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var name = reader.GetString(1);
							var address = reader.GetString(2);
							var membership = reader.GetString(3);
							var phonenumber = reader.GetDecimal(4);
							var info = reader.GetString(5);

							if (!list.Contains(new Ipad.PersonDataModel(name, address, membership, (int)phonenumber, info)))
								list.Add(new Ipad.PersonDataModel(name, address, membership, (int)phonenumber, info));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating wanteds: " + ex.Message);
			}
			return list;
		}

		public static void deleteAllWanteds(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				using (var cmd = new MySqlCommand("UPDATE wanteds SET reason AND categorie = [] WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating wanteds: " + ex.Message);
			}
		}

		/*public static void addContact(string owner, string display, int number)
		{
			int id = new Random().Next(5, 99999);

			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "INSERT INTO user_contacts (id, displayname, name, number) VALUES (@id ,@display, @name, @number)";
				val2.Parameters.AddWithValue("@display", display);
				val2.Parameters.AddWithValue("@name", owner);
				val2.Parameters.AddWithValue("@number", number);
				val2.Parameters.AddWithValue("@id", id);
				val2.ExecuteNonQuery();
			} finally
			{
				val.Dispose();
			}
		} */


		public static string getDisplayName(string name)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT displayname FROM user_contacts WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
				   if(val3.HasRows)
				   {
						while(val3.Read())
						{
							return val3.GetString("displayname");
						}
				   }

				} finally
				{
					val3.Dispose();
				}
			}
			finally
			{
				val.Dispose();
			}
			return "";
		}

		public static List<ContactModel> getUserContacts(string character)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			List<ContactModel> contacts = new List<ContactModel>();
			try
			{

				using (var cmd = new MySqlCommand("SELECT contact FROM user_contacts WHERE name = '" + character + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{

						if (reader.HasRows)
						{
							while (reader.Read())
							{
								var contactsjson = reader.GetString(0);

								if (contactsjson != "[]")
								{
									return NAPI.Util.FromJson<List<ContactModel>>(contactsjson);
								}

							}
						}
						else
						{
							Notification.SendPlayerNotifcation(NAPI.Player.GetPlayerFromName(character), "Deine Kontaktliste wurde Installiert.", 3500, "green", "KONTAKTE", "");

							createContacts(character);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user items: " + ex.Message);
			}
			return contacts;
		}

		public static void changeUserContact(string account, string name, int phonenumber, bool favorite, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{
				List<ContactModel> list = getUserContacts(account);
				bool found = false;
				ContactModel contactModel = new ContactModel("", 0, false);
				if (remove)
				{
					foreach(ContactModel contact in getUserContacts(account))
					{
						if(contact.name == name)
						{
							list.Remove(contact);
						}
					}

				}
				if (!remove && !found)
				{
					list.Add(new ContactModel(name, phonenumber, favorite));
				}

				using (var cmd = new MySqlCommand("UPDATE user_contacts SET contact = '" + NAPI.Util.ToJson(list) + "' WHERE name = '" + account + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing contacts: " + ex.Message);
			}
		}

		public static void createContacts(string account)
		{

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("INSERT INTO user_contacts (name, contact) VALUES ('" + account + "', '" + NAPI.Util.ToJson(new List<ContactModel>()) + "')", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while creating contacts: " + ex.Message);
			}
		}

		public static decimal getContactNumber(string name)
		{
			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "SELECT number FROM user_contacts WHERE name = @name";
				val2.Parameters.AddWithValue("@name", name);
				MySqlDataReader val3 = val2.ExecuteReader();
				try
				{
					if (val3.HasRows)
					{
						while (val3.Read())
						{
							return val3.GetDecimal("number");
						}
					}

				}
				finally
				{
					val3.Dispose();
				}
			}
			finally
			{
				val.Dispose();
			}
			return -1;
		}

		public static void setDeathStatus(string name, bool value)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            int newvalue = 0;

            if (value)
                newvalue = 1;

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET death = " + newvalue + " WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user deathstate: " + ex.Message);
            }
        }

		public static void updateSMS(string name, string message)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("UPDATE sms SET message = " + message + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing sms Message: " + ex.Message);
			}
		}

		public static string getSMS(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("SELECT message FROM sms WHERE  sender = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing sms Message: " + ex.Message);
			}
			return "";
		}



		public static string getAdText(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{
				using (var cmd = new MySqlCommand("SELECT text FROM ads WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							return reader.GetString(0);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing sms Message: " + ex.Message);
			}
			return "";
		}

		public static decimal getWarns(string social)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT warns FROM accounts WHERE social = '" + social + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var warns = reader.GetDecimal(0);

                            return warns;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user warns: " + ex.Message);
            }
            return 0;
        }

        public static void banPlayer(string socialname, string reason)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE accounts SET banned = 1, banreason = '" + reason + "' WHERE social = '" + socialname + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user ban: " + ex.Message);
            }

        }

        public static void unbanPlayer(string socialname)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE accounts SET banned = 1, banreason = 'null' WHERE social = '" + socialname + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user ban: " + ex.Message);
            }

        }

        public static void changeVehiclePrimaryColor(string plate, Color color)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();


            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET primaryColor = '" + NAPI.Util.ToJson(color) + "' WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing vehicle color: " + ex.Message);
            }

        }

        public static void changeVehicleSecondaryColor(string plate, Color color)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();


            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET secondaryColor = '" + NAPI.Util.ToJson(color) + "' WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing vehicle color: " + ex.Message);
            }

        }

        public static void restoreVehicleColor(Vehicle veh)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT primaryColor, secondaryColor FROM user_vehicles WHERE plate = '" + veh.NumberPlate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Color primary = NAPI.Util.FromJson<Color>(reader.GetString(0));
                            Color secondary = NAPI.Util.FromJson<Color>(reader.GetString(1));

                            veh.CustomPrimaryColor = primary;
                            veh.CustomSecondaryColor = secondary;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle colors: " + ex.Message);
            }

        }

        public static Dictionary<int, int> getVehicleTuning(string plate)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT tuning FROM user_vehicles WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tuning = reader.GetString(0);

                            if(tuning != "[]")
                            {
                                return NAPI.Util.FromJson<Dictionary<int, int>>(tuning); ;
                            }
                            else
                            {
                                return new Dictionary<int, int>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle tuning: " + ex.Message);
            }
            return new Dictionary<int, int>();
        }

        public static void restoreVehicleTuning(Vehicle veh)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT tuning FROM user_vehicles WHERE plate = '" + veh.NumberPlate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tuning = reader.GetString(0);

                            if(tuning != "[]")
                            {
                                Dictionary<int, int> mods = NAPI.Util.FromJson<Dictionary<int, int>>(tuning);

                                foreach (KeyValuePair<int, int> mod in mods)
                                {
                                    veh.SetMod(mod.Key, mod.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting vehicle tuning: " + ex.Message);
            }

        }

        public static void changeVehicleTuning(string plate, int id1, int id2)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            Dictionary<int, int> mods = getVehicleTuning(plate);

            mods[id1] = id2;

            try
            {
                using (var cmd = new MySqlCommand("UPDATE user_vehicles SET tuning = '" + NAPI.Util.ToJson(mods) + "' WHERE plate = '" + plate + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing vehicle tuning: " + ex.Message);
            }

        }

        public static void changeWarns(string socialname, decimal value, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            int newvalue = (int)getWarns(socialname);

            if (remove)
                newvalue = newvalue - (int)value;
            else
                newvalue = newvalue + (int)value;

            try
            {
                using (var cmd = new MySqlCommand("UPDATE accounts SET warns = " + newvalue + " WHERE social = '" + socialname + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user warns: " + ex.Message);
            }

        }

        public static decimal getPlaytime(string social)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT playtime FROM accounts WHERE social = '" + social + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var playtime = reader.GetDecimal(0);

                            return playtime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user playtime: " + ex.Message);
            }
            return 0;
        }

		public static decimal getXP(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT xp FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var xp = reader.GetDecimal(0);

							return xp;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user xp: " + ex.Message);
			}
			return 0;
		}

		public static decimal getEventPoints(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT EventPunkte FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var EventPunkte = reader.GetDecimal(0);

							return EventPunkte;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user EventPunkte: " + ex.Message);
			}
			return 0;
		}

		public static void changeUserXP(string name, decimal value, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			int newvalue = (int)getXP(name);

			if (remove)
				newvalue = newvalue - (int)value;
			else
				newvalue = newvalue + (int)value;

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET xp = " + newvalue + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing user xp: " + ex.Message);
			}

		}

		public static void changeUserEventPoints(string name, decimal value, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			int newvalue = (int)getEventPoints(name);

			if (remove)
				newvalue = newvalue - (int)value;
			else
				newvalue = newvalue + (int)value;

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET EventPunkte = " + newvalue + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing user EventPunkte: " + ex.Message);
			}

		}

		public static void changePlaytime(string socialname, decimal value, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            int newvalue = (int)getPlaytime(socialname);

            if (remove)
                newvalue = newvalue - (int)value;
            else
                newvalue = newvalue + (int)value;

            try
            {
                using (var cmd = new MySqlCommand("UPDATE accounts SET playtime = " + newvalue + " WHERE social = '" + socialname + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user playtime: " + ex.Message);
            }

        }

        public static void changeMoney(string name, decimal value, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            decimal newvalue = getMoney(name) + value;

            if (remove)
                newvalue = getMoney(name) - value;

            Client player = NAPI.Pools.GetAllPlayers().Find((Client t) => t.Name == name);

            if (player != null)
                player.TriggerEvent("updateMoney", newvalue);

            try
            {
                using (var cmd = new MySqlCommand("UPDATE users SET money = " + newvalue + " WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing user money: " + ex.Message);
            }

            Other.Utils.KeyByValue(Start.loggedInPlayers, name).TriggerEvent("overlay:changemoney", getMoney(name));
        }

		public static void changeBusinessMoney(string name, decimal value, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			decimal newvalue = getMoney(name) + value;

			if (remove)
				newvalue = getMoney(name) - value;

			try
			{
				using (var cmd = new MySqlCommand("UPDATE business SET bank = " + newvalue + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing user money: " + ex.Message);
			}

		}

		public static void changeBlackMoney(string name, decimal value, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			decimal newvalue = getBlackMoney(name) + value;

			if (remove)
				newvalue = getBlackMoney(name) - value;

			Client player = NAPI.Pools.GetAllPlayers().Find((Client t) => t.Name == name);

			if (player != null)
				player.TriggerEvent("updateBlackMoney", newvalue);

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET blackmoney = " + newvalue + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing user blackmoney: " + ex.Message);
			}

			Other.Utils.KeyByValue(Start.loggedInPlayers, name).TriggerEvent("overlay:changeblackmoney", getBlackMoney(name));
		}

		public static void changeFraktionMoney(string fraktion, decimal value, bool remove)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            decimal newvalue = getFrakBank(fraktion) + value;

            if (remove)
                newvalue = getFrakBank(fraktion) - value;

            try
            {
                using (var cmd = new MySqlCommand("UPDATE fraktionen SET frakKontostand = " + newvalue + " WHERE fraktionName = '" + fraktion + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while changing fraktion kontostand: " + ex.Message);
            }
        }

		public static void changeUserBank(string name, decimal value, bool remove)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			decimal newvalue = getUserBank(name) + value;

			if (remove)
				newvalue = getUserBank(name) - value;

			try
			{
				using (var cmd = new MySqlCommand("UPDATE users SET bank = " + newvalue + " WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
					}
				}

			}
			catch (Exception ex)
			{
				Log.Write("Error while changing user kontostand: " + ex.Message);
			}
		}


		public static decimal getMoney(string name)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {

                using (var cmd = new MySqlCommand("SELECT money FROM users WHERE name = '" + name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var money = reader.GetDecimal(0);

                            return money;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting user money: " + ex.Message);
            }
            return 0;
        }

		public static decimal getBusinessMoney(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT bank FROM business WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var money = reader.GetDecimal(0);

							return money;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting business money: " + ex.Message);
			}
			return 0;
		}

		public static decimal getBlackMoney(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT blackmoney FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var blackmoney = reader.GetDecimal(0);

							return blackmoney;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user blackmoney: " + ex.Message);
			}
			return 0;
		}

		public static decimal getBank(string name)
		{
			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();

			try
			{

				using (var cmd = new MySqlCommand("SELECT bank FROM users WHERE name = '" + name + "'", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var bank = reader.GetDecimal(0);

							return bank;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting user bank: " + ex.Message);
			}
			return 0;
		}

		public static string getItemURL(string name)
        {
            Item item = ItemManager.itemRegisterList.Find((Item x) => x.Name == name);

            if(ItemManager.weaponImages.ContainsKey(name))
            {
                return ItemManager.weaponImages[name];
            }

            if (item == null)
                return "";

            return item.ImagePath;
        }

		public static int getItemWeight(string name)
		{
			Item item = ItemManager.itemRegisterList.Find((Item x) => x.Name == name);

			if (item == null)
				return 0;

			return item.WeightInG;
		}

		public static void SavePlayer(Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            if (!Other.Paintball.würfelparkPlayers.Contains(p) && p.Position.DistanceTo2D(new Vector3(402.8664, -996.4108, -99.00027)) > 5.0f)
            {

                try
                {
                    using (var cmd = new MySqlCommand("UPDATE users SET location = '" + NAPI.Util.ToJson(p.Position) + "' WHERE name = '" + p.Name + "'", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log.Write("Error while saving user: " + ex.Message);
                }
            }
        }


        public static void giveFraktionVehicle(string fraktion, string model)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO fraktion_vehicles (fraktionName, vehicleModel) VALUES ('" + fraktion + "', '" + model + "')", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating vehicle in fraktion garage: " + ex.Message);
            }
        }

        public static void giveOwnedVehicle(string name, string model, string plate)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO user_vehicles (modelname, plate, owner) VALUES ('" + model + "', '" + plate + "', '" + name + "')", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating vehicle in user garage: " + ex.Message);
            }
        }

        public static string loginUser(string name, string password, Client p)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            name = Functions.RemoveSpecialCharacters(name);

            if (name.Length < 6)
                return "Dein Name muss mehr als 5 Zeichen lang sein.";

            if (!name.Contains("_"))
                return "Dein Name muss einen _ enthalten.";

            if (name.Contains(" "))
                return "Dein Name darf kein Leerzeichen enthalten.";

            try
            {
                if(isUserExists(name))
                {
                    try
                    {

                        using (var cmd = new MySqlCommand("SELECT * FROM users WHERE name = '" + name + "'", connection))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var id = reader.GetDecimal(0);
                                    var social = reader.GetString(1);
                                    var dbname = reader.GetString(2);
                                    var dbpassword = reader.GetString(3);
                                    var serial = reader.GetString(13);

                                    if(p.SocialClubName != social)
                                    {
                                        return "Das ist nicht dein Account.";
                                    }

                                    if(BCrypt.Net.BCrypt.Verify(password, dbpassword))
                                    {
                                        return "Du hast dich erfolgreich eingeloggt.";
                                    }
                                    else
                                    {
                                        return "Das Passwort ist falsch.";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Error while login: " + ex.Message);
                    }
                }
                else
                {
                    int count = 0;

                    try
                    {

                        using (var cmd = new MySqlCommand("SELECT * FROM users WHERE social = '" + p.SocialClubName + "'", connection))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    count++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Error while counting users: " + ex.Message);
                    }
                    if(count < 1)
                    {
                        registerUser(p.SocialClubName.ToString(), name, password, p.Serial);
                        return "Du wurdest erfolgreich registriert.";
                    }
                    else
                    {
                        return "Du hast bereits einen Account.";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating user: " + ex.Message);
            }
            return "Fehler: Bitte kontaktiere den Support.";
        }

        public static void swtichMaintenance()
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            int value = 1;

            if(isMaintenance())
            {
                value = 0;
            }

            try
            {
                using (var cmd = new MySqlCommand("UPDATE settings SET maintenance = " + value, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write("Error while updating maintenance: " + ex.Message);
            }
            
        }

        public static List<ClothingShops.ClothingModel> getClothingDataList()
        {
            List<ClothingShops.ClothingModel> list = new List<ClothingShops.ClothingModel>();

            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM clothingsdata", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString(0);
                            var category = reader.GetString(1);
                            var component = reader.GetDecimal(2);
                            var drawable = reader.GetDecimal(3);
                            var texture = reader.GetDecimal(4);
							var price = reader.GetDecimal("price");

                            if(!list.Contains(new ClothingShops.ClothingModel(name, category, (int)component, (int)drawable, (int)texture, (int)price)))
                                list.Add(new ClothingShops.ClothingModel(name, category, (int)component, (int)drawable, (int)texture, (int)price));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting clothingdata: " + ex.Message);
            }
            return list;
        }

        public static List<Gangwar.Gebiet> getGebietList()
        {
            List<Gangwar.Gebiet> list = new List<Gangwar.Gebiet>();

            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM gebiete", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetDecimal(0);
                            var gebietName = reader.GetString(1);
                            var fraktionName = reader.GetString(2);
                            var position = NAPI.Util.FromJson<Vector3>(reader.GetString(3));
                            var gebietRadius = reader.GetFloat(4);
                            var flagOne = NAPI.Util.FromJson<Vector3>(reader.GetString(5));
                            var flagTwo = NAPI.Util.FromJson<Vector3>(reader.GetString(6));
                            var flagThree = NAPI.Util.FromJson<Vector3>(reader.GetString(7));
                            var flagFour = NAPI.Util.FromJson<Vector3>(reader.GetString(8));

                            if(!list.Contains(new Gangwar.Gebiet(gebietName, fraktionName, position, gebietRadius, flagOne, flagTwo, flagThree, flagFour)))
                                list.Add(new Gangwar.Gebiet(gebietName, fraktionName, position, gebietRadius, flagOne, flagTwo, flagThree, flagFour));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting gebiete: " + ex.Message);
            }
            return list;
        }

		public static List<Fraktionen.Fraktion> getFraktionList()
        {
            List<Fraktionen.Fraktion> list = new List<Fraktionen.Fraktion>();

            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            try
            {

                using (var cmd = new MySqlCommand("SELECT * FROM fraktionen", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetDecimal(0);
                            var fraktionName = reader.GetString(1);
                            var shortName = reader.GetString(2);
                            var spawnPoint = reader.GetString(3);
                            var fraktionsLagerPoint = reader.GetString(4);
							var frakLaborPoint = reader.GetString(5);
                            var fraktionsDimension = reader.GetDecimal(6);
                            var garagePoint = reader.GetString(7);
                            var garageSpawnPoint = reader.GetString(8);
                            var garageSpawnPointRotation = reader.GetFloat(9);
                            var blipColor = reader.GetDecimal(10);
                            var rgbColor = reader.GetString(11);
                            var fraktionsClothes = reader.GetString(12);
                            var isBadFraktion = reader.GetDecimal(13);

                            bool badFrak = false;

                            if (isBadFraktion == 1)
                                badFrak = true;

                            if(!list.Contains(new Fraktionen.Fraktion(fraktionName, shortName, NAPI.Util.FromJson<Vector3>(spawnPoint), NAPI.Util.FromJson<Vector3>(fraktionsLagerPoint), NAPI.Util.FromJson<Vector3>(frakLaborPoint), (int)fraktionsDimension, NAPI.Util.FromJson<Vector3>(garagePoint), NAPI.Util.FromJson<Vector3>(garageSpawnPoint), garageSpawnPointRotation, (int)blipColor, NAPI.Util.FromJson<Color>(rgbColor), NAPI.Util.FromJson<List<ClothingModel>>(fraktionsClothes), badFrak)))
                                list.Add(new Fraktionen.Fraktion(fraktionName, shortName, NAPI.Util.FromJson<Vector3>(spawnPoint), NAPI.Util.FromJson<Vector3>(fraktionsLagerPoint), NAPI.Util.FromJson<Vector3>(frakLaborPoint),(int)fraktionsDimension, NAPI.Util.FromJson<Vector3>(garagePoint), NAPI.Util.FromJson<Vector3>(garageSpawnPoint), garageSpawnPointRotation, (int)blipColor, NAPI.Util.FromJson<Color>(rgbColor), NAPI.Util.FromJson<List<ClothingModel>>(fraktionsClothes), badFrak));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while requesting fraktiondata: " + ex.Message);
            }
            return list;
        }

		public static List<HouseSystem.HouseModel> getHouseList()
		{
			List<HouseSystem.HouseModel> list = new List<HouseSystem.HouseModel>();

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT * FROM house", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							var id = reader.GetDecimal(0);
							var status = reader.GetBoolean(1);
							var owner = reader.GetString(2);
							var interior = reader.GetString(3);
							var position = reader.GetString(4);
							var lagerPosition = reader.GetString(5);
							var exitPosition = reader.GetString(6);
							var locked = reader.GetBoolean(7);
							var cost = reader.GetDecimal(8);
							var hasKeller = reader.GetBoolean(9);
							var isUpgraded = reader.GetBoolean(10);
							var dimension = reader.GetDecimal(11);
							var renter = reader.GetBoolean(12);
							var rentcost = reader.GetDecimal(13);
							var platz = reader.GetDecimal(14);
							var lager = reader.GetString(15);
							var keller = reader.GetString(16);

							if (!list.Contains(new HouseSystem.HouseModel((int)id ,status, owner, NAPI.Util.FromJson<Vector3>(interior), NAPI.Util.FromJson<Vector3>(position), NAPI.Util.FromJson<Vector3>(lagerPosition), NAPI.Util.FromJson<Vector3>(exitPosition), locked, (int)cost, hasKeller, isUpgraded, (int)dimension, renter, (int)rentcost, (int)platz)))
								list.Add(new HouseSystem.HouseModel((int)id, status, owner, NAPI.Util.FromJson<Vector3>(interior), NAPI.Util.FromJson<Vector3>(position), NAPI.Util.FromJson<Vector3>(lagerPosition), NAPI.Util.FromJson<Vector3>(exitPosition), locked, (int)cost, hasKeller, isUpgraded, (int)dimension, renter, (int)rentcost, (int)platz));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting fraktiondata: " + ex.Message);
			}
			return list;
		}

		public static List<Business.ÖlPumpenModel> getOilList()
		{
			List<Business.ÖlPumpenModel> list = new List<Business.ÖlPumpenModel>();

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT * FROM oelpumpe", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							var id = reader.GetDecimal(0);
							var owner = reader.GetString(1);
							var cost = reader.GetDecimal("cost");
							var position = reader.GetString("position");

							if (!list.Contains(new Business.ÖlPumpenModel((int)id, owner, (int)cost, NAPI.Util.FromJson<Vector3>(position))))
								list.Add(new Business.ÖlPumpenModel((int)id, owner, (int)cost, NAPI.Util.FromJson<Vector3>(position)));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting fraktiondata: " + ex.Message);
			}
			return list;
		}

		public static List<LagerhallenSystem.LagerHallenModel> getWareHouseList()
		{
			List<LagerhallenSystem.LagerHallenModel> list = new List<LagerhallenSystem.LagerHallenModel>();

			using MySqlConnection connection = new MySqlConnection(Start.connectionString);
			connection.Open();
			try
			{

				using (var cmd = new MySqlCommand("SELECT * FROM warehouse", connection))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var id = reader.GetDecimal("id");
							var owner = reader.GetString("owner");
							var locked = reader.GetBoolean("locked");
							var storage = reader.GetString("storage");
							var cost = reader.GetDecimal("cost");
							var position = reader.GetString("position");

							if (!list.Contains(new LagerhallenSystem.LagerHallenModel((int)id, owner, locked, storage, (int)cost, NAPI.Util.FromJson<Vector3>(position))))
								list.Add(new LagerhallenSystem.LagerHallenModel((int)id, owner, locked, storage, (int)cost, NAPI.Util.FromJson<Vector3>(position)));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write("Error while requesting warehousedata: " + ex.Message);
			}
			return list;
		}


		public static bool hasFraktionVehicle(string fraktion, string name)
        {
            foreach(VehicleModel vehicle in getFraktionVehicles(fraktion))
            {
                if (vehicle.name.ToLower() == name.ToLower())
                    return true;
            }
            return false;
        }

        public static void createFraktion(string fraktion, string shortName)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO fraktionen (fraktionName, shortName, spawnPoint, fraktionsLagerPoint, fraktionsDimension, garagePoint, garageSpawnPoint, garageSpawnPointRotation, blipColor, rgbColor, fraktionsClothes, isBadFraktion) VALUES ('" + fraktion + "', '" + shortName + "', '" + NAPI.Util.ToJson(new Vector3(0, 0, 0)) + "', '" + NAPI.Util.ToJson(new Vector3(0, 0, 0)) + "', 10, '" + NAPI.Util.ToJson(new Vector3(0, 0, 0)) + "', '" + NAPI.Util.ToJson(new Vector3(0, 0, 0)) + "', 0.0, 0, '" + NAPI.Util.ToJson(new Color(0, 0, 0)) + "', '" + NAPI.Util.ToJson(new List<ClothingModel>() {
                  new ClothingModel("Keine", "Maske", 1, 0, 0, 0),
                  new ClothingModel("Standart Shirt", "Oberteil", 11, 0, 0, 0),
                  new ClothingModel("Keine", "Unterteil", 8, 15, 0, 0),
                  new ClothingModel("Körper 1", "Körper", 3, 0, 0, 0),
                  new ClothingModel("Jogginghose Weiss", "Hose", 4, 5, 0, 0),
                  new ClothingModel("Sneaker Schwarz", "Schuhe", 6, 1, 0, 0)
                }) + "', 1)", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error while creating fraktion: " + ex.Message);
            }
        }

        [RemoteEvent("UpdateCharacterCustomization")]
        public static void saveCustomization(Client p, string json, int price)
        {
            using MySqlConnection connection = new MySqlConnection(Start.connectionString);
            connection.Open();
            customization customizeModel = NAPI.Util.FromJson<customization>(json);
            PlayerClothes playerClothe = new PlayerClothes()
            {
                Maske = new clothingPart()
                {
                    drawable = 0,
                    texture = 0
                }
            };
            PlayerClothes.setClothes(p, 1, playerClothe.Maske.drawable, playerClothe.Maske.texture);
            playerClothe.Hut = new clothingPart()
            {
                drawable = -1,
                texture = 0
            };
            playerClothe.Oberteil = new clothingPart()
            {
                drawable = 1,
                texture = 0
            };
            PlayerClothes.setClothes(p, 11, playerClothe.Oberteil.drawable, playerClothe.Oberteil.texture);
            playerClothe.Haare = new clothingPart()
            {
                drawable = customizeModel.Hair.Hair,
                texture = 0
            };
            PlayerClothes.setClothes(p, 2, playerClothe.Haare.drawable, playerClothe.Haare.texture);
            playerClothe.Unterteil = new clothingPart()
            {
                drawable = 15,
                texture = 0
            };
            PlayerClothes.setClothes(p, 8, playerClothe.Unterteil.drawable, playerClothe.Unterteil.texture);
            playerClothe.Koerper = new clothingPart()
            {
                drawable = 0,
                texture = 0
            };
            PlayerClothes.setClothes(p, 3, playerClothe.Koerper.drawable, playerClothe.Koerper.texture);
            playerClothe.Hose = new clothingPart()
            {
                drawable = 5,
                texture = 0
            };
            PlayerClothes.setClothes(p, 4, playerClothe.Hose.drawable, playerClothe.Hose.texture);
            playerClothe.Schuhe = new clothingPart()
            {
                drawable = 1,
                texture = 0
            };
            PlayerClothes.setClothes(p, 6, playerClothe.Schuhe.drawable, playerClothe.Schuhe.texture);

            try
                {
                using (var cmd = new MySqlCommand("UPDATE users SET customization = '" + string.Concat("{\"customization\":", json, ",\"level\":0}") + "', clothes = '" + NAPI.Util.ToJson(playerClothe) + "' WHERE name = '" + p.Name + "'", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                    }
                }

             }
            catch (Exception ex)
            {
                Log.Write("Error while updating customization: " + ex.Message);
            }

            p.Position = Database.getPlayerPosition(p.Name);
            p.Rotation = new Vector3(0f, 0f, 327f);
            p.Dimension = 0;

        }

    }
}

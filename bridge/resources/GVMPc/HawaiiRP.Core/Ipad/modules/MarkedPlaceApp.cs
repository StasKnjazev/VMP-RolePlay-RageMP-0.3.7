using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	class MarkedPlaceApp : Script
	{
		public static List<OfferModel> offers = new List<OfferModel>();

		public static List<OfferModel> myOffers = new List<OfferModel>();

		public static List<OfferModel> carOffer = new List<OfferModel>();

		public static List<OfferModel> serviceOffer = new List<OfferModel>();

		public static List<OfferModel> houseOffer = new List<OfferModel>();

		[ServerEvent(Event.ResourceStart)]
		public void Start()
		{
			Log.Write("MarketplaceApp | Angebote gecleart!");
		}

		[RemoteEvent("deleteOffer")]
		public void deleteOffer(Client p, int id)
		{
			offers.Remove(offers.Find((OfferModel offer) => offer.phone == Database.getUserPhoneNumber(p.Name).ToString()));
			carOffer.Remove(carOffer.Find((OfferModel offer) => offer.phone == Database.getUserPhoneNumber(p.Name).ToString()));
			houseOffer.Remove(houseOffer.Find((OfferModel offer) => offer.phone == Database.getUserPhoneNumber(p.Name).ToString()));
			serviceOffer.Remove(serviceOffer.Find((OfferModel offer) => offer.phone == Database.getUserPhoneNumber(p.Name).ToString()));
			myOffers.Remove(myOffers.Find((OfferModel offer) => offer.phone == Database.getUserPhoneNumber(p.Name).ToString()));
			Notification.SendPlayerNotifcation(p, "Angebot gelöscht", 5000, "grey", "Gebay", "");
			requestMarketPlaceOffers(p, 0);
			requestMyOffers(p);
		}

		[RemoteEvent("requestMarketplaceCategories")]
		public void requestMarketplaceCategories(Client p)
		{
			try
			{
				p.TriggerEvent("componentServerEvent", new object[3]
				{
					"MarketplaceApp",
					"responseMarketPlaceCategories",
					"[{\"id\":\"1\",\"name\":\"Autos\",\"icon_path\":\"car.png\"}, {\"id\":\"2\",\"name\":\"Immobilien\",\"icon_path\":\"house.png\"}, {\"id\":\"3\",\"name\":\"Dienstleistungen\",\"icon_path\":\"service.png\"}]"
				});


			} catch (Exception ex)
			{
				Log.Write(ex.ToString());
			}
			Log.Write("Test");
		}

		[RemoteEvent("requestMyOffers")]
		public void requestMyOffers(Client c)
		{
			foreach(OfferModel offer in offers)
			{
				if(offer.phone == Database.getUserPhoneNumber(c.Name).ToString())
				{
					myOffers.Add(new OfferModel(offer.categoryId, c.Name, offer.search, offer.phone ,offer.price, offer.description));
				}
			}

			c.TriggerEvent("componentServerEvent", new object[3]
			{
				"MarketplaceMyOffers",
				"responseMyOffers",
				JsonConvert.SerializeObject(myOffers)
			});
			Log.Write("Hallo");
		}

		[RemoteEvent("requestMarketPlaceOffers")]
		public void requestMarketPlaceOffers(Client p, int categoryId)
		{
			try
			{
				if (categoryId == 1)
				{
					p.TriggerEvent("componentServerEvent", new object[3]
					{
						"MarketplaceCategory",
						"responseMarketPlaceOffers",
						JsonConvert.SerializeObject(carOffer)
					});
				} else if(categoryId == 2)
				{
					p.TriggerEvent("componentServerEvent", new object[3]
					{
						"MarketplaceCategory",
						"responseMarketPlaceOffers",
						JsonConvert.SerializeObject(houseOffer)
					});
				} else if(categoryId == 3)
	     		{

					p.TriggerEvent("componentServerEvent", new object[3]
					{
						"MarketplaceCategory",
						"responseMarketPlaceOffers",
						JsonConvert.SerializeObject(serviceOffer)
					});
				}

			} catch (Exception e)
			{
				Log.Write(e.ToString());
			}
		}

		[RemoteEvent("addOffer")]
		public void addOffer(Client c, int id, string name, int price, string desc, bool search)
		{
			int phonenumber = (int)Database.getUserPhoneNumber(c.Name);
			if(id == 1)
			{
				carOffer.Add(new OfferModel(1, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
				myOffers.Add(new OfferModel(1, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
			} else if(id == 2)
			{
				houseOffer.Add(new OfferModel(2, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
				myOffers.Add(new OfferModel(1, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
			} else if(id == 3)
			{
				serviceOffer.Add(new OfferModel(3, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
				myOffers.Add(new OfferModel(1, name, search, Database.getUserPhoneNumber(c.Name).ToString(), price.ToString(), desc));
			}
			foreach(Client p in NAPI.Pools.GetAllPlayers())
			{
				Notification.SendPlayerNotifcation(p, "Es wurde eine neue Anzeige in der Gebay App geschaltet!", 5000, "grey", "Gebay", "");
			}
			requestMarketPlaceOffers(c, id);
			Notification.SendPlayerNotifcation(c, "Angebot erstellt", 5000, "grey", "Gebay", "");
		}
	}
}

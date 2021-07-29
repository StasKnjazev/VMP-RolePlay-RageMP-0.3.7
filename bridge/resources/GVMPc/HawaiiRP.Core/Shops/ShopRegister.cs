using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;
using Newtonsoft.Json;

namespace GVMPc.Shops
{
    class ShopRegister : Script
    {
        public static List<Shop> shopList = new List<Shop>();

		public static int robPrice = 0;

		[ServerEvent(Event.ResourceStart)]
        public void registerShops()
        {
            shopList.Add(new ShopAmmunation());
            shopList.Add(new ShopDavis());
            shopList.Add(new ShopVespucci());
            shopList.Add(new ShopVinewood());
            shopList.Add(new Shop247());
			shopList.Add(new ElektroShop());
			shopList.Add(new ShopSkaterPark());
			shopList.Add(new ShopMirrorpark());
			shopList.Add(new ShopPaletoBay());
			shopList.Add(new ShopBauMarkt());
			shopList.Add(new ShopSandy());
			shopList.Add(new ShopAmmunation2());
			shopList.Add(new ShopAmmunation3());
			shopList.Add(new ShopSandyShores());
			shopList.Add(new ShopHarmony());
			shopList.Add(new ShopAmmunation4());
			shopList.Add(new ShopRucksack());
			shopList.Add(new ShopHochzeit());

            foreach (Shop shop in shopList)
            {
                NAPI.Marker.CreateMarker(1, shop.position, new Vector3(), new Vector3(), 1.0f, new Color((int)byte.MaxValue, 165, 0), false, 0);

                ColShape val = NAPI.ColShape.CreateCylinderColShape(shop.position, 1.4f, 1.4f, 0);
                val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openShop", JsonConvert.SerializeObject(shop)));
                val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um den Shop zu öffnen.", shop.title, "white", 3500));

				if (shop.customBlip > 0U)
                    NAPI.Blip.CreateBlip(shop.customBlip, shop.position, 1f, shop.customBlipColor, shop.title, byte.MaxValue, 0.0f, true, (short)0, 0);
                else
                    NAPI.Blip.CreateBlip(52, shop.position, 1f, (byte)2, shop.title, byte.MaxValue, 0.0f, true, (short)0, 0);
            } 
        }

        [RemoteEvent("openShop")]
        public void openShop(Client p, string shopModel)
        {
            if (shopModel == null)
                return;

            try
            {
                p.TriggerEvent("openWindow", new object[2]
                {
                    "Shop",
                    shopModel
                });
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("shopBuy")]
        public void shopBuy(Client p, string json)
        {
            try
            {
                List<basketItems> _basketItems = NAPI.Util.FromJson<basketShop>(json).basket;
                List<ItemModel> itemModels = new List<ItemModel>();

                int num = 0;
				int count = 0;

                foreach (basketItems basketItem in _basketItems)
                {
                    if (basketItem.price > 0)
                    {
                        num += basketItem.price;
						count += basketItem.count;
                        itemModels.Add(Database.getItemModelByName(basketItem.itemId, basketItem.count));
                    }
                }
				int count2 = (int)Database.getXP(p.Name) + 2;

                if (Database.getMoney(p.Name) >= num)
                {
                        Database.changeMoney(p.Name, num, true);
                        Database.changeUserXP(p.Name, 2, false);
                        p.TriggerEvent("updateXP", count2);
                        Notification.SendPlayerNotifcation(p, "Du hast einige Items gekauft.", 3500, "green", "SHOP", "white");
                        Notification.SendPlayerNotifcation(p, "Für den Einkauf hast du 2 XP erhalten", 4500, "green", "SHOP", "white");
                        foreach (ItemModel itemModel in itemModels)
                        {
                            Database.changeInventoryItem(p.Name, itemModel.Name, count, false);
                        }
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast zu wenig Geld, um diese Items zu kaufen.", 5000, "green", "SHOP", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
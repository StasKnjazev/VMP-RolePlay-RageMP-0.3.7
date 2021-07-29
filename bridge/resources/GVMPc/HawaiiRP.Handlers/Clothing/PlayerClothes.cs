using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Clothing
{
    public class PlayerClothes : Script
    {
        public clothingPart Hut { get; set; }

        public clothingPart Maske { get; set; }

        public clothingPart Haare { get; set; }

        public clothingPart Oberteil { get; set; }

        public clothingPart Unterteil { get; set; }

        public clothingPart Koerper { get; set; }

        public clothingPart Hose { get; set; }

        public clothingPart Schuhe { get; set; }

        public static void setCaillou(Client p)
        {
            try
            {
                Clothing.PlayerClothes playerClothes = new Clothing.PlayerClothes();
                playerClothes.Haare = new Clothing.clothingPart()
                {
                    drawable = 0,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 2, playerClothes.Haare.drawable, playerClothes.Haare.texture);
                playerClothes.Hut = new Clothing.clothingPart()
                {
                    drawable = -1,
                    texture = 0
                };
                p.SetAccessories(0, playerClothes.Hut.drawable, playerClothes.Hut.texture);
                playerClothes.Maske = new Clothing.clothingPart()
                {
                    drawable = 0,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 1, playerClothes.Maske.drawable, playerClothes.Maske.texture);
                playerClothes.Oberteil = new Clothing.clothingPart()
                {
                    drawable = 1,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 11, playerClothes.Oberteil.drawable, playerClothes.Oberteil.texture);
                playerClothes.Unterteil = new Clothing.clothingPart()
                {
                    drawable = 15,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 8, playerClothes.Unterteil.drawable, playerClothes.Unterteil.texture);
                playerClothes.Koerper = new Clothing.clothingPart()
                {
                    drawable = 0,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 3, playerClothes.Koerper.drawable, playerClothes.Koerper.texture);
                playerClothes.Hose = new Clothing.clothingPart()
                {
                    drawable = 5,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 4, playerClothes.Hose.drawable, playerClothes.Hose.texture);
                playerClothes.Schuhe = new Clothing.clothingPart()
                {
                    drawable = 1,
                    texture = 0
                };
                Clothing.PlayerClothes.setClothes(p, 6, playerClothes.Schuhe.drawable, playerClothes.Schuhe.texture);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        public static void setClothes(Client p, int componentId, int drawableId, int textureId)
        {
            try
            {
                p.SetClothes(componentId, drawableId, textureId);
                p.Eval("mp.events.callRemote('sync:changeclothes', " + componentId + ", " + drawableId + ", " + textureId + ");");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        public static void setAdmin(Client p, int id)
        {
            try
            {
                if (p.Model == 1885233650)
                {
                    Clothing.PlayerClothes playerClothes = new Clothing.PlayerClothes();
                    playerClothes.Maske = new Clothing.clothingPart()
                    {
                        drawable = 135,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 1, playerClothes.Maske.drawable, playerClothes.Maske.texture);
                    playerClothes.Oberteil = new Clothing.clothingPart()
                    {
                        drawable = 287,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 11, playerClothes.Oberteil.drawable, playerClothes.Oberteil.texture);
                    playerClothes.Unterteil = new Clothing.clothingPart()
                    {
                        drawable = 15,
                        texture = 0
                    };
                    Clothing.PlayerClothes.setClothes(p, 8, playerClothes.Unterteil.drawable, playerClothes.Unterteil.texture);
                    playerClothes.Koerper = new Clothing.clothingPart()
                    {
                        drawable = 3,
                        texture = 0
                    };
                    Clothing.PlayerClothes.setClothes(p, 3, playerClothes.Koerper.drawable, playerClothes.Koerper.texture);
                    playerClothes.Hose = new Clothing.clothingPart()
                    {
                        drawable = 114,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 4, playerClothes.Hose.drawable, playerClothes.Hose.texture);
                    playerClothes.Schuhe = new Clothing.clothingPart()
                    {
                        drawable = 78,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 6, playerClothes.Schuhe.drawable, playerClothes.Schuhe.texture);
                }
                else
                {
                    Clothing.PlayerClothes playerClothes = new Clothing.PlayerClothes();
                    playerClothes.Maske = new Clothing.clothingPart()
                    {
                        drawable = 135,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 1, playerClothes.Maske.drawable, playerClothes.Maske.texture);
                    playerClothes.Oberteil = new Clothing.clothingPart()
                    {
                        drawable = 300,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 11, playerClothes.Oberteil.drawable, playerClothes.Oberteil.texture);
                    playerClothes.Unterteil = new Clothing.clothingPart()
                    {
                        drawable = 15,
                        texture = 0
                    };
                    Clothing.PlayerClothes.setClothes(p, 8, playerClothes.Unterteil.drawable, playerClothes.Unterteil.texture);
                    playerClothes.Koerper = new Clothing.clothingPart()
                    {
                        drawable = 8,
                        texture = 0
                    };
                    Clothing.PlayerClothes.setClothes(p, 3, playerClothes.Koerper.drawable, playerClothes.Koerper.texture);
                    playerClothes.Hose = new Clothing.clothingPart()
                    {
                        drawable = 121,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 4, playerClothes.Hose.drawable, playerClothes.Hose.texture);
                    playerClothes.Schuhe = new Clothing.clothingPart()
                    {
                        drawable = 82,
                        texture = id
                    };
                    Clothing.PlayerClothes.setClothes(p, 6, playerClothes.Schuhe.drawable, playerClothes.Schuhe.texture);
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
            
        }
    }
}

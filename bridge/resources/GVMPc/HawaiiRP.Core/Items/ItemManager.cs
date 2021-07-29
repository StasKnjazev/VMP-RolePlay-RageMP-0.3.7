using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Other;

namespace GVMPc.Items
{
    class ItemManager : Script
    {
        public static List<Item> itemRegisterList = new List<Item>();
		public static List<string> itemList = new List<string>();
        public static Dictionary<string, string> weaponImages = new Dictionary<string, string>();

        public ItemManager()
        {
            itemRegisterList.Add(new Schutzweste());
            itemRegisterList.Add(new Verbandskasten());
            itemRegisterList.Add(new Waffenkiste());
            itemRegisterList.Add(new Advancedrifle());
            itemRegisterList.Add(new Assaultrifle());
            itemRegisterList.Add(new Pistol());
            itemRegisterList.Add(new Heavypistol());
            itemRegisterList.Add(new Machinepistol());
			itemRegisterList.Add(new PDW());
			itemRegisterList.Add(new Taser());
			itemRegisterList.Add(new MP());
            itemRegisterList.Add(new Ephidrin());
            itemRegisterList.Add(new Meth());
            itemRegisterList.Add(new Eisen());
            itemRegisterList.Add(new Abmeldeschein());
            itemRegisterList.Add(new Absperrung());
            itemRegisterList.Add(new Acetylsalicylsaeure());
            itemRegisterList.Add(new Aderklemme());
            itemRegisterList.Add(new Adipinkonzentrat());
            itemRegisterList.Add(new Adipinprobe());
            itemRegisterList.Add(new Adrenalin());
            itemRegisterList.Add(new Agencycard());
            itemRegisterList.Add(new airtester());
            itemRegisterList.Add(new alarmanlage());
            itemRegisterList.Add(new Alicepack());
            itemRegisterList.Add(new Alteisen());
            itemRegisterList.Add(new Aluminiumbarre());
            itemRegisterList.Add(new Aluminiumerz());
            itemRegisterList.Add(new Ammoadvancedrifle());
            itemRegisterList.Add(new AmmoApPistole());
            itemRegisterList.Add(new AmmoAssaultRifle());
            itemRegisterList.Add(new AmmoAssaultSMG());
            itemRegisterList.Add(new AmmoBullpuprifle());
            itemRegisterList.Add(new AmmoCarbineRifle());
            itemRegisterList.Add(new AmmoCombactRifle());
            itemRegisterList.Add(new AmmoCombatMG());
            itemRegisterList.Add(new AmmoCombatPDW());
            itemRegisterList.Add(new AmmoCombatPistol());
            itemRegisterList.Add(new AmmoGusenberg());
            itemRegisterList.Add(new AmmoHeavyPistol());
            itemRegisterList.Add(new AmmoHeavyShotgun());
            itemRegisterList.Add(new AmmoMachinePistol());
            itemRegisterList.Add(new AmmoMG());
            itemRegisterList.Add(new AmmoMicroSMG());
            itemRegisterList.Add(new AmmoMiniSMG());
            itemRegisterList.Add(new AmmoPistol());
            itemRegisterList.Add(new AmmoPistol50());
            itemRegisterList.Add(new AmmoRevolver());
            itemRegisterList.Add(new AmmoSMG());
            itemRegisterList.Add(new AmmoSniperrifle());
            itemRegisterList.Add(new AmmoSpecialCarbine());
            itemRegisterList.Add(new angel());
            itemRegisterList.Add(new Anmeldeschein());
            itemRegisterList.Add(new apple());
            itemRegisterList.Add(new Aramidfaser());
            itemRegisterList.Add(new Aramidstoff());
            itemRegisterList.Add(new Armypack());
            itemRegisterList.Add(new Aruwana());
            itemRegisterList.Add(new bacardi());
            itemRegisterList.Add(new bacardikiste());
            itemRegisterList.Add(new backpack());
            itemRegisterList.Add(new Barsch());
            itemRegisterList.Add(new basket());
            itemRegisterList.Add(new batteriecell());
            itemRegisterList.Add(new battery());
            itemRegisterList.Add(new Beatmungsgearet());
            itemRegisterList.Add(new beer());
            itemRegisterList.Add(new BenutzKennzeichen());
            itemRegisterList.Add(new BenutztesKondom());
            itemRegisterList.Add(new benzin());
            itemRegisterList.Add(new Benzinfaesser());
            itemRegisterList.Add(new bierkiste());
            itemRegisterList.Add(new blackrose());
            itemRegisterList.Add(new Dietrich());
            itemRegisterList.Add(new Blockade());
            itemRegisterList.Add(new bloodymary());
            itemRegisterList.Add(new bloodymarykiste());
            itemRegisterList.Add(new blueberry());
            itemRegisterList.Add(new blueprint());
            itemRegisterList.Add(new Blumenerde());
            itemRegisterList.Add(new Blumentopf());
            itemRegisterList.Add(new Blutdruckmessgeraet());
            itemRegisterList.Add(new Bolzenschneider());
            itemRegisterList.Add(new Borschtsch());
            itemRegisterList.Add(new bouquet());
            itemRegisterList.Add(new Bronzebarren());
            itemRegisterList.Add(new Brecheisen());
            itemRegisterList.Add(new Brooksten());
            itemRegisterList.Add(new Brooksten_Light());
            itemRegisterList.Add(new Bropack());
            itemRegisterList.Add(new cement());
            itemRegisterList.Add(new champange());
            itemRegisterList.Add(new champangekiste());
            itemRegisterList.Add(new chillichips());
            itemRegisterList.Add(new clothbag());
            itemRegisterList.Add(new clothesbag());
            itemRegisterList.Add(new ChickenNuggets());
            itemRegisterList.Add(new Cobalt());
            itemRegisterList.Add(new Cobalterz());
            itemRegisterList.Add(new cocain());
            itemRegisterList.Add(new cocaineleaf());
            itemRegisterList.Add(new cocaineleafdry());
            itemRegisterList.Add(new cocapack());
            itemRegisterList.Add(new cocapaste());
            itemRegisterList.Add(new Cocktail());
            itemRegisterList.Add(new Cocktail2());
            itemRegisterList.Add(new coffe());
            itemRegisterList.Add(new Cola());
            itemRegisterList.Add(new Cortison());
            itemRegisterList.Add(new CT());
            itemRegisterList.Add(new Defibrillator());
            itemRegisterList.Add(new Desinfektionsmittel());
            itemRegisterList.Add(new Diamonddice());
            itemRegisterList.Add(new DiamondVodka());
            itemRegisterList.Add(new DiamondWater());
            itemRegisterList.Add(new dice());
            itemRegisterList.Add(new drill());
            itemRegisterList.Add(new DrTeddy());
            itemRegisterList.Add(new dryMeertraeubelzweige());
            itemRegisterList.Add(new Duenger());
            itemRegisterList.Add(new easter20201());
            itemRegisterList.Add(new easter20202());
            itemRegisterList.Add(new Ei());
			itemRegisterList.Add(new Ephidrinkonzentrat());
            itemRegisterList.Add(new facialmask());
            itemRegisterList.Add(new Fischekisten());
            itemRegisterList.Add(new fischernetz());
            itemRegisterList.Add(new flashlight());
            itemRegisterList.Add(new Funkgeraet());
            itemRegisterList.Add(new GaspberryPi());
            itemRegisterList.Add(new gelbeente());
            itemRegisterList.Add(new Geldkarte());
            itemRegisterList.Add(new Geldkoffer());
            itemRegisterList.Add(new Geschenkrot());
            itemRegisterList.Add(new giftbasket());
            itemRegisterList.Add(new Glitzerlolly());
            itemRegisterList.Add(new Golddice());
            itemRegisterList.Add(new Goldnugget());
            itemRegisterList.Add(new goldsteintaler());
            itemRegisterList.Add(new Griff());
            itemRegisterList.Add(new grindedweed());
            itemRegisterList.Add(new hacker());
            itemRegisterList.Add(new hackingkit());
            itemRegisterList.Add(new hamburger());
            itemRegisterList.Add(new Hamburgerpack());
            itemRegisterList.Add(new Hookah());
            itemRegisterList.Add(new Hanfknospe());
            itemRegisterList.Add(new hanfplanze());
            itemRegisterList.Add(new hanfpulver());
            itemRegisterList.Add(new holz());
            itemRegisterList.Add(new Holzaxt());
            itemRegisterList.Add(new Holzkisten());
            itemRegisterList.Add(new Holzplanke());
            itemRegisterList.Add(new Holzspaene());
            itemRegisterList.Add(new iVZugang());
            itemRegisterList.Add(new Infusionsschlau());
            itemRegisterList.Add(new Jagertee());
            itemRegisterList.Add(new jewel());
            itemRegisterList.Add(new joint());
            itemRegisterList.Add(new Kaefig());
            itemRegisterList.Add(new Kaufvertrag());
            itemRegisterList.Add(new Kennzeichen());
            itemRegisterList.Add(new KisteAdvanceRifle());
			itemRegisterList.Add(new Reperaturkasten());
			itemRegisterList.Add(new FakeID());
			itemRegisterList.Add(new Personalausweis());
			itemRegisterList.Add(new Orange());
			itemRegisterList.Add(new Orangesaft());
			itemRegisterList.Add(new Trauben());
			itemRegisterList.Add(new Traubensaft());
			itemRegisterList.Add(new FIBWeste());
			itemRegisterList.Add(new Sprengstoff());
			itemRegisterList.Add(new Banknoten());
			itemRegisterList.Add(new Goldbarren());
			itemRegisterList.Add(new Bohrer());
			itemRegisterList.Add(new Handy1());
			itemRegisterList.Add(new Waffenteile());
			itemRegisterList.Add(new Kevlar());
			itemRegisterList.Add(new Fesseln());
			itemRegisterList.Add(new Kokain());
			itemRegisterList.Add(new Kokainblätter());
			itemRegisterList.Add(new PoliceWeste());
			itemRegisterList.Add(new TaskForceWeste());
			itemRegisterList.Add(new Eisenerz());
			itemRegisterList.Add(new Schweißgeraet());
			itemRegisterList.Add(new Tablet());
			itemRegisterList.Add(new Schwarzgeldstapel());
			itemRegisterList.Add(new Magazin());
			itemRegisterList.Add(new Schalldaempfer());
			itemRegisterList.Add(new Laptop());
			itemRegisterList.Add(new Mietvertrag());
			itemRegisterList.Add(new Kröten());
			itemRegisterList.Add(new GehäuteteKröten());

			itemList.Add("Handy");
			itemList.Add("Brecheisen");
			itemList.Add("Laptop");
			itemList.Add("Schutzweste");
            itemList.Add("Advancedrifle");
            itemList.Add("Verbandskasten");
            itemList.Add("Tablet");
            itemList.Add("Fesseln");
			itemList.Add("Mietvertrag");
			itemList.Add("Kroeten");

            //WEAPONS
            weaponImages.Add("Advancedrifle", "AdvanvcedRifle.png");
            weaponImages.Add("Assaultrifle", "Assaultrifle.png");
            weaponImages.Add("Pistol", "Pistol.png");
            weaponImages.Add("Gusenberg", "Gusenberg.png");
            weaponImages.Add("Taser", "Taser.png");
            weaponImages.Add("Heavypistol", "HeavyPistol.png");
			weaponImages.Add("MP", "MicroSMG.png");
			weaponImages.Add("PDW", "PDW.png");
        }

        public static void useItem(Client p, string item)
        {
            if (item == null)
                return;

            try
            {
                if (!Start.loggedInPlayers.ContainsKey(p))
                    return;

                if(Items.Weapons.weapons.ContainsValue(item))
                {
                    Database.changeInventoryItem(p.Name, item, 1, true);
                    p.GiveWeapon(Utils.KeyByValue(Items.Weapons.weapons, item), 0);
                    Database.addLoadout(p.Name, Utils.KeyByValue(Items.Weapons.weapons, item));
                }
                else
                {
                    Item item2 = itemRegisterList.Find((Item x) => x.Name == item);
                    if (Database.getItemCount(p.Name, item) >= 1 && item2.getItemFunction(p))
                    {
                        Database.changeInventoryItem(p.Name, item, 1, true);
                    }
                }

            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}

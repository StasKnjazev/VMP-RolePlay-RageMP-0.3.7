using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    public class Item : Script
    {

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public int WeightInG
        {
            get;
            set;
        }

        public int MaxStackSize
        {
            get;
            set;
        }

        public WeaponHash whash
        {
            get;
            set;
        }

        public Item()
        {
        }

        public virtual bool getItemFunction(Client client)
        {
            return false;
        }

    }
}

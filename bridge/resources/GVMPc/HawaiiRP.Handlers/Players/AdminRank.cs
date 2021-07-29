using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Players
{
    public class AdminRank
    {
        public int rankPermission { get; set; }

        public string rankName { get; set; }

        public int clothId { get; set; }

        public Color rgbColor { get; set; }

        public AdminRank(int rankPermission, string rankName, int clothId, Color rgbColor)
        {
            this.rankPermission = rankPermission;
            this.rankName = rankName;
            this.clothId = clothId;
            this.rgbColor = rgbColor;
        }

    }
}

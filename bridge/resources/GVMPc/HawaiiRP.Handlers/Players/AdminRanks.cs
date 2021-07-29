using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Players
{
    class AdminRanks : Script
    {

        public static List<AdminRank> adminRankList = new List<AdminRank>();

        public static void RegisterAdmins()
        {
            if (adminRankList.Count < 1)
            {
                adminRankList.Add(new AdminRank(100, "Projektleitung", 1, new Color(255, 0, 0)));
                adminRankList.Add(new AdminRank(99, "Stv. Projektleitung", 1, new Color(255, 0, 0)));
                adminRankList.Add(new AdminRank(98, "Management", 11, new Color(227, 121, 0)));
				adminRankList.Add(new AdminRank(97, "Entwicklungsleitung", 13, new Color(277, 121, 0)));
                adminRankList.Add(new AdminRank(96, "Entwickler", 13, new Color(35, 35, 35)));
                adminRankList.Add(new AdminRank(95, "Superadministration", 12, new Color(91, 32, 118)));
                adminRankList.Add(new AdminRank(94, "Administration", 3, new Color(228, 180, 0)));
                adminRankList.Add(new AdminRank(93, "Moderator", 4, new Color(0, 45, 207)));
                adminRankList.Add(new AdminRank(92, "Supporter", 5, new Color(0, 154, 51)));
                adminRankList.Add(new AdminRank(91, "Guide", 5, new Color(193, 162, 208)));
            }
        }

        public static AdminRank getRankFromName(string name)
        {
            foreach(AdminRank adminRank in adminRankList)
            {
                if (adminRank.rankName == name)
                    return adminRank;
            }
            return new AdminRank(0, "User", 0, new Color(0, 0, 0));
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Voice
{
    class Voice : Script
    {
		public static List<int> voiceRanges = new List<int>()
		{
		   5,
		   15,
		   40,
		   60
        };

        [RemoteEvent("Server:Voice:SwitchRange")]
        public void changeVoiceRange(Client p)
        {
            try
            {
                p.TriggerEvent("ConnectTeamspeak", false);
                NAPI.Task.Run(() =>
                {
                    p.TriggerEvent("ConnectTeamspeak", true);
                }, 5000);
            } catch(Exception ex) { Log.Write(ex.Message); }

			try
            {
                int nextRange = 0;
                int index = voiceRanges.IndexOf(p.GetSharedData("voiceRange"));
                if (index == -1 || index == voiceRanges.Count - 1)
                {
                    nextRange = voiceRanges[0];
                }
                else
                {
                    nextRange = voiceRanges[index + 1];
                }
                p.SetSharedData("voiceRange", nextRange);
                p.TriggerEvent("setVoiceType", (index + 1).ToString());
            } catch(Exception ex) { Log.Write(ex.Message);  }
        }
    }
}

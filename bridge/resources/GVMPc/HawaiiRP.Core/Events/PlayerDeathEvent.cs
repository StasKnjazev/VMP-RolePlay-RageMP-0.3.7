using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;
using Newtonsoft.Json;

namespace GVMPc
{
	public class PlayerDeathEvent
	{
		public static List<Client> deathPlayer = new List<Client>();

		public static Timer deathTimer;

		[ServerEvent(Event.ResourceStart)]
		public void Start()
		{
			deathTimer = new Timer(6000.0);
			deathTimer.AutoReset = true;
			deathTimer.Elapsed += delegate (object x, ElapsedEventArgs e)
			{
				OnDeath(null);
			};
		}

		[ServerEvent(Event.PlayerDeath)]
		public void onPlayerDeath(Client c)
		{
			if(!deathPlayer.Contains(c))
			{
				deathPlayer.Add(c);
				Functions.disableAllPlayerControls(c, true);
				c.TriggerEvent("startScreenEffect", "DeathFailOut", 6000, false);
				deathTimer.Start();
			}
		}

		public void OnDeath(object unused)
		{

		}
	}
}

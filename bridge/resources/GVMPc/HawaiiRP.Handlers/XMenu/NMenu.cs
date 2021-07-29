using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.XMenu
{
	public class NMenu : Script
	{
		[RemoteEvent("REQUEST_ANIMATION_USE")]
		public void REQUEST_ANIMATION_USE(Client p, int slot)
		{
			if (!Start.deathTime.ContainsKey(p))
			{

				if (slot == 0)
				{
					NAPI.Player.StopPlayerAnimation(p);
				}
				else if (slot == 1)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_player_intcelebrationfemale@the_woogie", "the_woogie", 8f);
				}
				else if (slot == 2)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "amb@world_human_bum_slumped@male@laying_on_right_side@base", "base", 8f);
				}
				else if (slot == 3)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "amb@world_human_bum_wash@male@low@idle_a", "idle_a", 8f);
				}
				else if (slot == 4)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "mp_player_int_uppergang_sign_a", "mp_player_int_gang_sign_a", 8f);
				}
				else if (slot == 5)
				{
					NAPI.Player.PlayPlayerAnimation(p, 49, "anim@mp_player_intselfiethe_bird", "idle_a", 8f);
				}
				else if (slot == 6)
				{
					NAPI.Player.PlayPlayerAnimation(p, 49, "anim@amb@nightclub@peds@", "rcmme_amanda1_stand_loop_cop", 8f);
				}
				else if (slot == 7)
				{
					NAPI.Player.PlayPlayerAnimation(p, 49, "missheist_jewelleadinout", "jh_int_outro_loop_a", 8f);
				}
				else if (slot == 8)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "amb@world_human_leaning@male@wall@back@hands_together@idle_a", "idle_a", 8f);
				}
				else if (slot == 9)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "random@homelandsecurity", "knees_loop_girl", 8f);
				}
				else if (slot == 10)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "amb@code_human_wander_idles_cop@female@static", "static", 8f);
				}
				else if (slot == 11)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "switch@michael@sitting", "idle", 8f);
				}
			}
		}
	}
}

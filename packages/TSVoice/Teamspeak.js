var tsBrowser = null;
var refresh = 0;
var oldDateTime = 0;

API.onResourceStart.connect(function() {
	var res = API.getScreenResolution();
	tsBrowser = API.createCefBrowser(0, 0, false);
	API.setCefBrowserPosition(tsBrowser, 0, 0);
	API.waitUntilCefBrowserInit(tsBrowser);
});

API.onServerEventTrigger.connect(function (name, args) 
{
	if (name == "ConnectTeamspeak")
	{
		refresh = 1;
	}
});

API.onUpdate.connect(function ()
{
	var dateTime = API.getGameTime();
	if (!API.isCefBrowserLoading(tsBrowser))
	{
		if (refresh == 1 && (dateTime - oldDateTime) >= 500)
		{
			oldDateTime = dateTime;
			var player = API.getLocalPlayer();
			var playerPos = API.getEntityPosition(player);
			var playerRot = API.getGameplayCamRot();
			var rotation = Math.PI / 180 * (playerRot.Z * -1);
			var streamedPlayers = API.getStreamedPlayers();
			var playerNames = new Array();
			
			if (API.hasEntitySyncedData(player, "CALLING_PLAYER_NAME") && API.hasEntitySyncedData(player, "CALL_IS_STARTED") && API.getEntitySyncedData(player, "CALL_IS_STARTED").toString() == "1")
			{
				var callingPlayerName = API.getEntitySyncedData(player, "CALLING_PLAYER_NAME");
				if (callingPlayerName != "")
				{
					playerNames.push(callingPlayerName + "~10~0~0~3");
				}
			}
			for (var i = 0; i < streamedPlayers.Length; i++)
			{
				var streamedPlayerPos = API.getEntityPosition(streamedPlayers[i]);
				var distance = playerPos.DistanceTo(streamedPlayerPos);
				var voiceRange = API.getEntitySyncedData(streamedPlayers[i], "VOICE_RANGE");
				var volumeModifier = 0;
				var range = 25;
				if (voiceRange == "Weit")
				{
					range = 50;
				}
				else if (voiceRange == "Kurz")
				{
					range = 5;
				}
				if (distance > 5)
				{
					volumeModifier = (distance * -5 / 10);
				}
				if (volumeModifier > 0)
				{
					volumeModifier = 0;
				}
				
				if (distance < range)
				{
					var subPos = streamedPlayerPos.Subtract(playerPos);
					
					var x = subPos.X * Math.cos(rotation) - subPos.Y * Math.sin(rotation);
					var y = subPos.X * Math.sin(rotation) + subPos.Y * Math.cos(rotation);
					
					x = x * 10 / range;
					y = y * 10 / range;
					
					var isDeath = API.hasEntitySyncedData(streamedPlayers[i], "IS_DEATH") && API.getEntitySyncedData(streamedPlayers[i], "IS_DEATH");
					if (!isDeath)
					{
						playerNames.push(API.getPlayerName(streamedPlayers[i]) + "~" + (Math.round(x * 1000) / 1000) + "~" + (Math.round(y * 1000) / 1000) + "~0~" + (Math.round(volumeModifier * 1000) / 1000));
					}
				}
			}
			
			API.loadPageCefBrowser(tsBrowser, "http://localhost:15555/players/" + API.getPlayerName(player) + "/" + playerNames.join(";") + "/");
		}
	}
});

API.onResourceStop.connect(function() {
	if (tsBrowser != null) {
		refresh = false;
		API.destroyCefBrowser(tsBrowser);
	}
});
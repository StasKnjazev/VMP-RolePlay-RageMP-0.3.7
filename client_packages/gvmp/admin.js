mp.events.add("kicknotify", (reason) => {
	mp.game.graphics.notify('~r~ Du wurdest gekickt Grund: ' + reason );
  });
  mp.events.add("bannotify", (reason) => {
	mp.game.graphics.notify('~r~ Du wurdest gebannt Grund: ' + reason );
  });
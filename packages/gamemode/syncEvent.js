
mp.events.add("sync:changeclothes", (player, componentId, drawable, texture) => {
	player.changeClothes(componentId, drawable, texture, false, true);
});
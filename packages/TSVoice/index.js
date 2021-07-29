mp.events.add('Server:Voice:SetRange', (player) => {
	player.setVariable('CLIENT_RANGE', 'Normal');
});

let ranges = ['Normal', 'Weit', 'Leise'];

mp.events.add('Server:Voice:SwitchRange', (player) => {
    let nextRange = undefined;
    let index = ranges.indexOf(player.getVariable('CLIENT_RANGE'));
    if (index === -1 || index === ranges.length - 1) {
        nextRange = ranges[0];
    } else {
        nextRange = ranges[index + 1];
    }
    player.setVariable('CLIENT_RANGE', nextRange);
});

mp.events.add("Funk:Talk", (player) => {
    if(player.getVariable("Funk_Frequenz") == 0)
        return;

    player.setVariable("Funk_Talk", true);
    player.playAnimation("random@arrests", "generic_radio_chatter", 5, 49);
});

mp.events.add("Funk:NoTalk", (player) => {
    if(player.getVariable("Funk_Frequenz") == 0)
        return;
    
    player.setVariable("Funk_Talk", false);
    player.stopAnimation();
});

mp.events.add("Funk:Talk2", (player) => {
    if(player.getVariable("Funk_Frequenz") == 0)
        return;

    player.setVariable("Funk_Talk", true);
});

mp.events.add("Funk:NoTalk2", (player) => {
    if(player.getVariable("Funk_Frequenz") == 0)
        return;
    
    player.setVariable("Funk_Talk", false);
});

mp.events.add("Funk:ChangeTalkState", (player, state) => {
    if(player.getVariable("Funk_Frequenz") == 0)
        return;

        player.setVariable("Funk_Talk", state);
});

mp.events.add("server:joinradio", (player, radio) => {
    player.setVariable("Funk_Frequenz", radio);
});

mp.events.add("server:leaveradio", (player) => {
    player.setVariable("Funk_Frequenz", 0);
    player.setVariable("Funk_Talk", false);
});
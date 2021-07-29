var object = null;

const prop = {
propname1: "p_cs_cam_phone", /*Handy 1*/
propname2: "prop_npc_phone_02", /*Handy 2*/
propname3: "prop_phone_proto", /*Handy 3*/

};

mp.events.add("createphoneobject", () => {
    if(object == null){
        object = mp.objects.new(mp.game.joaat(prop.propname2), mp.players.local.position, new mp.Vector3(), 255, 0);
        object.attachTo(mp.players.local.handle, mp.players.local.getBoneIndex(28422), 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, false, false, false, false, 2, true);
    }
});

mp.events.add("destroyphoneobject", () => {
    if(object != null) 
    { 
        object.destroy();
        object = null;
    }
    return true;
});

/*
    Wiki   : https://wiki.gtanet.work/index.php?title=Doors
    Author : https://rage.mp/profile/16958-tampa/
*/

const doors = [
    
    {id: 510, name: 'Bolingbroke Penitentiary Main Enter First Door',      hash:  741314661,   locked: true, position: new mp.Vector3(1844.72, 2608.49, 46.0)},  
    {id: 511, name: 'Bolingbroke Penitentiary Main Enter Second Door',     hash:  741314661,   locked: true, position: new mp.Vector3(1818.252, 2608.384, 46.0)},  
    {id: 512, name: 'Bolingbroke Penitentiary Main Enter Third Door',      hash:  741314661,   locked: true, position: new mp.Vector3(1795.98, 2616.696, 45.565)},  

    // Pacific Bank
    {id: -1, name: 'Pacific Standard Bank Main Safe',                      hash: 961976194,    locked: true, position: new mp.Vector3(254.230, 224.55, 101.87)},

    //LCN

    {id: 600, name: 'LCN Enter Gate',                                      hash: -2125423493, locked: true, position: new mp.Vector3(-1470.892, 68.23928, 52.19585)},
    {id: 600, name: 'LCN Right Gate',                                      hash: -2125423493, locked: true, position: new mp.Vector3(-1613.893, 77.86759, 60.47474)},
    {id: 601, name: 'LCN Enter Door',                                      hash: -1568354151, locked: true, position: new mp.Vector3(-1461.574, 65.44924, 51.80658)},
    {id: 602, name: 'LCN Side Door Left',                                  hash: -1568354151, locked: true, position: new mp.Vector3(-1441.012, 172.1933, 54.7051)},
    {id: 603, name: 'LCN Side Door Middle',                                hash: -1568354151, locked: true, position: new mp.Vector3(-1434.081, 235.8178, 58.929471)},
    {id: 604, name: 'LCN Side Door Right',                                 hash: -1568354151, locked: true, position: new mp.Vector3(-1579.332, 153.2776, 57.58541)},


]

doors.forEach((door) =>
{
    mp.game.object.doorControl(door.hash, door.position.x, door.position.y, door.position.z, door.locked, 0.0, 0.0, 0);
});

mp.keys.bind(0x45, true, () =>
{
    doors.forEach((door) =>
    {
        if(mp.game.gameplay.getDistanceBetweenCoords(door.position.x, door.position.y, door.position.z, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, true) < 2.6)
        {
            door.locked =! door.locked;
            mp.game.object.doorControl(door.hash, door.position.x, door.position.y, door.position.z, door.locked, 0.0, 0.0, 0.0);
            mp.events.callRemote('openDoor');
        }
    });
});
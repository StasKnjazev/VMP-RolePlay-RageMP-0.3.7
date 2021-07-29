using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
    class TeleporterMarker : Script
    {

        public static Dictionary<string, Tuple<Vector3, Vector3>> points = new Dictionary<string, Tuple<Vector3, Vector3>>();

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (points.Count < 1)
            {
                points.Add("das Casino", new Tuple<Vector3, Vector3>(new Vector3(935.6288, 46.594963, 80.09572), new Vector3(1089.6112, 206.51218, -50.099737)));

				foreach (KeyValuePair<string, Tuple<Vector3, Vector3>> current in points)
                {
                    NAPI.Marker.CreateMarker(1, current.Value.Item1, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1.0f, new Color(255, 140, 0), false, 0);
                    NAPI.Marker.CreateMarker(1, current.Value.Item2, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1.0f, new Color(255, 140, 0), false, 0);

                    ColShape val = NAPI.ColShape.CreateCylinderColShape(current.Value.Item1, 1.4f, 1.4f, 0);
                    val.SetData("COLSHAPE_FUNCTION", new FunctionModel("teleportTo", NAPI.Util.ToJson(current.Value.Item2)));
                    val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um " + current.Key + " zu verlassen.", current.Key, "white", 5000));

                    ColShape val2 = NAPI.ColShape.CreateCylinderColShape(current.Value.Item2, 1.4f, 1.4f, 0);
                    val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("teleportTo", NAPI.Util.ToJson(current.Value.Item1)));
                    val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um " + current.Key + " zu betreten.", current.Key, "white", 5000));


					ColShape val3 = NAPI.ColShape.CreateCylinderColShape(current.Value.Item1, 1.4f, 1.4f, 0);
					val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("teleportTo", NAPI.Util.ToJson(current.Value.Item2)));
					val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um " + current.Key + " zu verlassen.", current.Key, "white", 5000));

					ColShape val4 = NAPI.ColShape.CreateCylinderColShape(current.Value.Item2, 1.4f, 1.4f, 0);
					val4.SetData("COLSHAPE_FUNCTION", new FunctionModel("teleportTo", NAPI.Util.ToJson(current.Value.Item1)));
					val4.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um " + current.Key + " zu betreten.", current.Key, "white", 5000));

				}


            }
        }

        [RemoteEvent("teleportTo")]
        public void teleportTo(Client p, string vector)
        {
            if(vector == null)
                    return;

            Vector3 pos = NAPI.Util.FromJson<Vector3>(vector);

            Anticheat.Wait(p); p.Position = pos;
        }

    }
}

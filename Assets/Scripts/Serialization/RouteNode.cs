using System;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public partial class RouteNode
    {
        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "System", System },
                { "LocationName", LocationName },
                { "x", Convert.ToString(Position.x) },
                { "y", Convert.ToString(Position.y) },
                { "Time", Convert.ToString(Time) }
            };
        }

        public static RouteNode FromJson(JSONNode json)
        {
            return new RouteNode
            {
                System = json["System"].Value,
                LocationName = json["LocationName"].Value,
                Position = new Vector2(float.Parse(json["x"].Value), float.Parse(json["y"].Value)),
                Time = DateTime.Parse(json["Time"].Value)
            };
        }
    }
}
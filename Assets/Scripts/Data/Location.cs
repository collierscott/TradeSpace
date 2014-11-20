using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class Location
    {
        public string Name;
        public string System;
        public Vector2 Position;
        public string Image;
        public string Description;
        public float Scale = 1;
        public float Rotation;
        public long Interference;
        public long Radiation;
        public Color Color = Color.white;
        public Vector2 Pivot = Vector2.right;
    }

    public static class Extensions
    {
        public static Gates ToGates(this Location location)
        {
            return (Gates) location;
        }

        public static RouteNode ToRouteNode(this Location location, DateTime time)
        {
            return new RouteNode
            {
                System = location.System,
                LocationName = location.Name,
                Position = location.Position,
                Time = time
            };
        }

        public static RouteNode ToRouteNode(this Location location)
        {
            return location.ToRouteNode(DateTime.UtcNow);
        }
    }
}
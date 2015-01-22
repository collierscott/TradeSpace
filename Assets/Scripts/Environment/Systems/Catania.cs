using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Catania = new SpaceSystem
        {
            Name = Env.SystemNames.Catania,
            Position = new Vector2(1600, 40),
            Color = ColorHelper.GetColor("#00CCFF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Wyvern }
            }
        };
    }
}
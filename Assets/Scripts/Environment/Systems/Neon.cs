using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Neon = new SpaceSystem
        {
            Name = Env.SystemNames.Neon,
            Position = new Vector2(-1700, 1400),
            Color = ColorHelper.GetColor("#00FF40", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Flovex }
            }
        };
    }
}
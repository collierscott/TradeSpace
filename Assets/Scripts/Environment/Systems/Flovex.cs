using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Flovex = new SpaceSystem
        {
            Name = Env.SystemNames.Flovex,
            Position = new Vector2(-1900, 1100),
            Color = ColorHelper.GetColor("#FE2EC8", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Niona },
                new Gates { ConnectedSystem = Env.SystemNames.Neon }
            }
        };
    }
}
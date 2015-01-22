using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Zeta = new SpaceSystem
        {
            Name = Env.SystemNames.Zeta,
            Position = new Vector2(-600, -200),
            Color = ColorHelper.GetColor("#FF6666", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Epsilon },
                new Gates { ConnectedSystem = Env.SystemNames.Eta }
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Vortex = new SpaceSystem
        {
            Name = Env.SystemNames.Vortex,
            Position = new Vector2(-1200, 500),
            Color = ColorHelper.GetColor("#FE2E9A", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Eta },
                new Gates { ConnectedSystem = Env.SystemNames.Chinga },
                new Gates { ConnectedSystem = Env.SystemNames.Hive }
            }
        };
    }
}
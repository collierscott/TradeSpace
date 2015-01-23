using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Hive = new SpaceSystem
        {
            Name = Env.SystemNames.Hive,
            Position = new Vector2(-1500, 300),
            Color = ColorHelper.GetColor("#FACC2E", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Vortex },
                new Gates { ConnectedSystem = Env.SystemNames.Rox },
                new Gates { ConnectedSystem = Env.SystemNames.Cizo }
            }
        };
    }
}
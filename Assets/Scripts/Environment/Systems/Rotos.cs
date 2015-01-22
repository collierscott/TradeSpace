using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Rotos = new SpaceSystem
        {
            Name = Env.SystemNames.Rotos,
            Position = new Vector2(420, 1000),
            Color = ColorHelper.GetColor("#9966FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Alpha },
                new Gates { ConnectedSystem = Env.SystemNames.Union },
                new Gates { ConnectedSystem = Env.SystemNames.Amber },
            }
        };
    }
}
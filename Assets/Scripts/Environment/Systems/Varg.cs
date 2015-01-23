using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Varg = new SpaceSystem
        {
            Name = Env.SystemNames.Varg,
            Position = new Vector2(1800, -570),
            Color = ColorHelper.GetColor("#FA8258", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Zed },
                new Gates { ConnectedSystem = Env.SystemNames.Centra },
                new Gates { ConnectedSystem = Env.SystemNames.Mono },
            }
        };
    }
}
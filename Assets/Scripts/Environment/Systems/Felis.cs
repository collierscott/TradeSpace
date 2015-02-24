using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Felis = new SpaceSystem
        {
            Name = Env.SystemNames.Felis,
            Position = new Vector2(-220, -1350),
            Color = ColorHelper.GetColor("#01A9DB", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Nessa },
                new Gates { ConnectedSystem = Env.SystemNames.Eris },
                new Gates { ConnectedSystem = Env.SystemNames.Atlax }
            }
        };
    }
}
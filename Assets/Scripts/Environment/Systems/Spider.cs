using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Spider = new SpaceSystem
        {
            Name = Env.SystemNames.Spider,
            Position = new Vector2(1000, -380),
            Color = ColorHelper.GetColor("#B40431", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Gamma },
                new Gates { ConnectedSystem = Env.SystemNames.Zed },
                new Gates { ConnectedSystem = Env.SystemNames.Oran }
            }
        };
    }
}
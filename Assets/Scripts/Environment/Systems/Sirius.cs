using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Sirius = new SpaceSystem
        {
            Name = Env.SystemNames.Sirius,
            Position = new Vector2(1120, 440),
            Color = ColorHelper.GetColor("#AAAAAA", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Beta },
                new Gates { ConnectedSystem = Env.SystemNames.Capella },
                new Gates { ConnectedSystem = Env.SystemNames.Wyvern },
            }
        };
    }
}
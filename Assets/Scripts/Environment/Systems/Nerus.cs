using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Nerus = new SpaceSystem
        {
            Name = Env.SystemNames.Nerus,
            Position = new Vector2(1880, 620),
            Color = ColorHelper.GetColor("#00CCFF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Wyvern },
                new Gates { ConnectedSystem = Env.SystemNames.Xavis },
                new Gates { ConnectedSystem = Env.SystemNames.Hemicuda }
            }
        };
    }
}
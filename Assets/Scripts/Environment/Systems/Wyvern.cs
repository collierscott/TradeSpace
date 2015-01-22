using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Wyvern = new SpaceSystem
        {
            Name = Env.SystemNames.Wyvern,
            Position = new Vector2(1480, 220),
            Color = ColorHelper.GetColor("#99CC00", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Sirius },
                new Gates { ConnectedSystem = Env.SystemNames.Catania },
                new Gates { ConnectedSystem = Env.SystemNames.Nerus }
            }
        };
    }
}
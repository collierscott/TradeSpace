using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Oran = new SpaceSystem
        {
            Name = Env.SystemNames.Oran,
            Position = new Vector2(1100, -700),
            Color = ColorHelper.GetColor("#0B4C5F", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Spider },
                new Gates { ConnectedSystem = Env.SystemNames.Centra },
                new Gates { ConnectedSystem = Env.SystemNames.Alan }
            }
        };
    }
}
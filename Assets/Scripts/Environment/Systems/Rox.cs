using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Rox = new SpaceSystem
        {
            Name = Env.SystemNames.Rox,
            Position = new Vector2(-1800, 540),
            Color = ColorHelper.GetColor("#9A2EFE", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Niona },
                new Gates { ConnectedSystem = Env.SystemNames.Hive }
            }
        };
    }
}
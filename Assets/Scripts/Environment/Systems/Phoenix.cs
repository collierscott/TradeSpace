using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Phoenix = new SpaceSystem
        {
            Name = Env.SystemNames.Phoenix,
            Position = new Vector2(-360, 1400),
            Color = ColorHelper.GetColor("#FF4400", 220),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Oreon },
                new Gates { ConnectedSystem = Env.SystemNames.Unity },
                new Gates { ConnectedSystem = Env.SystemNames.Falcon },
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Eta = new SpaceSystem
        {
            Name = Env.SystemNames.Eta,
            Position = new Vector2(-650, 200),
            Color = ColorHelper.GetColor("#CC9900", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Theta },
                new Gates { ConnectedSystem = Env.SystemNames.Zeta },
                new Gates { ConnectedSystem = Env.SystemNames.Vortex }
            }
        };
    }
}
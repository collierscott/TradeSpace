using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Niona = new SpaceSystem
        {
            Name = Env.SystemNames.Niona,
            Position = new Vector2(-1680, 800),
            Color = ColorHelper.GetColor("#01DFD7", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Chinga },
                new Gates { ConnectedSystem = Env.SystemNames.Flovex },
                new Gates { ConnectedSystem = Env.SystemNames.Rox }
            }
        };
    }
}
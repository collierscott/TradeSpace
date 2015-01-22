using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Oreon = new SpaceSystem
        {
            Name = Env.SystemNames.Oreon,
            Position = new Vector2(-400, 900),
            Color = ColorHelper.GetColor("#FFCC00", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Theta },
                new Gates { ConnectedSystem = Env.SystemNames.Phoenix }
            }
        };
    }
}
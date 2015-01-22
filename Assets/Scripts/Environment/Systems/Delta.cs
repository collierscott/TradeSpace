using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Delta = new SpaceSystem
        {
            Name = Env.SystemNames.Delta,
            Position = new Vector2(300, -640),
            Color = ColorHelper.GetColor("#9933FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Gamma },
                new Gates { ConnectedSystem = Env.SystemNames.Epsilon }
            }
        };
    }
}
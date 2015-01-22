using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Epsilon = new SpaceSystem
        {
            Name = Env.SystemNames.Epsilon,
            Position = new Vector2(-250, -600),
            Color = ColorHelper.GetColor("#009999", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Delta },
                new Gates { ConnectedSystem = Env.SystemNames.Zeta }
            }
        };
    }
}
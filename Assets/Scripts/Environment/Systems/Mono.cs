using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Mono = new SpaceSystem
        {
            Name = Env.SystemNames.Mono,
            Position = new Vector2(2300, -600),
            Color = ColorHelper.GetColor("#81DAF5", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Varg },
            }
        };
    }
}
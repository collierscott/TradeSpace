using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Xavis = new SpaceSystem
        {
            Name = Env.SystemNames.Xavis,
            Position = new Vector2(2020, 920),
            Color = ColorHelper.GetColor("#9966FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Nerus }
            }
        };
    }
}
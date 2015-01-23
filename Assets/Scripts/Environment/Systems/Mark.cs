using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Mark = new SpaceSystem
        {
            Name = Env.SystemNames.Mark,
            Position = new Vector2(-1000, 1050),
            Color = ColorHelper.GetColor("#FACC2E", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Chinga }
            }
        };
    }
}
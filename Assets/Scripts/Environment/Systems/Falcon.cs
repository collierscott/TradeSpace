using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Falcon = new SpaceSystem
        {
            Name = Env.SystemNames.Falcon,
            Position = new Vector2(-760, 1500),
            Color = ColorHelper.GetColor("#99FF33", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Phoenix }
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Nessa = new SpaceSystem
        {
            Name = Env.SystemNames.Nessa,
            Position = new Vector2(-440, -980),
            Color = ColorHelper.GetColor("#ACFA58", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Epsilon },
                new Gates { ConnectedSystem = Env.SystemNames.Felis }
            }
        };
    }
}
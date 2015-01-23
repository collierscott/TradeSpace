using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Synta = new SpaceSystem
        {
            Name = Env.SystemNames.Synta,
            Position = new Vector2(1840, -1100),
            Color = ColorHelper.GetColor("#ACFA58", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Centra }
            }
        };
    }
}
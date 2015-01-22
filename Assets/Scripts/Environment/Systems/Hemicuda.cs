using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Hemicuda = new SpaceSystem
        {
            Name = Env.SystemNames.Hemicuda,
            Position = new Vector2(2200, 420),
            Color = ColorHelper.GetColor("#FF3300", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Nerus }
            }
        };
    }
}
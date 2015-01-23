using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Cizo = new SpaceSystem
        {
            Name = Env.SystemNames.Cizo,
            Position = new Vector2(-1600, 0),
            Color = ColorHelper.GetColor("#ACFA58", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Hive }
            }
        };
    }
}
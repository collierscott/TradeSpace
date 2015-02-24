using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Atlax = new SpaceSystem
        {
            Name = Env.SystemNames.Atlax,
            Position = new Vector2(-580, -1720),
            Color = ColorHelper.GetColor("#BDBDBD", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Felis }
            }
        };
    }
}
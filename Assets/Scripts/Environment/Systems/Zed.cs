using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Zed = new SpaceSystem
        {
            Name = Env.SystemNames.Zed,
            Position = new Vector2(1370, -300),
            Color = ColorHelper.GetColor("#7401DF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Spider },
                new Gates { ConnectedSystem = Env.SystemNames.Varg }
            }
        };
    }
}
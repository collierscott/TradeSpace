using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Alan = new SpaceSystem
        {
            Name = Env.SystemNames.Alan,
            Position = new Vector2(1000, -1080),
            Color = ColorHelper.GetColor("#B40486", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Oran },
            }
        };
    }
}
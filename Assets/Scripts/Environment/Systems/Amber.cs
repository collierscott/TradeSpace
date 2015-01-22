using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Amber = new SpaceSystem
        {
            Name = Env.SystemNames.Amber,
            Position = new Vector2(740, 1260),
            Color = ColorHelper.GetColor("#00CCFF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Rotos },
                new Gates { ConnectedSystem = Env.SystemNames.Union }
            }
        };
    }
}
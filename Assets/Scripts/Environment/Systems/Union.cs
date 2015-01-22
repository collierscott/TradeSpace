using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Union = new SpaceSystem
        {
            Name = Env.SystemNames.Union,
            Position = new Vector2(300, 1320),
            Color = ColorHelper.GetColor("#FF99CC", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Rotos },
                new Gates { ConnectedSystem = Env.SystemNames.Amber }
            }
        };
    }
}
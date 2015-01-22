using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Theta = new SpaceSystem
        {
            Name = Env.SystemNames.Theta,
            Position = new Vector2(-250, 600),
            Color = ColorHelper.GetColor("#FF66CC", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Eta },
                new Gates { ConnectedSystem = Env.SystemNames.Alpha },
                new Gates { ConnectedSystem = Env.SystemNames.Oreon }
            }
        };
    }
}
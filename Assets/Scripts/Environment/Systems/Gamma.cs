using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Gamma = new SpaceSystem
        {
            Name = Env.SystemNames.Gamma,
            Position = new Vector2(620, -280),
            Color = ColorHelper.GetColor("#009900", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Beta }, 
                new Gates { ConnectedSystem = Env.SystemNames.Delta },
                new Gates { ConnectedSystem = Env.SystemNames.Spider }, 
            }
        };
    }
}
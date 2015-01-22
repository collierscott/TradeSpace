using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Capella = new SpaceSystem
        {
            Name = Env.SystemNames.Capella,
            Position = new Vector2(1200, 580),
            Color = ColorHelper.GetColor("#6699FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Sirius }
            }
        };
    }
}
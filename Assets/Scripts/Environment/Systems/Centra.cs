using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Centra = new SpaceSystem
        {
            Name = Env.SystemNames.Centra,
            Position = new Vector2(1450, -750),
            Color = ColorHelper.GetColor("#0040FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Oran },
                new Gates { ConnectedSystem = Env.SystemNames.Varg },
            }
        };
    }
}
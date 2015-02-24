using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Unity = new SpaceSystem
        {
            Name = Env.SystemNames.Unity,
            Position = new Vector2(-20, 1580),
            Color = ColorHelper.GetColor("#0066FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Phoenix }
            }
        };
    }
}
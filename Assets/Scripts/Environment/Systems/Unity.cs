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
            Position = new Vector2(-120, 1700),
            Color = ColorHelper.GetColor("#0066FF", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Phoenix,
                    ConnectedSystem = Env.SystemNames.Phoenix,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                }
            }
        };
    }
}
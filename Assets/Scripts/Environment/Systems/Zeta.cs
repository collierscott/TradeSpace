using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Zeta = new SpaceSystem
        {
            Name = Env.SystemNames.Zeta,
            Position = new Vector2(-600, -200),
            Color = ColorHelper.GetColor("#FF6666", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Epsilon,
                    ConnectedSystem = Env.SystemNames.Epsilon,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Eta,
                    ConnectedSystem = Env.SystemNames.Eta,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
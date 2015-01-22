using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Epsilon = new SpaceSystem
        {
            Name = Env.SystemNames.Epsilon,
            Position = new Vector2(-250, -600),
            Color = ColorHelper.GetColor("#009999", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Delta,
                    ConnectedSystem = Env.SystemNames.Delta,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Zeta,
                    ConnectedSystem = Env.SystemNames.Zeta,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
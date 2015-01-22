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
                new Gates
                {
                    Name = Env.SystemNames.Beta,
                    ConnectedSystem = Env.SystemNames.Beta,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Delta,
                    ConnectedSystem = Env.SystemNames.Delta,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Delta = new SpaceSystem
        {
            Name = Env.SystemNames.Delta,
            Position = new Vector2(300, -640),
            Color = ColorHelper.GetColor("#9933FF", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Gamma,
                    ConnectedSystem = Env.SystemNames.Gamma,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Epsilon,
                    ConnectedSystem = Env.SystemNames.Epsilon,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
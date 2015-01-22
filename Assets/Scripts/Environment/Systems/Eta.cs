using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Eta = new SpaceSystem
        {
            Name = Env.SystemNames.Eta,
            Position = new Vector2(-650, 200),
            Color = ColorHelper.GetColor("#CC9900", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Zeta,
                    ConnectedSystem = Env.SystemNames.Zeta,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Theta,
                    ConnectedSystem = Env.SystemNames.Theta,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
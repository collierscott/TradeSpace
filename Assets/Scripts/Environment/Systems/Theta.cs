using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Theta = new SpaceSystem
        {
            Name = Env.SystemNames.Theta,
            Position = new Vector2(-250, 600),
            Color = ColorHelper.GetColor("#FF66CC", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Eta,
                    ConnectedSystem = Env.SystemNames.Eta,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Alpha,
                    ConnectedSystem = Env.SystemNames.Alpha,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Oreon,
                    ConnectedSystem = Env.SystemNames.Oreon,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
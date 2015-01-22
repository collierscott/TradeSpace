using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Oreon = new SpaceSystem
        {
            Name = Env.SystemNames.Oreon,
            Position = new Vector2(-400, 900),
            Color = ColorHelper.GetColor("#FFCC00", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Theta,
                    ConnectedSystem = Env.SystemNames.Theta,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Phoenix,
                    ConnectedSystem = Env.SystemNames.Phoenix,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
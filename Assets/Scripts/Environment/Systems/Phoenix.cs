using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Phoenix = new SpaceSystem
        {
            Name = Env.SystemNames.Phoenix,
            Position = new Vector2(-360, 1400),
            Color = ColorHelper.GetColor("#FF4400", 220),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Oreon,
                    ConnectedSystem = Env.SystemNames.Oreon,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Unity,
                    ConnectedSystem = Env.SystemNames.Unity,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Falcon,
                    ConnectedSystem = Env.SystemNames.Falcon,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                },
            }
        };
    }
}
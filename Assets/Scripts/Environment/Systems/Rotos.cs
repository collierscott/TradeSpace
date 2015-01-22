using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Rotos = new SpaceSystem
        {
            Name = Env.SystemNames.Rotos,
            Position = new Vector2(420, 1000),
            Color = ColorHelper.GetColor("#9966FF", 220),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Alpha,
                    ConnectedSystem = Env.SystemNames.Alpha,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Union,
                    ConnectedSystem = Env.SystemNames.Union,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Amber,
                    ConnectedSystem = Env.SystemNames.Amber,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                },
            }
        };
    }
}
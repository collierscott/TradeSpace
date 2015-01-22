using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Union = new SpaceSystem
        {
            Name = Env.SystemNames.Union,
            Position = new Vector2(300, 1320),
            Color = ColorHelper.GetColor("#FF99CC", 220),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Rotos,
                    ConnectedSystem = Env.SystemNames.Rotos,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                },
                new Gates
                {
                    Name = Env.SystemNames.Amber,
                    ConnectedSystem = Env.SystemNames.Amber,
                    Position = new Vector2(-600, 300),
                    Image = "G01"
                }
            }
        };
    }
}
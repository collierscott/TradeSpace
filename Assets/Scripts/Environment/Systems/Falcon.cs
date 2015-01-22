using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Falcon = new SpaceSystem
        {
            Name = Env.SystemNames.Falcon,
            Position = new Vector2(-800, 1600),
            Color = ColorHelper.GetColor("#99FF33", 180),
            Locations = new List<Location>
            {
                new Gates
                {
                    Name = Env.SystemNames.Phoenix,
                    ConnectedSystem = Env.SystemNames.Phoenix,
                    Position = new Vector2(200, 400),
                    Image = "G01"
                }
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Succubus = new SpaceSystem
        {
            Name = Env.SystemNames.Succubus,
            Position = new Vector2(720, -1680),
            Color = ColorHelper.GetColor("#DF013A", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Eris }
            }
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Eris = new SpaceSystem
        {
            Name = Env.SystemNames.Eris,
            Position = new Vector2(210, -1380),
            Color = ColorHelper.GetColor("#FACC2E", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Mio },
                new Gates { ConnectedSystem = Env.SystemNames.Felis },
                new Gates { ConnectedSystem = Env.SystemNames.Succubus }
            }
        };
    }
}
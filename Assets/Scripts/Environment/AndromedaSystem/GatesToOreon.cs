using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Gates GatesToOreon = new Gates
        {
            Name = Env.SystemNames.Oreon,
            ConnectedSystem = Env.SystemNames.Oreon,
            Position = new Vector2(-600, 300),
            Image = "G01"
        };
    }
}
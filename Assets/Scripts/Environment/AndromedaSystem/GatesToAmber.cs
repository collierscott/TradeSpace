using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Gates GatesToAmber = new Gates
        {
            Name = Env.SystemNames.Amber,
            ConnectedSystem = Env.SystemNames.Amber,
            Position = new Vector2(300, 400),
            Image = "G01"
        };
    }
}
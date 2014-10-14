using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.AmberSystem
{
    internal static partial class Amber
    {
        public static readonly Gates GatesToPhoenix = new Gates
        {
            Name = Env.SystemNames.Phoenix,
            ConnectedSystem = Env.SystemNames.Phoenix,
            Position = new Vector2(300, -200),
            Image = "G01",
            NamePosition = -Vector2.right
        };
    }
}
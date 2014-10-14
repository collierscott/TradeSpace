using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.AmberSystem
{
    internal static partial class Amber
    {
        public static readonly Gates GatesToAndromeda = new Gates
        {
            Name = Env.SystemNames.Andromeda,
            ConnectedSystem = Env.SystemNames.Andromeda,
            Position = new Vector2(-200, -100),
            Image = "G01",
            NamePosition = -Vector2.right
        };
    }
}
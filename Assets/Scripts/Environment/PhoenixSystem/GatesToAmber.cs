using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.PhoenixSystem
{
    internal static partial class Phoenix
    {
        public static readonly Gates GatesToAmber = new Gates
        {
            Name = Env.SystemNames.Amber,
            ConnectedSystem = Env.SystemNames.Amber,
            Position = new Vector2(-200, 200),
            Image = "G01"
        };
    }
}
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.OreonSystem
{
    internal static partial class Oreon
    {
        public static readonly Gates GatesToAndromeda = new Gates
        {
            Name = Env.SystemNames.Andromeda,
            ConnectedSystem = Env.SystemNames.Andromeda,
            Position = new Vector2(-300, 300),
            Image = "G01"
        };
    }
}
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Environment.OreonSystem
{
    internal static partial class Oreon
    {
        public static readonly Planet Osiris = new Planet
        {
            Name = "Osiris",
            Position = new Vector2(-250, -300),
            Image = "25611",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Export = new List<ShopGoods>()
        };
    }
}
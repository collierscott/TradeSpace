using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AmberSystem
{
    internal static partial class Amber
    {
        public static readonly Planet Eqsmion = new Planet
        {
            Name = "Eqsmion",
            Position = new Vector2(-400, -250),
            Image = "8544",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Export = new List<ShopGoods>
            {
                new ShopGoods { Id = GoodsId.Smartphone, Min = 20, Max = 80, Availability = 1 },
            },
            Import = new List<GoodsType> { GoodsType.Food }
        };
    }
}
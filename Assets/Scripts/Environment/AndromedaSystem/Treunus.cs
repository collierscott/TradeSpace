using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Planet Treunus = new Planet
        {
            Name = "Treunus",
            Position = new Vector2(-300, -300),
            Image = "12601",
            Color = ColorHelper.GetColor(116, 205, 230),
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Export = new List<ShopGoods>
            {
                new ShopGoods { Id = GoodsId.Smartphone, Min = 20, Max = 80, Availability = 1 }
            },
            Import = new List<GoodsType> { GoodsType.Food }
        };
    }
}
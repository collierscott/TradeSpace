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
                new ShopGoods { Id = GoodsId.Smartphone, MinPrice = 1, MaxPrice = 1.2f, MinQuantity = 20, MaxQuantity = 80, Availability = 1 },
                new ShopGoods { Id = GoodsId.Water, MinPrice = 1.5f, MaxPrice = 1.8f, Availability = 0 }
            }
        };
    }
}
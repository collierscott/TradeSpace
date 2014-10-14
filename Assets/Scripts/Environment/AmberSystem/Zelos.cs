using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AmberSystem
{
    internal static partial class Amber
    {
        public static readonly Planet Zelos = new Planet
        {
            Name = "Zelos",
            Position = new Vector2(-50, 300),
            Image = "19858",
            Color = ColorHelper.GetColor(255, 73, 249),
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
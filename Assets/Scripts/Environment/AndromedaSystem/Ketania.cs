using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Planet Ketania = new Planet
        {
            Name = "Ketania",
            Position = new Vector2(0, -350),
            Image = "6574",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Export = new List<ShopGoods>
            {
                new ShopGoods { Id = GoodsId.Water, MinPrice = 1, MaxPrice = 1.1f, MinQuantity = 100, MaxQuantity = 200, Availability = 1 },
                new ShopGoods { Id = GoodsId.Fish, MinPrice = 1, MaxPrice = 1.15f, MinQuantity = 40, MaxQuantity = 100, Availability = 1 },
                new ShopGoods { Id = GoodsId.Smartphone, MinPrice = 1.6f, MaxPrice = 1.8f, Availability = 0 }
            }
        };
    }
}
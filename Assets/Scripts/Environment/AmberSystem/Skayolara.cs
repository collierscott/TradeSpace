using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AmberSystem
{
    internal static partial class Amber
    {
        public static readonly Planet Skayolara = new Planet
        {
            Name = "Skayolara",
            Position = new Vector2(200, 0),
            Image = "10891",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Export = new List<ShopGoods>
            {
                new ShopGoods { Id = GoodsId.Water, MinPrice = 1, MaxPrice = 1.1f, MinQuantity = 100, MaxQuantity = 200, Availability = 1 },
                new ShopGoods { Id = GoodsId.Fish, MinPrice = 1, MaxPrice = 1.15f, MinQuantity = 40, MaxQuantity = 100, Availability = 1 },
                new ShopGoods { Id = GoodsId.Smartphone, MinPrice = 1.6f, MaxPrice = 1.8f, Availability = 0 },
            }
        };
    }
}
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
                new ShopGoods { Id = GoodsId.Smartphone, MinPrice = 1, MaxPrice = 1.2f, MinQuantity = 20, MaxQuantity = 80, Availability = 1 },
                new ShopGoods { Id = GoodsId.Water, MinPrice = 1.5f, MaxPrice = 1.8f, Availability = 0 }
            }
        };
    }
}
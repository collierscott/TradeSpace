using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Planet Fobos = new Planet
        {
            Name = "Fobos",
            Position = new Vector2(-10, 400),
            Image = "21600",
            Color = ColorHelper.GetColor(255, 255, 74),
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            DefaultPriceRate = 1.2f,
            Export = new List<ShopGoods>
            {
                new ShopGoods { Id = GoodsId.Smartphone, MinPrice = 1.2, MaxPrice = 1.5f, MinQuantity = 20, MaxQuantity = 80, Availability = 1 },
                new ShopGoods { Id = GoodsId.Water, MinPrice = 1.5f, MaxPrice = 2f, Availability = 0 }
            }
        };
    }
}
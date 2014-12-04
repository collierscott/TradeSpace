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
            Export =
            {
                new ShopGoods { Id = GoodsId.Water, Min = 100, Max = 200, Availability = 1 }
            },
            Import = new List<GoodsType> { GoodsType.Food }
        };
    }
}
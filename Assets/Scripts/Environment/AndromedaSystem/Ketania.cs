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
                new ShopGoods { Id = GoodsId.Water, Min = 100, Max = 200, Availability = 1 },
                new ShopGoods { Id = GoodsId.Fish, Min = 40, Max = 100, Availability = 1 },
            },
            Import = new List<GoodsType> { GoodsType.Electronics }
        };
    }
}
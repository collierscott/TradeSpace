﻿using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Station Station = new Station
        {
            Name = "Highway to Hell",
            Position = new Vector2(0, -200),
            Image = "Station2",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Equipments = new List<ShopEquipment>
            {
                new ShopEquipment { Id = EquipmentId.Armor, MinPrice = 2, MaxPrice = 2.4f, MinQuantity = 2, MaxQuantity = 4, Availability = 1 },
                new ShopEquipment { Id = EquipmentId.EngineReactive, MinPrice = 2, MaxPrice = 3f, MinQuantity = 1, MaxQuantity = 2, Availability = 1 }
            }
        };
    }
}
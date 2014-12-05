using System.Collections.Generic;
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
            Equipment = new List<ShopEquipment>
            {
                new ShopEquipment { Id = EquipmentId.MassKit100, Min = 2, Max = 4, Availability = 1 },
                new ShopEquipment { Id = EquipmentId.JetEngine100, Min = 1, Max = 2, Availability = 1 }
            },
            Ships = new List<ShopShip>
            {
                new ShopShip { Id = ShipId.Rhino, Max = 1, Availability = 0.5f },
                new ShopShip { Id = ShipId.Rover, Max = 1, Availability = 0.5f }
            }
        };
    }
}
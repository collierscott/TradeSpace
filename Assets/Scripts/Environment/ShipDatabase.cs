using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static Dictionary<ShipId, Ship> ShipDatabase = new Dictionary<ShipId, Ship>
        {
            {
                ShipId.ST400,
                new Ship
                {
                    DisplayName = "ST400",
                    Image = "ST400",
                    Description = "SPACETECH Corporation ST500 \"Rover\" - компактная базовая модель транспортного корабля.",
                    Mass = 1600,
                    Volume = 320,
                    Price = 10000,
                    Speed = 60,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 5,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            },
            {
                ShipId.ST500,
                new Ship
                {
                    DisplayName = "ST500",
                    Image = "ST500",
                    Description = "SPACETECH Corporation ST40 \"Rhino\" - самый распространенный транспорт для перевозок на небольшие расстояния.",
                    Mass = 2000,
                    Volume = 400,
                    Price = 12000,
                    Speed = 40,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 6,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            },
            {
                ShipId.ST800,
                new Ship
                {
                    DisplayName = "ST800",
                    Image = "ST800",
                    Description = "SPACETECH Corporation ST40 \"Hippo\" - последняя модель корпорации с увеличенной грузоподъемностью.",
                    Mass = 2200,
                    Volume = 480,
                    Price = 18000,
                    Speed = 70,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 6,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            },
        };
    }
}
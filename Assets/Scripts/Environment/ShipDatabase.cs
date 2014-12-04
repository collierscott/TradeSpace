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
                ShipId.Rhino,
                new Ship
                {
                    DisplayName = "Rhino",
                    Image = "Rhino",
                    Description = "SPACETECH Corporation ST40 \"Rhino\" - popular transport used for short distance shipping.",
                    Mass = 2000,
                    Volume = 400,
                    Price = 10000,
                    Speed = 40,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 5,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            },
            {
                ShipId.Rover,
                new Ship
                {
                    DisplayName = "Rover",
                    Image = "Rover",
                    Description = "X-Enterprise NGX \"Rover\" - compact base shipping model.",
                    Mass = 1600,
                    Volume = 320,
                    Price = 12000,
                    Speed = 60,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 6,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            }
        };
    }
}
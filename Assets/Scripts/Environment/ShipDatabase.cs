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
                    Mass = 100,
                    Volume = 100,
                    Price = 10000,
                    Speed = 40,
                    Armor = 0,
                    Shield = 0,
                    EquipmentSlots = 5,
                    FuelTankCapacity = 20,
                    SupportedEngineTypes = new List<EngineType> { EngineType.Jet },
                    SupportedEngineTopologies = new List<EngineTopology> { EngineTopology.X1 }
                }
            }
        };
    }
}
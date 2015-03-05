using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Ship : Item
    {
        public ShipId Id;
        public long Speed;
        public long Armor;
        public long Shield;
        public long Energy;
        public long Equipment;
        public long FuelTank;
        public List<EngineType> EngineTypes;
        public List<EngineTopology> EngineTopologies;
    }
}
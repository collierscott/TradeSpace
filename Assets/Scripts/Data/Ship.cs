using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Ship : Item
    {
        public ShipId Id;
        public string DisplayName;
        public long Speed;
        public long Armor;
        public long Shield;
        public long EquipmentSlots;
        public long FuelTankCapacity;
        public List<EngineType> SupportedEngineTypes;
        public List<EngineTopology> SupportedEngineTopologies;
    }
}
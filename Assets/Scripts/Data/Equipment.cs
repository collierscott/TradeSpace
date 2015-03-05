using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Equipment : Item
    {
        public EquipmentId Id;
        public EquipmentType Type;
        public long Energy;
        public long Fuel;
        public long Bonus;
        public float BonusM;
        public TransferRequirement TransferRequirement;
    }
}
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Equipment : Item
    {
        public EquipmentId Id;
        public EquipmentType Type;
        public long BonusAdd;
        public long BonusMultiply;
        public TransferRequirement TransferRequirement;
    }
}
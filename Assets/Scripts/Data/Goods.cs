using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Goods : Item
    {
        public GoodsId Id;
        public GoodsType Type;
        public TransferRequirement TransferRequirement;
    }
}
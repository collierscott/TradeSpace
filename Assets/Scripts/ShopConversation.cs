using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using ShopItem = Assets.Scripts.Views.ShopItem;

namespace Assets.Scripts
{
    public static class ShopConversation
    {
        public static ShopItem GetShopItem(MemoGoods item)
        {
            return new ShopItem
            {
                Id = new ProtectedValue(item.Id),
                Quantity = item.Quantity.Copy(),
                Mass = Env.GoodsDatabase[item.Id].Mass,
                Volume = Env.GoodsDatabase[item.Id].Volume,
                Price = item.Price.Copy()
            };
        }

        public static ShopItem GetShopItem(MemoEquipment item)
        {
            return new ShopItem
            {
                Id = new ProtectedValue(item.Id),
                Quantity = item.Quantity.Copy(),
                Mass = Env.EquipmentDatabase[item.Id].Mass,
                Volume = Env.EquipmentDatabase[item.Id].Volume,
                Price = item.Price.Copy()
            };
        }

        public static ShopItem GetShopItem(MemoShipItem item, string disabled = null)
        {
            return new ShopItem
            {
                Id = new ProtectedValue(item.Id),
                Quantity = 1,
                Price = item.Price.Copy()
            };
        }

        public static MemoGoods GetMemoGoods(ShopItem item)
        {
            return new MemoGoods
            {
                Id = item.Id.String.ToEnum<GoodsId>(),
                Quantity = item.Quantity.Copy(),
                Price = item.Price.Copy()
            };
        }

        public static MemoEquipment GetMemoEquipment(ShopItem item)
        {
            return new MemoEquipment
            {
                Id = item.Id.String.ToEnum<EquipmentId>(),
                Quantity = item.Quantity.Copy(),
                Price = item.Price.Copy()
            };
        }

        public static MemoShipItem GetMemoShipItem(ShopItem item)
        {
            return new MemoShipItem
            {
                Id = item.Id.String.ToEnum<ShipId>(),
                Quantity = item.Quantity.Copy(),
                Price = item.Price.Copy()
            };
        }
    }
}
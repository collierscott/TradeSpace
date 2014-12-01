using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Data
{
    public class GenericShopItem
    {
        public ProtectedValue Id;
        public ProtectedValue Quantity;
        public ProtectedValue Mass;
        public ProtectedValue Volume;
        public ProtectedValue Price;
        public ProtectedValue Disabled;

        public GenericShopItem()
        {
        }

        public GenericShopItem(MemoGoods item)
        {
            Id = new ProtectedValue(item.Id);
            Quantity = item.Quantity.Copy();
            Mass = Env.GoodsDatabase[item.Id].Mass;
            Volume = Env.GoodsDatabase[item.Id].Volume;
            Price = item.Price.Copy();
        }

        public GenericShopItem(MemoEquipment item)
        {
            Id = new ProtectedValue(item.Id);
            Quantity = item.Quantity.Copy();
            Mass = Env.EquipmentDatabase[item.Id].Mass;
            Volume = Env.EquipmentDatabase[item.Id].Volume;
            Price = item.Price.Copy();
        }

        public GenericShopItem(MemoShipItem item)
        {
            Id = new ProtectedValue(item.Id);
            Quantity = 1;
            Price = item.Price.Copy();
            Mass = 0;
            Volume = 0;
        }

        public T Extract<T>() where T : MemoItem
        {
            if (typeof(T) == typeof(MemoGoods))
            {
                return (T) (object) new MemoGoods { Id = Id.String.ToEnum<GoodsId>(), Quantity = Quantity.Copy(), Price = Price.Copy() };
            }

            if (typeof(T) == typeof(MemoEquipment))
            {
                return (T) (object) new MemoEquipment { Id = Id.String.ToEnum<EquipmentId>(), Quantity = Quantity.Copy(), Price = Price.Copy() };
            }

            if (typeof(T) == typeof(MemoShipItem))
            {
                return (T) (object) new MemoShipItem { Id = Id.String.ToEnum<ShipId>(), Quantity = Quantity.Copy(), Price = Price.Copy() };
            }

            throw new Exception();
        }
    }
}
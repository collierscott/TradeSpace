using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class GenericShopItem
    {
        public ProtectedValue Id;
        public ProtectedValue Quantity;
        public ProtectedValue Mass;
        public ProtectedValue Volume;
        public ProtectedValue Price;
        public Type Type;
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
            Type = item.GetType();
        }

        public GenericShopItem(MemoEquipment item)
        {
            Id = new ProtectedValue(item.Id);
            Quantity = item.Quantity.Copy();
            Mass = Env.EquipmentDatabase[item.Id].Mass;
            Volume = Env.EquipmentDatabase[item.Id].Volume;
            Type = item.GetType();
        }

        public GenericShopItem(MemoShipItem item)
        {
            Id = new ProtectedValue(item.Id);
            Quantity = 1;
            Mass = 0;
            Volume = 0;
            Type = item.GetType();
        }

        public T Extract<T>() where T : MemoItem
        {
            Debug.Log(Price);

            if (typeof(T) == typeof(MemoGoods))
            {
                return (T) (object) new MemoGoods { Id = GetType<GoodsId>(), Quantity = Quantity.Copy() };
            }

            if (typeof(T) == typeof(MemoEquipment))
            {
                return (T) (object) new MemoEquipment { Id = GetType<EquipmentId>(), Quantity = Quantity.Copy() };
            }

            if (typeof(T) == typeof(MemoShipItem))
            {
                return (T) (object) new MemoShipItem { Id = GetType<ShipId>(), Quantity = Quantity.Copy() };
            }

            throw new Exception();
        }

        public T GetType<T>()
        {
            return Id.String.ToEnum<T>();
        }
    }
}
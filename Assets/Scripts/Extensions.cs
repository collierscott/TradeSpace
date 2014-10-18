using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Extensions
    {
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static ProtectedValue Encrypt(this object value)
        {
            return new ProtectedValue(value);
        }

        public static void Clean(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static bool Contains(this List<MemoGoods> goods, GoodsId id)
        {
            return goods.Any(i => i.Id == id);
        }

        public static MemoGoods Single(this List<MemoGoods> goods, GoodsId id)
        {
            return goods.Single(i => i.Id == id);
        }

        public static MemoGoods SingleOrDefault(this List<MemoGoods> goods, GoodsId id)
        {
            return goods.SingleOrDefault(i => i.Id == id);
        }

        public static bool Contains(this List<MemoEquipment> equipment, EquipmentId id)
        {
            return equipment.Any(i => i.Id == id);
        }

        public static MemoEquipment Single(this List<MemoEquipment> equipment, EquipmentId id)
        {
            return equipment.Single(i => i.Id == id);
        }

        public static MemoEquipment SingleOrDefault(this List<MemoEquipment> equipment, EquipmentId id)
        {
            return equipment.SingleOrDefault(i => i.Id == id);
        }
    }
}
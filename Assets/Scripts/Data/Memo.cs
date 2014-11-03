using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class MemoItem
    {
        public ProtectedValue Quantity = 0;
        public ProtectedValue Price = 0;
    }

    public class MemoAsteroid
    {
        public List<int> EmptyParts;
        public string Name;

        public override string ToString()
        {
            return string.Format("Name:{0}, EmptyParts:{1}", Name,
                EmptyParts!=null ? string.Join(",", EmptyParts.Select(s => s.ToString()).ToArray()):"null");
        }
    }

    public class MemoGoods : MemoItem
    {
        public GoodsId Id;
    }

    public class MemoEquipment : MemoItem
    {
        public EquipmentId Id;
    }

    public class MemoInstalledEquipment
    {
        public EquipmentId Id;
        public long Index;
    }

    public class MemoShop
    {
        public string Id;
        public List<MemoGoods> Goods;
        public List<MemoEquipment> Equipment;
    }

    public class MemoShip
    {
        public ShipId Id;
        public ShipState State;
        public List<RouteNode> Route;
        public List<RouteNode> Trace;
        public List<MemoGoods> Goods;
        public List<MemoEquipment> Equipment;
        public List<MemoInstalledEquipment> InstalledEquipment;
    }
}
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class MemoItem
    {
        public ProtectedValue Quantity = 0.Encrypt();
        public ProtectedValue Price = 0.Encrypt();
    }

    public class MemoAsteroid
    {
        public ProtectedValue Quantity = 0.Encrypt();
        public string Name;
    }

    public class MemoGoods : MemoItem
    {
        public GoodsId Id;
    }

    public class MemoEquipment : MemoItem
    {
        public EquipmentId Id;
    }

    public class MemoInstalledEquipment : MemoEquipment
    {
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
        public List<RouteNode> Trace; // TODO: Disable serialize
        public List<MemoGoods> Goods;
        public List<MemoEquipment> Equipment;
        public List<MemoInstalledEquipment> InstalledEquipment;
    }
}
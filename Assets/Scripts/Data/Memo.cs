using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public partial class MemoItem
    {
        public ProtectedValue Quantity = 0;
    }

    public partial class MemoAsteroid
    {
        public string Name;
        public List<int> EmptyParts;
    }

    public partial class MemoGoods : MemoItem
    {
        public GoodsId Id;
    }

    public partial class MemoEquipment : MemoItem
    {
        public EquipmentId Id;
    }

    public partial class MemoShipItem : MemoItem
    {
        public ShipId Id;
    }

    public partial class MemoInstalledEquipment
    {
        public EquipmentId Id;
        public long Index;
    }

    public partial class MemoShop
    {
        public List<MemoGoods> Goods = new List<MemoGoods>();
        public List<MemoEquipment> Equipment = new List<MemoEquipment>();
        public List<MemoShipItem> Ships = new List<MemoShipItem>();
        public double PriceDelta;
    }

    public partial class MemoWarehouse : MemoShop
    {
    }

    public partial class MemoShip
    {
        public ShipId Id;
        public ProtectedValue UniqName { get; private set; }
        public ShipState State;
        public List<RouteNode> Route = new List<RouteNode>();
        public List<RouteNode> Trace = new List<RouteNode>();
        public List<MemoGoods> Goods = new List<MemoGoods>();
        public List<MemoEquipment> Equipment = new List<MemoEquipment>();
        public List<MemoInstalledEquipment> InstalledEquipment = new List<MemoInstalledEquipment>();

        public MemoShip()
        {
            UniqName = Convert.ToString(CRandom.GetRandom(1000000));
        }
    }
}
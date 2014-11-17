using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using SimpleJSON;

namespace Assets.Scripts.Data
{
    public class MemoItem
    {
        public ProtectedValue Quantity = 0;
        public ProtectedValue Price = 0;
    }

    public class MemoAsteroid
    {
        public string Name;
        public List<int> EmptyParts;

        public override string ToString() // TODO: Remove
        {
            return ToJson();
        }

        public JSONNode ToJson()
        {
            var parts = new JSONArray();

            foreach (var part in EmptyParts)
            {
                parts.Add(Convert.ToString(part));
            }

            return new JSONClass
            {
                { "Name", Name },
                { "EmptyParts", parts }
            };
        }

        public static MemoAsteroid FromJson(JSONNode json)
        {
            return new MemoAsteroid
            {
                Name = json["Name"].Value,
                EmptyParts = json["EmptyParts"].Childs.Select(i => int.Parse(i.Value)).ToList()
            };
        }
    }

    public class MemoGoods : MemoItem
    {
        public GoodsId Id;

        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Quantity", Quantity.ToJson() },
                { "Price", Price.ToJson() }
            };
        }

        public static MemoGoods FromJson(JSONNode json)
        {
            return new MemoGoods
            {
                Id = json["Id"].Value.ToEnum<GoodsId>(),
                Quantity = ProtectedValue.FromJson(json["Quantity"]),
                Price = ProtectedValue.FromJson(json["Price"])
            };
        }
    }

    public class MemoEquipment : MemoItem
    {
        public EquipmentId Id;

        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Quantity", Quantity.ToJson() },
                { "Price", Price.ToJson() }
            };
        }

        public static MemoEquipment FromJson(JSONNode json)
        {
            return new MemoEquipment
            {
                Id = json["Id"].Value.ToEnum<EquipmentId>(),
                Quantity = ProtectedValue.FromJson(json["Quantity"]),
                Price = ProtectedValue.FromJson(json["Price"])
            };
        }
    }

    public class MemoInstalledEquipment
    {
        public EquipmentId Id;
        public long Index;
    }

    public class MemoShop
    {
        public List<MemoGoods> Goods;
        public List<MemoEquipment> Equipment;

        public JSONNode ToJson()
        {
            var goods = new JSONArray();
            var equipment = new JSONArray();

            foreach (var i in Goods)
            {
                goods.Add(i.Id.ToString(), i.ToJson());
            }

            foreach (var i in Equipment)
            {
                equipment.Add(i.Id.ToString(), i.ToJson());
            }

            return new JSONClass
            {
                { "Goods", goods },
                { "Equipment", equipment }
            };
        }

        public static MemoShop FromJson(JSONNode json)
        {
            return new MemoShop
            {
                Goods = json["Goods"].Childs.Select(i => MemoGoods.FromJson(i)).ToList(),
                Equipment = json["Equipment"].Childs.Select(i => MemoEquipment.FromJson(i)).ToList()
            };
        }
    }

    public class MemoWarehouse : MemoShop
    {
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
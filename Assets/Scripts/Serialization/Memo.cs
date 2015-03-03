using System;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using SimpleJSON;

namespace Assets.Scripts.Data
{
    public partial class MemoAsteroid
    {
        public JSONNode ToJson()
        {
            var parts = new JSONArray();

            foreach (var part in Extracted)
            {
                parts.Add(Convert.ToString(part));
            }

            return new JSONClass { { "Extracted", parts } };
        }

        public static MemoAsteroid FromJson(JSONNode json)
        {
            return new MemoAsteroid { Extracted = json["Extracted"].Childs.Select(i => int.Parse(i.Value)).ToList() };
        }
    }

    public partial class MemoGoods
    {
        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Quantity", Quantity.ToJson() }
            };
        }

        public static MemoGoods FromJson(JSONNode json)
        {
            return new MemoGoods
            {
                Id = json["Id"].Value.ToEnum<GoodsId>(),
                Quantity = ProtectedValue.FromJson(json["Quantity"])
            };
        }
    }

    public partial class MemoEquipment
    {
        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Quantity", Quantity.ToJson() }
            };
        }

        public static MemoEquipment FromJson(JSONNode json)
        {
            return new MemoEquipment
            {
                Id = json["Id"].Value.ToEnum<EquipmentId>(),
                Quantity = ProtectedValue.FromJson(json["Quantity"])
            };
        }
    }

    public partial class MemoShipItem
    {
        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Quantity", Quantity.ToJson() }
            };
        }

        public static MemoShipItem FromJson(JSONNode json)
        {
            return new MemoShipItem
            {
                Id = json["Id"].Value.ToEnum<ShipId>(),
                Quantity = ProtectedValue.FromJson(json["Quantity"])
            };
        }
    }

    public partial class MemoInstalledEquipment
    {
        public JSONNode ToJson()
        {
            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "Index", Convert.ToString(Index) }
            };
        }

        public static MemoInstalledEquipment FromJson(JSONNode json)
        {
            return new MemoInstalledEquipment
            {
                Id = json["Id"].Value.ToEnum<EquipmentId>(),
                Index = long.Parse(json["Index"])
            };
        }
    }

    public partial class MemoShop
    {
        public JSONNode ToJson()
        {
            var goods = new JSONArray();
            var equipment = new JSONArray();
            var ships = new JSONArray();

            foreach (var i in Goods)
            {
                goods.Add(i.Id.ToString(), i.ToJson());
            }

            foreach (var i in Equipment)
            {
                equipment.Add(i.Id.ToString(), i.ToJson());
            }

            foreach (var i in Ships)
            {
                ships.Add(i.Id.ToString(), i.ToJson());
            }

            return new JSONClass
            {
                { "Goods", goods },
                { "Equipment", equipment },
                { "Ships", equipment },
                { "PriceDelta", new JSONData(PriceDelta) },
            };
        }

        public static MemoShop FromJson(JSONNode json)
        {
            return new MemoShop
            {
                Goods = json["Goods"].Childs.Select(i => MemoGoods.FromJson(i)).ToList(),
                Equipment = json["Equipment"].Childs.Select(i => MemoEquipment.FromJson(i)).ToList(),
                Ships = json["Ships"].Childs.Select(i => MemoShipItem.FromJson(i)).ToList(),
                PriceDelta = json["PriceDelta"].AsDouble
            };
        }
    }

    public partial class MemoWarehouse
    {
        public new static MemoWarehouse FromJson(JSONNode json)
        {
            return new MemoWarehouse
            {
                Goods = json["Goods"].Childs.Select(i => MemoGoods.FromJson(i)).ToList(),
                Equipment = json["Equipment"].Childs.Select(i => MemoEquipment.FromJson(i)).ToList()
            };
        }
    }

    public partial class MemoShip
    {
        public JSONNode ToJson()
        {
            var route = new JSONArray();
            var trace = new JSONArray();
            var goods = new JSONArray();
            var equipment = new JSONArray();
            var installedEquipment = new JSONArray();

            Route.ForEach(i => route.Add(i.ToJson()));
            Goods.ForEach(i => goods.Add(i.ToJson()));
            Equipment.ForEach(i => equipment.Add(i.ToJson()));
            InstalledEquipment.ForEach(i => installedEquipment.Add(i.ToJson()));

            return new JSONClass
            {
                { "Id", Id.ToString() },
                { "UniqName", UniqName.ToJson() },
                { "State", State.ToString() },
                { "Route", route },
                { "Trace", trace },
                { "Goods", goods },
                { "Equipment", equipment },
                { "InstalledEquipment", installedEquipment }
            };
        }

        public static MemoShip FromJson(JSONNode json)
        {
            return new MemoShip
            {
                Id = json["Id"].Value.ToEnum<ShipId>(),
                UniqName = ProtectedValue.FromJson(json["UniqName"].Value),
                State = json["State"].Value.ToEnum<ShipState>(),
                Route = json["Route"].Childs.Select(i => RouteNode.FromJson(i)).ToList(),
                Trace = json["Trace"].Childs.Select(i => RouteNode.FromJson(i)).ToList(),
                Goods = json["Goods"].Childs.Select(i => MemoGoods.FromJson(i)).ToList(),
                Equipment = json["Equipment"].Childs.Select(i => MemoEquipment.FromJson(i)).ToList(),
                InstalledEquipment = json["InstalledEquipment"].Childs.Select(i => MemoInstalledEquipment.FromJson(i)).ToList(),
            };
        }
    }
}
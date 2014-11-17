using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Engine
{
    public class EnvironmentManager : Script
    {
        public void InitShop(Planet planet)
        {
            Profile.Instance.Shops.Remove(planet.Name);

            var shop = new MemoShop { Goods = new List<MemoGoods>() };

            foreach (var goods in planet.Export)
            {
                if (!CRandom.Chance(goods.Availability)) continue;

                var memoGoods = new MemoGoods
                {
                    Id = goods.Id,
                    Quantity = CRandom.GetRandom(goods.MinQuantity, goods.MaxQuantity),
                    Price = (Env.GoodsDatabase[goods.Id].Price * CRandom.GetRandom(goods.MinPrice, goods.MaxPrice)).RoundToLong()
                };

                shop.Goods.Add(memoGoods);
            }

            Profile.Instance.Shops.Add(planet.Name, shop);
        }

        public void InitShop(Station station)
        {
            Profile.Instance.Shops.Remove(station.Name);

            var shop = new MemoShop { Equipment = new List<MemoEquipment>() };

            foreach (var equipment in station.Equipments)
            {
                if (!CRandom.Chance(equipment.Availability)) continue;

                var memoEquipment = new MemoEquipment
                {
                    Id = equipment.Id,
                    Quantity = CRandom.GetRandom(equipment.MinQuantity, equipment.MaxQuantity),
                    Price = (Env.EquipmentDatabase[equipment.Id].Price * CRandom.GetRandom(equipment.MinPrice, equipment.MaxPrice)).RoundToLong()
                };

                shop.Equipment.Add(memoEquipment);
            }

            Profile.Instance.Shops.Add(station.Name, shop);
        }

        public void InitShops()
        {
            Profile.Instance.Shops = new Dictionary<string, MemoShop>();

            foreach (var planet in Env.Systems.Values.SelectMany(system => system.Values).OfType<Planet>())
            {
                InitShop(planet);
            }

            foreach (var station in Env.Systems.Values.SelectMany(system => system.Values).OfType<Station>())
            {
                InitShop(station);
            }

            //Profile.Instance.InitShopsTime.DateTime = DateTime.UtcNow;
        }
    }
}
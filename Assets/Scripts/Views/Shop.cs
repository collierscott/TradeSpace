using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class Shop : BaseShop
    {
        protected override bool StorageMode
        {
            get { return false; }
        }
        
        protected override void WrapItems()
        {
            var planet = (Data.Planet) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(planet.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(planet);
            }

            LocationItems = Profile.Instance.Shops[planet.Name].Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var planet = (Data.Planet) SelectManager.Location;

            Profile.Instance.Shops[planet.Name].Goods = LocationItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();
        }

        protected override long GetPrice(GenericShopItem item, bool sell)
        {
            return Env.GetPrice(item.GetType<GoodsId>(), SelectManager.Location, sell);
        }
    }
}
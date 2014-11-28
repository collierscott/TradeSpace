using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class ShopView : BaseShopView
    {
        protected override bool Storage
        {
            get { return false; }
        }

        protected override void WrapItems()
        {
            var planet = (Planet) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(planet.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(planet);
            }

            LocationItems = Profile.Instance.Shops[planet.Name].Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in PlayerItems.Values)
            {
                i.Price = LocationItems.ContainsKey(i.Id.String)
                    ? LocationItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * planet.PriceRate).RoundToLong();
            }
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var planet = (Planet) SelectManager.Location;

            Profile.Instance.Shops[planet.Name].Goods = LocationItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();

            foreach (var i in Profile.Instance.Ship.Goods)
            {
                i.Price = 0;
            }
        }
    }
}
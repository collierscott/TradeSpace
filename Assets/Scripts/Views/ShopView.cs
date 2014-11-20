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
        public void Start()
        {
            GetButton.Up += Get;
            PutButton.Up += Put;
        }

        protected override void SyncItems()
        {
            var planet = (Planet) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(planet.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(planet);
            }

            ShopItems = Profile.Instance.Shops[planet.Name].Goods.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);
            ShipItems = Profile.Instance.Ship.Goods.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in ShipItems.Values)
            {
                i.Price = ShopItems.ContainsKey(i.Id.String)
                    ? ShopItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * planet.PriceRate).RoundToLong();
            }
        }

        protected override void SyncItemsBack()
        {
            var planet = (Planet) SelectManager.Location;

            Profile.Instance.Shops[planet.Name].Goods = ShopItems.Values.Select(i => GetMemoGoods(i)).ToList();
            Profile.Instance.Ship.Goods = ShipItems.Values.Select(i => GetMemoGoods(i)).ToList();

            foreach (var item in Profile.Instance.Ship.Goods)
            {
                item.Price = 0;
            }
        }

        protected override void Refresh()
        {
            RefreshPrices();
            base.Refresh();
        }
    }
}
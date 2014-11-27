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
        protected override void WrapItems()
        {
            var planet = (Planet) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(planet.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(planet);
            }

            LocationItems = Profile.Instance.Shops[planet.Name].Goods.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Goods.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in PlayerItems.Values)
            {
                i.Price = LocationItems.ContainsKey(i.Id.String)
                    ? LocationItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * planet.PriceRate).RoundToLong();
            }
        }

        protected override void ExtractItems()
        {
            var planet = (Planet) SelectManager.Location;

            Profile.Instance.Shops[planet.Name].Goods = LocationItems.Values.Select(i => ShopConversation.GetMemoGoods(i)).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values.Select(i => ShopConversation.GetMemoGoods(i)).ToList();

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
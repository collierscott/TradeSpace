using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class EquipmentShopView : BaseShopView
    {
        protected override void WrapItems()
        {
            var station = (Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            LocationItems = Profile.Instance.Shops[station.Name].Equipment.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Equipment.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in PlayerItems.Values)
            {
                i.Price = LocationItems.ContainsKey(i.Id.String)
                    ? LocationItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * station.PriceRate).RoundToLong();
            }
        }

        protected override void ExtractItems()
        {
            var station = (Station)SelectManager.Location;

            Profile.Instance.Shops[station.Name].Equipment = LocationItems.Values.Select(i => ShopConversation.GetMemoEquipment(i)).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values.Select(i => ShopConversation.GetMemoEquipment(i)).ToList();

            foreach (var item in Profile.Instance.Ship.Equipment)
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
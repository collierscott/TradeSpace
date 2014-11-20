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
        public void Start()
        {
            GetButton.Up += Get;
            PutButton.Up += Put;
        }

        protected override void SyncItems()
        {
            var station = (Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            ShopItems = Profile.Instance.Shops[station.Name].Equipment.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);
            ShipItems = Profile.Instance.Ship.Equipment.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in ShipItems.Values)
            {
                i.Price = ShopItems.ContainsKey(i.Id.String)
                    ? ShopItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * station.PriceRate).RoundToLong();
            }
        }

        protected override void SyncItemsBack()
        {
            var station = (Station)SelectManager.Location;

            Profile.Instance.Shops[station.Name].Equipment = ShopItems.Values.Select(i => GetMemoEquipment(i)).ToList();
            Profile.Instance.Ship.Equipment = ShipItems.Values.Select(i => GetMemoEquipment(i)).ToList();

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
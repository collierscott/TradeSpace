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
        protected override bool Storage
        {
            get { return false; }
        }

        protected override void WrapItems()
        {
            var station = (Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            LocationItems = Profile.Instance.Shops[station.Name].Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);

            foreach (var i in PlayerItems.Values)
            {
                i.Price = LocationItems.ContainsKey(i.Id.String)
                    ? LocationItems[i.Id.String].Price.Copy()
                    : (Env.GoodsDatabase[i.Id.String.ToEnum<GoodsId>()].Price * station.PriceRate).RoundToLong();
            }
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var station = (Station)SelectManager.Location;

            Profile.Instance.Shops[station.Name].Equipment = LocationItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();

            foreach (var i in Profile.Instance.Ship.Equipment)
            {
                i.Price = 0;
            }
        }
    }
}
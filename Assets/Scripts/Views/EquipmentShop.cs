using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class EquipmentShop : BaseShop
    {
        protected override bool StorageMode
        {
            get { return false; }
        }

        protected override void WrapItems()
        {
            var station = (Data.Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            LocationItems = Profile.Instance.Shops[station.Name].Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var station = (Data.Station)SelectManager.Location;

            Profile.Instance.Shops[station.Name].Equipment = LocationItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();
        }

        protected override long GetPrice(GenericShopItem item, bool sell)
        {
            return Env.GetPrice(item.GetType<EquipmentId>(), SelectManager.Location, sell);
        }
    }
}
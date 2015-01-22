using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class Storage : BaseShop
    {
        protected override bool StorageMode
        {
            get { return true; }
        }

        protected override void WrapItems()
        {
            var location = SelectManager.Location.Name;

            if (!Profile.Instance.Warehouses.ContainsKey(location))
            {
                Profile.Instance.Warehouses.Add(location, new MemoWarehouse());
            }

            var locationItems = Profile.Instance.Warehouses[location].Goods.Select(i => new GenericShopItem(i)).ToList();
            var playerItems = Profile.Instance.Ship.Goods.Select(i => new GenericShopItem(i)).ToList();

            locationItems.AddRange(Profile.Instance.Warehouses[location].Equipment.Select(i => new GenericShopItem(i)));
            playerItems.AddRange(Profile.Instance.Ship.Equipment.Select(i => new GenericShopItem(i)));

            LocationItems = locationItems.ToDictionary(i => i.Id.String);
            PlayerItems = playerItems.ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Goods = LocationItems.Values
                .Where(i => i.Type == typeof(MemoGoods))
                .Select(i => i.Extract<MemoGoods>()).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values
                .Where(i => i.Type == typeof(MemoGoods))
                .Select(i => i.Extract<MemoGoods>()).ToList();

            Profile.Instance.Warehouses[location].Equipment = LocationItems.Values
                .Where(i => i.Type == typeof(MemoEquipment))
                .Select(i => i.Extract<MemoEquipment>()).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values
                .Where(i => i.Type == typeof(MemoEquipment))
                .Select(i => i.Extract<MemoEquipment>()).ToList();
        }

        protected override long GetPrice(GenericShopItem item, bool sell)
        {
            return 0;
        }
    }
}
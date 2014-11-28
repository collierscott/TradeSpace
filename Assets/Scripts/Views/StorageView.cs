using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class StorageView : BaseShopView
    {
        protected override bool Storage
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

            LocationItems = Profile.Instance.Warehouses[location].Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Goods.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Goods = LocationItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values.Select(i => i.Extract<MemoGoods>()).ToList();
        }
    }
}
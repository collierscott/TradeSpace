using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class EquipmentStorageView : BaseShopView
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

            LocationItems = Profile.Instance.Warehouses[location].Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Equipment.Select(i => new GenericShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Equipment = LocationItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values.Select(i => i.Extract<MemoEquipment>()).ToList();
        }
    }
}
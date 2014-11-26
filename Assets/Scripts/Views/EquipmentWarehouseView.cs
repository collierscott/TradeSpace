using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class EquipmentWarehouseView : BaseWarehouseView
    {
        public void Start()
        {
            GetButton.Up += Get;
            PutButton.Up += Put;
        }

        protected override void SyncItems()
        {
            var location = SelectManager.Location.Name;

            if (!Profile.Instance.Warehouses.ContainsKey(location))
            {
                Profile.Instance.Warehouses.Add(location, new MemoWarehouse());
            }

            ShopItems = Profile.Instance.Warehouses[location].Equipment.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);
            ShipItems = Profile.Instance.Ship.Equipment.Select(i => GetShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void SyncItemsBack()
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Equipment = ShopItems.Values.Select(i => GetMemoEquipment(i)).ToList();
            Profile.Instance.Ship.Equipment = ShipItems.Values.Select(i => GetMemoEquipment(i)).ToList();
        }
    }
}
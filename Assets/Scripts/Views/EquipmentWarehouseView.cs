using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class EquipmentWarehouseView : BaseWarehouseView
    {
        protected override void WrapItems()
        {
            var location = SelectManager.Location.Name;

            if (!Profile.Instance.Warehouses.ContainsKey(location))
            {
                Profile.Instance.Warehouses.Add(location, new MemoWarehouse());
            }

            LocationItems = Profile.Instance.Warehouses[location].Equipment.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Equipment.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems()
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Equipment = LocationItems.Values.Select(i => ShopConversation.GetMemoEquipment(i)).ToList();
            Profile.Instance.Ship.Equipment = PlayerItems.Values.Select(i => ShopConversation.GetMemoEquipment(i)).ToList();
        }
    }
}
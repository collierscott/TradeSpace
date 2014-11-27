using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class WarehouseView : BaseWarehouseView
    {
        protected override void WrapItems()
        {
            var location = SelectManager.Location.Name;

            if (!Profile.Instance.Warehouses.ContainsKey(location))
            {
                Profile.Instance.Warehouses.Add(location, new MemoWarehouse());
            }

            LocationItems = Profile.Instance.Warehouses[location].Goods.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
            PlayerItems = Profile.Instance.Ship.Goods.Select(i => ShopConversation.GetShopItem(i)).ToDictionary(i => i.Id.String);
        }

        protected override void ExtractItems()
        {
            var location = SelectManager.Location.Name;

            Profile.Instance.Warehouses[location].Goods = LocationItems.Values.Select(i => ShopConversation.GetMemoGoods(i)).ToList();
            Profile.Instance.Ship.Goods = PlayerItems.Values.Select(i => ShopConversation.GetMemoGoods(i)).ToList();

            foreach (var item in Profile.Instance.Ship.Goods)
            {
                item.Price = 0;
            }
        }
    }
}
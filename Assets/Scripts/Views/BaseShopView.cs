using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public class ShopItem
    {
        public ProtectedValue Id;
        public ProtectedValue Quantity;
        public ProtectedValue Mass;
        public ProtectedValue Volume;
        public ProtectedValue Price;
        public ProtectedValue Disabled;
    }

    public abstract class BaseShopView : BaseWarehouseView
    {
        public UILabel BuyPriceText;
        public UILabel SellPriceText;

        protected override void SelectItem(ProtectedValue id)
        {
            base.SelectItem(id);
            RefreshPrices();
        }

        protected override void RefreshButtons()
        {
            base.RefreshButtons();

            GetButton.Enabled &= Selected != null && LocationItems.ContainsKey(Selected.String)
                && Profile.Instance.Credits >= LocationItems[Selected.String].Price;
        }

        protected override void Move(Dictionary<string, ShopItem> source, Dictionary<string, ShopItem> destination, ProtectedValue id, bool sell)
        {
            var price = source[id.String].Price;

            base.Move(source, destination, id, sell);
           
            if (sell)
            {
                Profile.Instance.Credits += GetShopPrice(price);
            }
            else
            {
                Profile.Instance.Credits -= price;
            }

            Refresh();
            CargoView.Refresh();
        }

        protected void RefreshPrices()
        {
            if (Selected != null && LocationItems.ContainsKey(Selected.String))
            {
                var items = LocationItems[Selected.String];
                var price = items.Price.Long;
                var total = items.Quantity.Long * price;

                BuyPriceText.text = string.Format("{0} $[888888] / {1} $[-]", price, total);
            }
            else
            {
                BuyPriceText.text = null;
            }

            if (Selected != null && PlayerItems.ContainsKey(Selected.String))
            {
                var items = PlayerItems[Selected.String];
                var price = GetShopPrice(items.Price.Long);
                var total = items.Quantity.Long * price;

                SellPriceText.text = string.Format("{0} $[888888] / {1} $[-]", price, total);
            }
            else
            {
                SellPriceText.text = null;
            }
        }

        protected static long GetShopPrice(ProtectedValue price)
        {
            return (price.Long * Settings.SellRate).RoundToLong();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class BaseShopView : BaseScreenView
    {
        public Transform ShopGroup;
        public Transform ShipGroup;
        public GameButton SellButton;
        public GameButton BuyButton;
        public UISprite SelectedImage;
        public UILabel SelectedName;
        public UILabel BuyPriceText;
        public UILabel SellPriceText;

        protected Dictionary<string, GenericShopItem> LocationItems = new Dictionary<string, GenericShopItem>();
        protected Dictionary<string, GenericShopItem> PlayerItems = new Dictionary<string, GenericShopItem>();

        protected ProtectedValue Key;
        protected ProtectedValue Id;

        public void Start()
        {
            BuyButton.Up += Buy;
            SellButton.Up += Sell;
        }

        public static void TweenButton(Component component, Vector2 position, float alpha, float animationTime)
        {
            TweenPosition.Begin(component.gameObject, animationTime, position);
            TweenAlpha.Begin(component.gameObject, animationTime, alpha);
        }

        public void Reload()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            Key = null;
            SelectedName.text = SelectedImage.spriteName = null;
            ShopGroup.Clean();
            ShipGroup.Clean();
            WrapItems();
            Refresh();
            Open<CargoView>();
        }

        protected override void Cleanup()
        {
            Close<CargoView>();
        }

        #region Overridable

        protected abstract bool Storage { get; }
        protected abstract void WrapItems();
        protected abstract void ExtractItems(GenericShopItem item, bool sell);

        #endregion

        #region Private

        private void Buy()
        {
            Move(LocationItems, PlayerItems, Key, false);
        }

        private void Sell()
        {
            if (PlayerItems[Key.String].Disabled == null)
            {
                Move(PlayerItems, LocationItems, Key, true);
            }
            else
            {
                Debug.Log(PlayerItems[Key.String].Disabled.String);
            }
        }

        private void Refresh()
        {
            RefreshItems(LocationItems, ShopGroup, false);
            RefreshItems(PlayerItems, ShipGroup, true);
            RefreshButtons();
            RefreshPrices();
        }

        private void SelectItem(ProtectedValue key, ProtectedValue id)
        {
            Key = key;
            Id = id;
            SelectedImage.spriteName = SelectedName.text = id.String;
            RefreshButtons();
            RefreshPrices();
        }

        private void Move(IDictionary<string, GenericShopItem> source, IDictionary<string, GenericShopItem> destination, ProtectedValue key, bool sell)
        {
            var stringKey = key.String;
            var item = source[stringKey];

            if (item.Quantity == 1 && sell)
            {
                source.Remove(stringKey);
            }
            else
            {
                item.Quantity--;
            }

            if (destination.ContainsKey(stringKey))
            {
                destination[stringKey].Quantity++;
            }
            else
            {
                destination.Add(key.String, new GenericShopItem { Id = item.Id, Quantity = 1,
                    Mass = item.Mass.Copy(), Volume = item.Volume.Copy(), Price = item.Price.Copy() });
            }

            if (sell)
            {
                Profile.Instance.Credits += GetShopPrice(item.Price);
            }
            else
            {
                Profile.Instance.Credits -= item.Price;
            }

            ExtractItems(item, sell);
            Refresh();
            GetComponent<CargoView>().Refresh();
            GetComponent<StatusView>().Refresh();
        }

        private const float AnimationTime = 0.25f;
        private const float Step = 170;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private void RefreshItems(Dictionary<string, GenericShopItem> items, Transform parent, bool sell)
        {
            var unavailable = items.Where(i => i.Value.Quantity == 0).ToDictionary(i => i.Key, i => i.Value);
            var available = items.Where(i => i.Value.Quantity > 0).ToDictionary(i => i.Key, i => i.Value);
            var buttons = parent.GetComponentsInChildren<ShopItemButton>();
            var buttonsToDestroy = buttons.Where(button => available.All(item => item.Key != button.Key)
                || unavailable.Any(item => item.Key == button.Key)).ToList();

            if (buttonsToDestroy.Count > 0)
            {
                DestroyItems(buttonsToDestroy, sell);
            }

            ShowItems(buttons, available, parent, sell);
        }

        private static void DestroyItems(IEnumerable<ShopItemButton> buttons, bool shop)
        {
            foreach (var button in buttons)
            {
                var position = button.transform.localPosition + Shift * (shop ? -1 : 1);

                button.Pressed = false;
                TweenButton(button, position, 0, AnimationTime);
                Destroy(button.gameObject, AnimationTime);
            }
        }

        private void ShowItems(ShopItemButton[] buttons, Dictionary<string, GenericShopItem> items, Transform parent, bool sell)
        {
            foreach (var item in items)
            {
                var i = items.ToList().IndexOf(item);
                var button = buttons.FirstOrDefault(j => j.Key == item.Key);
                var position = new Vector3(-Step / 2 * (items.Count - 1) + Step * i, 0);

                if (button == null)
                {
                    if (this is ShopView || this is StorageView)
                    {
                        button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<ShopItemButton>();
                    }
                    else if (this is EquipmentShopView || this is EquipmentStorageView)
                    {
                        button = PrefabsHelper.InstantiateEquipmentButton(parent).GetComponent<ShopItemButton>();
                    }
                    else if (this is ShipShopView)
                    {
                        button = PrefabsHelper.InstantiateShipItemButton(parent).GetComponent<ShopItemButton>();
                    }
                    else
                    {
                        throw new Exception();
                    }

                    button.transform.localPosition = position + Shift * (sell ? -1 : 1);
                    TweenAlpha.Begin(button.gameObject, 0, 0);
                }

                var key = item.Key;
                var value = item.Value;

                if (Storage)
                {
                    button.Initialize(key, value.Id, () => SelectItem(key, value.Id), value.Quantity);
                }
                else
                {
                    button.Initialize(key, value.Id, () => SelectItem(key, value.Id), value.Quantity, sell ? GetShopPrice(value.Price) : value.Price);
                }

                TweenButton(button, position, 1, AnimationTime);
            }
        }

        private void RefreshButtons()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            if (SelectManager.Ship.Location.Name != SelectManager.Location.Name)
            {
                SellButton.Enabled = BuyButton.Enabled = false;

                return;
            }

            if (Key == null)
            {
                SellButton.Enabled = BuyButton.Enabled = false;
            }
            else
            {
                var shipItem = PlayerItems.ContainsKey(Key.String) ? PlayerItems[Key.String] : null;
                var shopItem = LocationItems.ContainsKey(Key.String) ? LocationItems[Key.String] : null;
                var item = shipItem ?? shopItem;

                SellButton.Enabled = shipItem != null && shipItem.Quantity > 0;
                BuyButton.Enabled = shopItem != null && shopItem.Quantity > 0
                    && ship.CargoMass + item.Mass <= ship.Mass
                    && ship.CargoVolume + item.Volume <= ship.Volume
                    && LocationItems.ContainsKey(Key.String)
                    && Profile.Instance.Credits >= LocationItems[Key.String].Price;
            }
        }

        protected void RefreshPrices()
        {
            if (Storage) return;

            if (Key != null && LocationItems.ContainsKey(Key.String))
            {
                var items = LocationItems[Key.String];
                var price = items.Price.Long;
                var total = items.Quantity.Long * price;

                BuyPriceText.text = string.Format("{0} $[888888] / {1} $[-]", price, total);
            }
            else
            {
                BuyPriceText.text = null;
            }

            if (Key != null && PlayerItems.ContainsKey(Key.String))
            {
                var items = PlayerItems[Key.String];
                var price = GetShopPrice(items.Price);
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

        #endregion
    }
}
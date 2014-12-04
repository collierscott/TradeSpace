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
    public abstract class BaseShop : BaseScreen
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
            Key = null;
            SelectedName.text = SelectedImage.spriteName = null;
            WrapItems();
            Refresh();
            Open<Cargo>();
        }

        protected override void Initialize()
        {
            ShopGroup.Clear();
            ShipGroup.Clear();
            Reload();
        }

        protected override void Cleanup()
        {
            Close<Cargo>();
        }

        #region Overridable

        protected abstract bool StorageMode { get; }
        protected abstract void WrapItems();
        protected abstract void ExtractItems(GenericShopItem item, bool sell);
        protected abstract long GetPrice(GenericShopItem item, bool sell);

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
            foreach (var item in LocationItems.Values)
            {
                item.Price = GetPrice(item, sell: false);
            }

            foreach (var item in PlayerItems.Values)
            {
                item.Price = GetPrice(item, sell: true);
            }

            RefreshItems(LocationItems, ShopGroup, false);
            RefreshItems(PlayerItems, ShipGroup, true);
            RefreshWorkflow();
        }

        private void RefreshWorkflow()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            if (Key == null || SelectManager.Ship.Location.Name != SelectManager.Location.Name)
            {
                SellButton.Enabled = BuyButton.Enabled = false;
                return;
            }

            var shipItem = PlayerItems.ContainsKey(Key.String) ? PlayerItems[Key.String] : null;
            var shopItem = LocationItems.ContainsKey(Key.String) ? LocationItems[Key.String] : null;
            var item = shipItem ?? shopItem;

            SellButton.Enabled = shipItem != null && shipItem.Quantity > 0;
            BuyButton.Enabled = shopItem != null && shopItem.Quantity > 0
                                && ship.CargoMass + item.Mass <= ship.Mass
                                && ship.CargoVolume + item.Volume <= ship.Volume
                                && LocationItems.ContainsKey(Key.String)
                                && Profile.Instance.Credits >= LocationItems[Key.String].Price;

            if (StorageMode) return;

            if (Key != null && LocationItems.ContainsKey(Key.String))
            {
                var price = LocationItems[Key.String].Price.Long;
                var total = LocationItems[Key.String].Quantity.Long*price;

                BuyPriceText.text = string.Format("{0} $[888888] / {1} $[-]", price, total);
            }
            else
            {
                BuyPriceText.text = null;
            }

            if (Key != null && PlayerItems.ContainsKey(Key.String))
            {
                var price = PlayerItems[Key.String].Price.Long;
                var total = PlayerItems[Key.String].Quantity.Long*price;

                SellPriceText.text = string.Format("{0} $[888888] / {1} $[-]", price, total);
            }
            else
            {
                SellPriceText.text = null;
            }
        }

        private void SelectItem(ProtectedValue key, GenericShopItem item)
        {
            Key = key;
            Id = item.Id;

            if (item.Type == typeof(MemoGoods)) // TODO: Move
            {
                SelectedImage.atlas = Resources.Load<UIAtlas>("Atlases/Goods");
            }
            else if (item.Type == typeof(MemoEquipment))
            {
                SelectedImage.atlas = Resources.Load<UIAtlas>("Atlases/Equipment");
            }
            else if (item.Type == typeof(MemoShipItem))
            {
                SelectedImage.atlas = Resources.Load<UIAtlas>("Atlases/Ships");
            }
            else
            {
                throw new Exception();
            }

            SelectedImage.spriteName = SelectedName.text = item.Id.String;
            RefreshWorkflow();
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
                    Mass = item.Mass.Copy(), Volume = item.Volume.Copy(), Type = item.Type });
            }

            if (sell)
            {
                Profile.Instance.Credits += item.Price;
            }
            else
            {
                Profile.Instance.Credits -= item.Price;
            }

            ExtractItems(item, sell);
            Refresh();
            GetComponent<Cargo>().Refresh();
            GetComponent<Status>().Refresh();
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
            for (var i = 0; i < items.Count; i++)
            {
                var key = items.Keys.ElementAt(i);
                var item = items.Values.ElementAt(i);
                var button = buttons.FirstOrDefault(j => j.Key == key);
                var position = new Vector3(-Step / 2 * (items.Count - 1) + Step * i, 0);

                if (button == null)
                {
                    if (item.Type == typeof(MemoGoods)) // TODO: Move
                    {
                        button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<ShopItemButton>();
                    }
                    else if (item.Type == typeof(MemoEquipment))
                    {
                        button = PrefabsHelper.InstantiateEquipmentButton(parent).GetComponent<ShopItemButton>();
                    }
                    else if (item.Type == typeof(MemoShipItem))
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

                if (StorageMode)
                {
                    button.Initialize(key, item.Id, () => SelectItem(key, item), item.Quantity);
                }
                else
                {
                    button.Initialize(key, item.Id, () => SelectItem(key, item), item.Quantity, item.Price);
                }

                TweenButton(button, position, 1, AnimationTime);
            }
        }

        #endregion
    }
}
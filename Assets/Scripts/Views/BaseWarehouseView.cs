using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class BaseWarehouseView : ViewBase, IScreenView
    {
        public Transform ShopGroup;
        public Transform ShipGroup;
        public GameButton PutButton;
        public GameButton GetButton;
        public UISprite SelectedImage;
        public UILabel SelectedName;

        protected Dictionary<string, ShopItem> LocationItems = new Dictionary<string, ShopItem>();
        protected Dictionary<string, ShopItem> PlayerItems = new Dictionary<string, ShopItem>();
        protected ProtectedValue Selected;

        public void Start()
        {
            GetButton.Up += Get;
            PutButton.Up += Put;
        }

        public static void TweenButton(Component component, Vector2 position, float alpha, float animationTime)
        {
            TweenPosition.Begin(component.gameObject, animationTime, position);
            TweenAlpha.Begin(component.gameObject, animationTime, alpha);
        }

        protected abstract void WrapItems();
        protected abstract void ExtractItems();

        protected override void Initialize()
        {
            Selected = null;
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

        protected void Get()
        {
            Move(LocationItems, PlayerItems, Selected, false);
        }

        protected void Put()
        {
            if (PlayerItems[Selected.String].Disabled == null)
            {
                Move(PlayerItems, LocationItems, Selected, true);
            }
            else
            {
                Debug.Log(PlayerItems[Selected.String].Disabled.String);
            }
        }

        #region Virtual

        protected virtual void Refresh()
        {
            RefreshItems(LocationItems, ShopGroup, false);
            RefreshItems(PlayerItems, ShipGroup, true);
            RefreshButtons();
        }

        protected virtual void SelectItem(ProtectedValue id)
        {
            Selected = id.Copy();
            SelectedImage.spriteName = SelectedName.text = id.String;
            RefreshButtons();
        }

        protected virtual void InitializeItemButton(ShopItemButton button, ProtectedValue key, ShopItem item)
        {
            button.Initialize(key, () => SelectItem(key), item.Quantity);
        }

        protected virtual void RefreshButtons()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            if (SelectManager.Ship.Location.Name != SelectManager.Location.Name)
            {
                PutButton.Enabled = GetButton.Enabled = false;

                return;
            }

            if (Selected == null)
            {
                PutButton.Enabled = GetButton.Enabled = false;
            }
            else
            {
                var shipItem = PlayerItems.ContainsKey(Selected.String) ? PlayerItems[Selected.String] : null;
                var shopItem = LocationItems.ContainsKey(Selected.String) ? LocationItems[Selected.String] : null;
                var item = shipItem ?? shopItem;

                PutButton.Enabled = shipItem != null && shipItem.Quantity > 0;
                GetButton.Enabled = shopItem != null && shopItem.Quantity > 0
                    && ship.CargoMass + item.Mass <= ship.Mass
                    && ship.CargoVolume + item.Volume <= ship.Volume;
            }
        }

        protected virtual void Move(Dictionary<string, ShopItem> source, Dictionary<string, ShopItem> destination, ProtectedValue id, bool sell)
        {
            var item = source[id.String];

            if (item.Quantity.Long == 1 && sell)
            {
                source.Remove(id.String);
            }
            else
            {
                item.Quantity--;
            }

            if (destination.ContainsKey(id.String))
            {
                destination[id.String].Quantity++;
            }
            else
            {
                destination.Add(id.String, new ShopItem
                {
                    Id = id.Copy(),
                    Quantity = 1,
                    Mass = item.Mass.Copy(),
                    Volume = item.Volume.Copy(),
                    Price = item.Price.Copy()
                });
            }

            ExtractItems();
            Refresh();
            GetComponent<CargoView>().Refresh();
        }

        #endregion

        #region Private

        private const float AnimationTime = 0.25f;
        private const float Step = 170;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private void RefreshItems(Dictionary<string, ShopItem> items, Transform parent, bool sell)
        {
            var unavailable = items.Where(i => i.Value.Quantity == 0).ToDictionary(i => i.Key, i => i.Value);
            var available = items.Where(i => i.Value.Quantity > 0).ToDictionary(i => i.Key, i => i.Value);
            var buttons = parent.GetComponentsInChildren<ShopItemButton>();
            var buttonsToDestroy = buttons.Where(button => available.All(item => item.Key != button.Id)
                || unavailable.Any(item => item.Key == button.Id)).ToList();

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

        private void ShowItems(ShopItemButton[] buttons, Dictionary<string, ShopItem> items, Transform parent, bool sell)
        {
            foreach (var item in items)
            {
                var i = items.ToList().IndexOf(item);
                var button = buttons.FirstOrDefault(j => j.Id == item.Key);
                var position = new Vector3(-Step / 2 * (items.Count - 1) + Step * i, 0);

                if (button == null)
                {
                    if (this is ShopView || this is WarehouseView)
                    {
                        button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<ShopItemButton>();
                    }
                    else if (this is EquipmentShopView || this is EquipmentWarehouseView)
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

                InitializeItemButton(button, item.Key, item.Value);
                TweenButton(button, position, 1, AnimationTime);
            }
        }

        #endregion
    }
}
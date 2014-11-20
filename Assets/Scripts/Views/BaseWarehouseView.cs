using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
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
        public CargoView CargoView;

        protected Dictionary<string, ShopItem> ShopItems = new Dictionary<string, ShopItem>();
        protected Dictionary<string, ShopItem> ShipItems = new Dictionary<string, ShopItem>();
        protected ProtectedValue Selected;

        protected abstract void SyncItems();

        protected override void Initialize()
        {
            Selected = null;
            SelectedName.text = SelectedImage.spriteName = null;
            ShopGroup.Clean();
            ShipGroup.Clean();
            SyncItems();
            Refresh();
            CargoView.Open();
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }

        protected virtual void Refresh()
        {
            RefreshItems(ShopItems.Values.ToList(), ShopGroup, false);
            RefreshItems(ShipItems.Values.ToList(), ShipGroup, true);
            RefreshButtons();
        }

        public virtual void SelectGoods(ProtectedValue id)
        {
            Selected = id.Copy();
            SelectedImage.spriteName = id.String;
            SelectedName.SetText(id.String);
            RefreshButtons();
        }

        protected void Get()
        {
            Move(ShopItems, ShipItems, Selected, false);
        }

        protected void Put()
        {
            Move(ShipItems, ShopItems, Selected, true);
        }

        #region Helpers

        private const float AnimationTime = 0.25f;
        private const float Step = 170;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private void RefreshItems(List<ShopItem> items, Transform parent, bool shop)
        {
            var unavailable = items.Where(i => i.Quantity == 0).ToList();
            var available = items.Where(i => i.Quantity > 0).ToList();
            var buttons = parent.GetComponentsInChildren<ShopItemButton>();
            var buttonsToDestroy = buttons.Where(i => available.All(j => j.Id != i.Id)
                || unavailable.Any(j => j.Id == i.Id)).ToList();

            if (buttonsToDestroy.Count > 0)
            {
                DestroyItems(buttonsToDestroy, shop);
            }

            ShowItems(buttons, available, parent, shop);
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

        private void ShowItems(ShopItemButton[] buttons, List<ShopItem> goods, Transform parent, bool shop)
        {
            for (var i = 0; i < goods.Count; i++)
            {
                var id = goods[i].Id;
                var button = buttons.FirstOrDefault(j => j.Id == id);
                var position = new Vector3(-Step / 2 * (goods.Count - 1) + Step * i, 0);

                if (button == null)
                {
                    button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<ShopItemButton>();
                    button.transform.localPosition = position + Shift * (shop ? -1 : 1);
                    TweenAlpha.Begin(button.gameObject, 0, 0);
                }

                button.Initialize(id, () => SelectGoods(id), goods[i].Quantity);
                TweenButton(button, position, 1, AnimationTime);
            }
        }

        public static void TweenButton(Component component, Vector2 position, float alpha, float animationTime)
        {
            TweenPosition.Begin(component.gameObject, animationTime, position);
            TweenAlpha.Begin(component.gameObject, animationTime, alpha);
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
                var shipItem = ShipItems.ContainsKey(Selected.String) ? ShipItems[Selected.String] : null;
                var shopItem = ShopItems.ContainsKey(Selected.String) ? ShopItems[Selected.String] : null;
                var item = shipItem ?? shopItem;

                PutButton.Enabled = shipItem != null && shipItem.Quantity > 0;
                GetButton.Enabled = shopItem != null && shopItem.Quantity > 0
                    && ship.CargoMass + item.Mass <= ship.Mass
                    && ship.CargoVolume + item.Volume <= ship.Volume;
            }
        }

        protected abstract void SyncItemsBack();

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

            SyncItemsBack();
            Refresh();
            CargoView.Refresh();
        }

        protected static ShopItem GetShopItem(MemoGoods item)
        {
            return new ShopItem
            {
                Id = new ProtectedValue(item.Id),
                Quantity = item.Quantity.Copy(),
                Mass = Env.GoodsDatabase[item.Id].Mass,
                Volume = Env.GoodsDatabase[item.Id].Volume,
                Price = item.Price.Copy()
            };
        }

        protected static MemoGoods GetMemoGoods(ShopItem item)
        {
            return new MemoGoods
            {
                Id = item.Id.String.ToEnum<GoodsId>(),
                Quantity = item.Quantity.Copy(),
                Price = item.Price.Copy()
            };
        }

        protected static ShopItem GetShopItem(MemoEquipment item)
        {
            return new ShopItem
            {
                Id = new ProtectedValue(item.Id),
                Quantity = item.Quantity.Copy(),
                Mass = Env.EquipmentDatabase[item.Id].Mass,
                Volume = Env.EquipmentDatabase[item.Id].Volume,
                Price = item.Price.Copy()
            };
        }

        protected static MemoEquipment GetMemoEquipment(ShopItem item)
        {
            return new MemoEquipment
            {
                Id = item.Id.String.ToEnum<EquipmentId>(),
                Quantity = item.Quantity.Copy(),
                Price = item.Price.Copy()
            };
        }

        #endregion
    }
}
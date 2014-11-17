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
    public class WarehouseView : ViewBase, IScreenView
    {
        public Transform WarehouseTransform;
        public Transform ShipTransform;
        public GameButton PutButton;
        public GameButton GetButton;
        public UISprite SelectedImage;
        public UILabel SelectedName;
        public CargoView CargoView;

        private List<MemoGoods> _warehouseGoods;
        private List<MemoGoods> _shipGoods;
        private GoodsId _selected;

        protected override void Initialize()
        {
            var planet = (Planet) SelectManager.Location;
            var environment = GetComponent<EnvironmentManager>();

            if (!Profile.Instance.Shops.ContainsKey(planet.Name))
            {
                environment.InitShop(planet);
            }

            WarehouseTransform.Clean();
            ShipTransform.Clean();

            SelectedName.text = null;
            SelectedImage.spriteName = null;

            if (!Profile.Instance.Warehouses.ContainsKey(planet.Name))
            {
                Profile.Instance.Warehouses[planet.Name] = new MemoWarehouse { Goods = new List<MemoGoods>() };
            }

            _warehouseGoods = Profile.Instance.Warehouses[planet.Name].Goods;
            _shipGoods = Profile.Instance.Ship.Goods;

            Refresh();
            CargoView.Open();
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }

        public void Refresh()
        {
            RefreshGoods(_warehouseGoods, WarehouseTransform, false);
            RefreshGoods(_shipGoods, ShipTransform, true);
            RefreshButtons();
        }

        public void SelectGoods(GoodsId goodsId)
        {
            _selected = goodsId;
            SelectedImage.spriteName = goodsId.ToString();
            SelectedName.SetText(goodsId.ToString());
            RefreshButtons();
        }

        public void Get()
        {
            Transfer(_warehouseGoods, _shipGoods, _selected, false);
        }

        public void Put()
        {
            Transfer(_shipGoods, _warehouseGoods, _selected, true);
        }

        #region Helpers

        private const float AnimationTime = 0.25f;
        private const float Step = 170;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private void RefreshGoods(List<MemoGoods> goods, Transform parent, bool shop)
        {
            var unavailableGoods = goods.Where(i => i.Quantity.Long == 0).ToList();
            var availableGoods = goods.Except(unavailableGoods).ToList();
            var buttons = parent.GetComponentsInChildren<GoodsButton>();
            var buttonsToDestroy = buttons.Where(i => availableGoods.All(j => j.Id != i.GoodsId)
                || unavailableGoods.Any(j => j.Id == i.GoodsId)).ToList();

            if (buttonsToDestroy.Count > 0)
            {
                DestroyGoods(buttonsToDestroy, shop);
            }

            ShowGoods(buttons, availableGoods, parent, shop);
        }

        private static void DestroyGoods(IEnumerable<GoodsButton> buttons, bool shop)
        {
            foreach (var button in buttons)
            {
                var position = button.transform.localPosition + Shift * (shop ? -1 : 1);

                button.Pressed = false;
                TweenButton(button, position, 0, AnimationTime);
                Destroy(button.gameObject, AnimationTime);
            }
        }

        private void ShowGoods(GoodsButton[] buttons, IList<MemoGoods> goods, Transform parent, bool shop)
        {
            for (var i = 0; i < goods.Count; i++)
            {
                var goodsId = goods[i].Id;
                var button = buttons.FirstOrDefault(j => j.GoodsId == goodsId);
                var position = new Vector3(-Step / 2 * (goods.Count - 1) + Step * i, 0);

                if (button == null)
                {
                    button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<GoodsButton>();
                    button.transform.localPosition = position + Shift * (shop ? -1 : 1);
                    TweenAlpha.Begin(button.gameObject, 0, 0);
                }

                button.Initialize(goodsId, goods[i].Quantity.Long, () => SelectGoods(goodsId));
                TweenButton(button, position, 1, AnimationTime);
            }
        }

        public static void TweenButton(Component component, Vector2 position, float alpha, float animationTime)
        {
            TweenPosition.Begin(component.gameObject, animationTime, position);
            TweenAlpha.Begin(component.gameObject, animationTime, alpha);
        }

        private void RefreshButtons()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            if (SelectManager.Ship.Location.Name != SelectManager.Location.Name)
            {
                PutButton.Enabled = GetButton.Enabled = false;

                return;
            }

            PutButton.Enabled = _selected != GoodsId.Empty && _shipGoods.Any(i => i.Id == _selected && i.Quantity.Long > 0);
            GetButton.Enabled = _selected != GoodsId.Empty && _warehouseGoods.Any(i => i.Id == _selected && i.Quantity.Long > 0)
                                && ship.GoodsMass + Env.GoodsDatabase[_selected].Mass <= ship.Mass
                                && ship.GoodsVolume + Env.GoodsDatabase[_selected].Volume <= ship.Volume;
        }

        private void Transfer(List<MemoGoods> source, List<MemoGoods> destination, GoodsId goodsId, bool sell)
        {
            var goods = source.Single(goodsId);

            if (goods.Quantity.Long == 1 && sell)
            {
                source.Remove(goods);
            }
            else
            {
                goods.Quantity--;
            }

            if (destination.Contains(goodsId))
            {
                destination.Single(goodsId).Quantity++;
            }
            else
            {
                destination.Add(new MemoGoods { Id = goodsId, Quantity = 1, Price = goods.Price });
            }

            Refresh();
            CargoView.Refresh();
        }

        #endregion
    }
}
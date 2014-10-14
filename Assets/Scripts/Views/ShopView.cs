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
    public class ShopView : ViewBase, IScreenView
    {
        public Transform ShopTransform;
        public Transform ShipTransform;
        public GameButton SellButton;
        public GameButton BuyButton;
        public UISprite SelectedImage;
        public UILabel SelectedName;
        public CargoView CargoView;

        private List<MemoGoods> _shopGoods;
        private List<MemoGoods> _shipGoods;
        private GoodsId _selected;

        protected override void Initialize()
        {
            var planet = (Planet) SelectManager.Location;
            var environment = GetComponent<EnvironmentManager>();

            if (!environment.ShopExists(planet.Name))
            {
                environment.InitShop(planet);
            }

            ShopTransform.Clean();
            ShipTransform.Clean();

            _shopGoods = Profile.Instance.Shops.Single(i => i.Id == planet.Name).Goods;
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
            var planet = (Planet) SelectManager.Location;

            _shipGoods.ForEach(i => i.Price.Long = _shopGoods.Contains(i.Id) ? _shopGoods.Single(i.Id).Price.Long : (Env.GoodsDatabase[i.Id].Price * planet.DefaultPriceRate).RoundToLong());
            RefreshGoodsButtons(_shopGoods, ShopTransform, false);
            RefreshGoodsButtons(_shipGoods, ShipTransform, true);
            RefreshButtons();
        }

        public void SelectGoods(GoodsId goods)
        {
            _selected = goods;
            SelectedImage.spriteName = goods.ToString();
            SelectedName.SetText(goods.ToString());
            RefreshButtons();
        }

        public void Buy()
        {
            Trade(_shopGoods, _shipGoods, _selected, false);
        }

        public void Sell()
        {
            Trade(_shipGoods, _shopGoods, _selected, true);
        }

        #region Helpers

        private const float AnimationTime = 0.25f;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private static void RefreshGoodsButtons(List<MemoGoods> goods, Transform parent, bool sell)
        {
            var unavailable = goods.Where(i => i.Quantity.Long == 0).ToList();
            var available = goods.Where(i => i.Quantity.Long > 0).ToList();
            var buttons = parent.GetComponentsInChildren<GoodsButton>();
        
            foreach (var button in buttons.Where(i => available.All(j => j.Id != i.GoodsId) || unavailable.Any(j => j.Id == i.GoodsId)))
            {
                var position = button.transform.localPosition + Shift * (sell ? -1 : 1);

                button.Pressed = false;
                TweenButton(button, position, 0, AnimationTime);
                Destroy(button.gameObject, AnimationTime);
            }

            for (var i = 0; i < available.Count; i++)
            {
                var button = buttons.FirstOrDefault(j => j.GoodsId == available[i].Id);
                var price = sell ? GetSellPrice(available[i].Price.Long) : available[i].Price.Long;
                var position = new Vector3(-75 * (available.Count - 1) + 150 * i, 0);

                if (button == null)
                {
                    button = PrefabsHelper.InstantiateGoodsButton(parent).GetComponent<GoodsButton>();
                    button.transform.localPosition = position + Shift * (sell ? -1 : 1);
                    TweenAlpha.Begin(button.gameObject, 0, 0);
                }

                button.Initialize(available[i].Id, available[i].Quantity.Long, price);
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
                SellButton.Enabled = BuyButton.Enabled = false;

                return;
            }

            SellButton.Enabled = _selected != GoodsId.Empty && _shipGoods.Any(i => i.Id == _selected && i.Quantity.Long > 0);
            BuyButton.Enabled = _selected != GoodsId.Empty && _shopGoods.Any(i => i.Id == _selected && i.Quantity.Long > 0)
                                && ship.GoodsMass + Env.GoodsDatabase[_selected].Mass <= ship.Mass
                                && ship.GoodsVolume + Env.GoodsDatabase[_selected].Volume <= ship.Volume
                                && Profile.Instance.Credits.Long >= _shopGoods.Single(i => i.Id == _selected).Price.Long;
        }

        private void Trade(List<MemoGoods> source, List<MemoGoods> destination, GoodsId goodsId, bool sell)
        {
            var goods = source.Single(goodsId);

            if (goods.Quantity.Long == 1 && sell)
            {
                source.Remove(goods);
            }
            else
            {
                goods.Quantity.Long--;
            }

            if (destination.Contains(goodsId))
            {
                destination.Single(goodsId).Quantity.Long++;
            }
            else
            {
                destination.Add(new MemoGoods { Id = goodsId, Quantity = 1.Encrypt(), Price = goods.Price });
            }

            Profile.Instance.Credits.Long += sell ? GetSellPrice(goods.Price.Long) : -goods.Price.Long;
            Refresh();
            CargoView.Refresh();
        }

        private static long GetSellPrice(long price)
        {
            return (price * Settings.SellRate).RoundToLong();
        }

        #endregion
    }
}
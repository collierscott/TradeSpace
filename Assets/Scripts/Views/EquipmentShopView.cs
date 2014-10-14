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
    public class EquipmentShopView : ViewBase, IScreenView
    {
        public Transform ShopTransform;
        public Transform ShipTransform;
        public GameButton SellButton;
        public GameButton BuyButton;
        public UISprite SelectedImage;
        public UILabel SelectedName;
        public CargoView CargoView;

        private List<MemoEquipment> _shopEquipment;
        private List<MemoEquipment> _shipEquipment;
        private EquipmentId _selected;

        protected override void Initialize()
        {
            Debug.Log(SelectManager.Location);

            var station = (Station) SelectManager.Location;
            var environment = GetComponent<EnvironmentManager>();

            if (!environment.ShopExists(station.Name))
            {
                environment.InitShop(station);
            }

            _shopEquipment = Profile.Instance.Shops.Single(i => i.Id == station.Name).Equipment;
            _shipEquipment = Profile.Instance.Ship.Equipment;

            ShopTransform.Clean();
            ShipTransform.Clean();
            Refresh();
            CargoView.Open();
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }

        public void Refresh()
        {
            var station = (Station) SelectManager.Location;

            _shipEquipment.ForEach(i => i.Price.Long = _shopEquipment.Contains(i.Id) ? _shopEquipment.Single(i.Id).Price.Long : (Env.EquipmentDatabase[i.Id].Price * station.PriceRate).RoundToLong());
            RefreshEquipmentButtons(_shopEquipment, ShopTransform, false);
            RefreshEquipmentButtons(_shipEquipment, ShipTransform, true);
            RefreshButtons();
        }

        public void SelectEquipment(EquipmentId equipment)
        {
            _selected = equipment;
            SelectedImage.spriteName = equipment.ToString();
            SelectedName.SetText(equipment.ToString());
            RefreshButtons();
        }

        public void BuyEquipment()
        {
            Trade(_shopEquipment, _shipEquipment, _selected, false);
        }

        public void SellEquipment()
        {
            Trade(_shipEquipment, _shopEquipment, _selected, true);
        }

        #region Helpers

        private const float AnimationTime = 0.25f;
        private static readonly Vector3 Shift = new Vector3(0, 150);

        private static void RefreshEquipmentButtons(List<MemoEquipment> equipment, Transform parent, bool sell)
        {
            var unavailable = equipment.Where(i => i.Quantity.Long == 0).ToList();
            var available = equipment.Where(i => i.Quantity.Long > 0).ToList();
            var buttons = parent.GetComponentsInChildren<GoodsButton>();

            foreach (var button in buttons.Where(i => available.All(j => j.Id != i.EquipmentId) || unavailable.Any(j => j.Id == i.EquipmentId)))
            {
                var position = button.transform.localPosition + Shift * (sell ? -1 : 1);

                button.Pressed = false;
                TweenButton(button, position, 0, AnimationTime);
                Destroy(button.gameObject, AnimationTime);
            }

            for (var i = 0; i < available.Count; i++)
            {
                var button = buttons.FirstOrDefault(j => j.EquipmentId == available[i].Id);
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

            SellButton.Enabled = _selected != EquipmentId.Empty && _shipEquipment.Any(i => i.Id == _selected && i.Quantity.Long > 0);
            BuyButton.Enabled = _selected != EquipmentId.Empty && _shopEquipment.Any(i => i.Id == _selected && i.Quantity.Long > 0)
                                && ship.GoodsMass + Env.EquipmentDatabase[_selected].Mass <= ship.Mass
                                && ship.GoodsVolume + Env.EquipmentDatabase[_selected].Volume <= ship.Volume
                                && Profile.Instance.Credits.Long >= _shopEquipment.Single(i => i.Id == _selected).Price.Long;
        }

        private void Trade(List<MemoEquipment> source, List<MemoEquipment> destination, EquipmentId equipment, bool sell)
        {
            var goods = source.Single(equipment);

            if (goods.Quantity.Long == 1 && sell)
            {
                source.Remove(goods);
            }
            else
            {
                goods.Quantity.Long--;
            }

            if (destination.Contains(equipment))
            {
                destination.Single(equipment).Quantity.Long++;
            }
            else
            {
                destination.Add(new MemoEquipment { Id = equipment, Quantity = 1.Encrypt(), Price = goods.Price });
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
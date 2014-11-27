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
    public class ShipShopView : BaseShopView
    {
        private static ShipId _shipId;

        #region Override

        protected override void WrapItems()
        {
            var station = (Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            LocationItems = new Dictionary<string, ShopItem>();
            PlayerItems = new Dictionary<string, ShopItem>();

            foreach (var shopItem in Profile.Instance.Shops[station.Name].Ships.Select(ship => ShopConversation.GetShopItem(ship)))
            {
                LocationItems.Add(CRandom.RandomString, shopItem);
            }

            foreach (var ship in CurrentShips)
            {
                var memoShipItem = GetShipItem(ship);
                var shopItem = ShopConversation.GetShopItem(memoShipItem);

                if (!ReadyForSale(ship))
                {
                    shopItem.Disabled = "Ship is not ready for sale: please remove goods and equipment";
                }

                PlayerItems.Add(ship.UniqName.String, shopItem);
            }

            foreach (var uniqName in PlayerItems.Keys)
            {
                var id = PlayerItems[uniqName].Id.String.ToEnum<ShipId>();
                
                PlayerItems[uniqName].Price = LocationItems.ContainsKey(uniqName)
                    ? LocationItems[uniqName].Price.Long
                    : (Env.ShipDatabase[id].Price * station.PriceRate).RoundToLong();
            }
        }

        protected override void ExtractItems()
        {
        }

        protected override void InitializeItemButton(ShopItemButton button, ProtectedValue key, ShopItem item)
        {
            button.Initialize(key, () => SelectItem(key, item), null);
            button.Image.spriteName = item.Id.String;
        }

        protected override void Move(Dictionary<string, ShopItem> source, Dictionary<string, ShopItem> destination, ProtectedValue uniqName, bool sell)
        {
            var item = source[uniqName.String];

            source.Remove(uniqName.String);
            destination.Add(uniqName.String, new ShopItem
            {
                Id = item.Id.Copy(),
                Quantity = 1,
                Price = item.Price.Copy()
            });

            if (sell)
            {
                Profile.Instance.Ships.RemoveAll(i => i.UniqName == Selected);
                Profile.Instance.Credits += GetShopPrice(item.Price);
            }
            else
            {
                var ship = new MemoShip
                {
                    Id = _shipId,
                    Route = new List<RouteNode> { SelectManager.Location.ToRouteNode() }
                };

                Profile.Instance.Ships.Add(ship);
                Profile.Instance.Credits -= item.Price;
            }

            Debug.Log(Profile.Instance.Credits.Long);

            Refresh();
        }

        protected override void Refresh()
        {
            RefreshPrices();
            base.Refresh();
        }

        protected override void RefreshButtons()
        {
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
                var uniqName = Selected.String;
                var shipItem = PlayerItems.ContainsKey(uniqName) ? PlayerItems[uniqName] : null;
                var shopItem = LocationItems.ContainsKey(uniqName) ? LocationItems[uniqName] : null;

                PutButton.Enabled = shipItem != null && shipItem.Quantity > 0;
                GetButton.Enabled = shopItem != null && shopItem.Quantity > 0
                    && Profile.Instance.Credits >= LocationItems[uniqName].Price;
            }
        }

        #endregion

        #region Private

        private static MemoShipItem GetShipItem(MemoShip ship)
        {
            return new MemoShipItem
            {
                Id = ship.Id,
                Quantity = 1
            };
        }

        private static IEnumerable<MemoShip> CurrentShips
        {
            get
            {
                return Profile.Instance.Ships.Where(i =>
                    i.State == ShipState.Ready && i.Route.Last().System == SelectManager.Location.System &&
                    i.Route.Last().LocationName == SelectManager.Location.Name).ToList();
            }
        }

        private static bool ReadyForSale(MemoShip ship)
        {
            return ship.Goods.Count + ship.Equipment.Count + ship.InstalledEquipment.Count == 0;
        }

        private void SelectItem(ProtectedValue key, ShopItem item)
        {
            base.SelectItem(key);

            var shipId = item.Id.String;

            _shipId = shipId.ToEnum<ShipId>();
            SelectedImage.spriteName = SelectedName.text = shipId;
        }
        
        #endregion
    }
}
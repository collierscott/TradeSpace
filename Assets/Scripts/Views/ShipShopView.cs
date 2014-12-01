using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class ShipShopView : BaseShopView
    {
        #region Override

        protected override void Initialize()
        {
            base.Initialize();
            Close<CargoView>();
            GetComponent<StatusView>().Refresh();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            GetComponent<StatusView>().Refresh();

            if (!Profile.Instance.Ships.ContainsKey(Profile.Instance.SelectedShip.String))
            {
                Profile.Instance.SelectedShip = Profile.Instance.Ships.First().Key;
            }
        }

        protected override bool Storage
        {
            get { return false; }
        }

        protected override void WrapItems()
        {
            var station = (Station) SelectManager.Location;

            if (!Profile.Instance.Shops.ContainsKey(station.Name))
            {
                GetComponent<EnvironmentManager>().InitShop(station);
            }

            LocationItems = new Dictionary<string, GenericShopItem>();
            PlayerItems = new Dictionary<string, GenericShopItem>();

            foreach (var shopItem in Profile.Instance.Shops[station.Name].Ships.Select(i => new GenericShopItem(i)))
            {
                LocationItems.Add(Env.GetUniqueShipName(), shopItem);
            }

            foreach (var ship in CurrentShips)
            {
                var memoShipItem = GetShipItem(ship);
                var shopItem = new GenericShopItem(memoShipItem);

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

        protected override void ExtractItems(GenericShopItem item, bool sell)
        {
            if (sell)
            {
                Profile.Instance.Ships.Remove(Key.String);
                Profile.Instance.Credits += GetShopPrice(item.Price);
            }
            else
            {
                var ship = new MemoShip
                {
                    Id = Id.String.ToEnum<ShipId>(),
                    Route = new List<RouteNode> { SelectManager.Location.ToRouteNode() }
                };

                Profile.Instance.Ships.Add(ship.UniqName.String, ship);
                Profile.Instance.Credits -= item.Price;
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
                return Profile.Instance.Ships.Values.Where(i =>
                    i.State == ShipState.Ready && i.Route.Last().System == SelectManager.Location.System &&
                    i.Route.Last().LocationName == SelectManager.Location.Name).ToList();
            }
        }

        private static bool ReadyForSale(MemoShip ship)
        {
            return ship.Goods.Count + ship.Equipment.Count + ship.InstalledEquipment.Count == 0;
        }
        
        #endregion
    }
}
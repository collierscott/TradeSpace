using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static long GetPrice(ShipId id, Location location, bool sell)
        {
            var station = (Station) Systems[location.System][location.Name];
            var ship = ShipDatabase[id];
            var price = ship.Price;

            if (sell)
            {
                price = ((1 - station.Tax) * price).RoundToLong();
                price = (price * station.ImportRate).RoundToLong();
            }
            else
            {
                price = (price * station.ExportRate).RoundToLong();
            }

            price += (price * Profile.Instance.Shops[station.Name].PriceDelta).RoundToLong();

            return price;
        }

        public static string GetUniqueShipName()
        {
            return Convert.ToString(CRandom.GetRandom(1000000));
        }
    }
}
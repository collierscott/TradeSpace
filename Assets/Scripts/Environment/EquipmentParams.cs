using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static long GetPrice(EquipmentId id, Location location, bool sell)
        {
            var station = (Station) Systems[location.System][location.Name];
            var equipment = EquipmentDatabase[id];
            var price = equipment.Price;

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
    }
}
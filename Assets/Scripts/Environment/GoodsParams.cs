using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static long GetPrice(GoodsId id, Location location, bool sell)
        {
            var planet = (Planet) Systems[location.System][location.Name];
            var goods = GoodsDatabase[id];
            var price = goods.Price;

            if (sell)
            {
                price = ((1 - planet.Tax) * price).RoundToLong();

                if (planet.Import.Contains(goods.Type))
                {
                    price = (price * planet.ImportRate).RoundToLong();
                }
            }
            else
            {
                price = (price * planet.ExportRate).RoundToLong();
            }

            price += (price * Profile.Instance.Shops[planet.Name].PriceDelta).RoundToLong();

            return price;
        }
    }
}
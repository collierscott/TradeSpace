using System;
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

        public static long GetMineralStructure(GoodsId mineral)
        {
            switch (mineral)
            {
                case GoodsId.Ferrum:
                    return 100;
                case GoodsId.Titanium:
                    return 200;
                default:
                    throw new NotImplementedException();
            }
        }

        public static GoodsId GetMineralCore(GoodsId mineral)
        {
            switch (mineral)
            {
                case GoodsId.Ferrum:
                    return GoodsId.FerrumCore;
                case GoodsId.Titanium:
                    return GoodsId.TitaniumCore;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
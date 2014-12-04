using System;
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

        public static DrillParams GetDrillParams(EquipmentId equipmentId)
        {
            switch (equipmentId)
            {
                case EquipmentId.ImpulseDrill100:
                    return new DrillParams
                    {
                        Power = 10,
                        Class = AsteroidClass.A,
                        Type = DrillType.Impulse,
                        ReloadSecs = 1
                    };
                case EquipmentId.LaserDrill100:
                    return new DrillParams
                    {
                        Power = 10,
                        Class = AsteroidClass.A,
                        Type = DrillType.Laser,
                        HeatingRate = 10,
                        CoolingRate = 25,
                        HeatingTo = 30,
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
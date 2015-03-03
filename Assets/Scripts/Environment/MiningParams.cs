using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static class MiningParams
    {
        public static Dictionary<LodeClass, long> Hardness
        {
            get
            {
                return new Dictionary<LodeClass, long>
                {
                    { LodeClass.A, 1 },
                    { LodeClass.B, 2 },
                    { LodeClass.C, 4 },
                    { LodeClass.D, 8 },
                    { LodeClass.E, 16 }
                };
            }
        }

        public static Dictionary<GoodsId, long> Structure
        {
            get
            {
                return new Dictionary<GoodsId, long>
                {
                    { GoodsId.Ferrum, 100 },
                    { GoodsId.Titanium, 200 },
                };
            }
        }

        public static Dictionary<GoodsId, GoodsId> Core
        {
            get
            {
                return new Dictionary<GoodsId, GoodsId>
                {
                    { GoodsId.Ferrum, GoodsId.FerrumCore },
                    { GoodsId.Titanium, GoodsId.TitaniumCore },
                };
            }
        }
    }
}
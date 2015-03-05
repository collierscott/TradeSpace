using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static class MiningParams
    {
        public static Dictionary<LodeClass, long> Hardness = new Dictionary<LodeClass, long>
        {
            { LodeClass.A, 1 },
            { LodeClass.B, 2 },
            { LodeClass.C, 4 },
            { LodeClass.D, 8 },
            { LodeClass.E, 16 },
            { LodeClass.F, 32 },
            { LodeClass.S, 64 },
            { LodeClass.X, 128 },
            { LodeClass.Z, 256 },
        };


        public static Dictionary<GoodsId, long> Structure = new Dictionary<GoodsId, long>
        {
            { GoodsId.Ferrum, 100 },
            { GoodsId.Titanium, 200 },
        };
        
        public static Dictionary<GoodsId, GoodsId> Core = new Dictionary<GoodsId, GoodsId>
        {
            { GoodsId.Ferrum, GoodsId.FerrumCore },
            { GoodsId.Titanium, GoodsId.TitaniumCore },
        };


        public static Dictionary<EquipmentId, DrillParams> DrillParams = new Dictionary<EquipmentId, DrillParams>
        {
            {
                EquipmentId.ImpulseDrill, new DrillParams
                {
                    Class = LodeClass.A,
                    Type = DrillType.Impulse,
                    Power = 10,
                    Efficiency = 0.5f,
                    Heating = 0.02f,
                    Cooling = 0.25f
                }
            },
            {
                EquipmentId.LaserDrill, new DrillParams
                {
                    Class = LodeClass.A,
                    Type = DrillType.Laser,
                    Power = 40,
                    Efficiency = 0.5f,
                    Heating = 0.05f,
                    Cooling = 0.20f
                }
            },
        };
    }
}
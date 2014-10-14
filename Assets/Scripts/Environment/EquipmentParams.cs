﻿using System;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static DrillParams GetDrillParams(EquipmentId equipmentId)
        {
            switch (equipmentId)
            {
                case EquipmentId.ImpulseDrill:
                    return new DrillParams
                    {
                        Power = 10,
                        Class = AsteroidClass.A,
                        Type = DrillType.Impulse,
                        ReloadSeconds = 1
                    };
                case EquipmentId.LaserDrill:
                    return new DrillParams
                    {
                        Power = 10,
                        Class = AsteroidClass.A,
                        Type = DrillType.Laser,
                        HeatingRate = 10,
                        CoolingRate = 25
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
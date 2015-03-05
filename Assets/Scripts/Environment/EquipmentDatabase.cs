using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        private static readonly List<Equipment> EquipmentList = new List<Equipment>
        {
            #region Engines

            new Equipment
            {
                Id = EquipmentId.JetEngine,
                DisplayName = "Реактивный двигатель",
                Description = "Недорогой и экономичный реактивный двигатель, которым оснащаются большинство кораблей начального уровня",
                Energy = 40,
                Fuel = 200,
                Mass = 100,
                Volume = 40,
                Price = 10000,
                Type = EquipmentType.Engine,
                Bonus = 100
            },

            #endregion

            #region Hyper accelerators

            new Equipment
            {
                Id = EquipmentId.HyperAccelerator,
                DisplayName = "Гиперпространственный ускоритель",
                Description = "Гиперпространственный ускоритель начального уровня",
                Energy = 20,
                Mass = 20,
                Volume = 10,
                Price = 10000,
                Type = EquipmentType.Hyper,
                Bonus = 2
            },

            #endregion

            #region Mass

            new Equipment
            {
                Id = EquipmentId.MassKit,
                DisplayName = "Комплект для увеличения грузоподъемности корабля",
                Description = "Модернизация корабля с применением облегченных материалов позволяет увеличить грузоподъемность",
                Mass = 50,
                Volume = 20,
                Price = 4000,
                Type = EquipmentType.MassKit,
                Bonus = 200
            },

            #endregion

            #region Volume

            new Equipment
            {
                Id = EquipmentId.VolumeKit,
                DisplayName = "Комплект для увеличения вместительности грузового отсека",
                Description = "Модернизация грузового отсека и улучшение эргономики позволяет увеличить вместительность грузового отсека",
                Mass = 50,
                Volume = 20,
                Price = 4000,
                Type = EquipmentType.VolumeKit,
                Bonus = 200
            },

            #endregion

            #region Drill

            new Equipment
            {
                Id = EquipmentId.ImpulseDrill,
                DisplayName = "Импульсный бур для астеройдов",
                Description = "Описание ...",
                Energy = 20,
                Mass = 60,
                Volume = 40,
                Price = 7500,
                Type = EquipmentType.Drill,
                Bonus = 0
            },

            new Equipment
            {
                Id = EquipmentId.LaserDrill,
                DisplayName = "Лазерный бур для астеройдов",
                Description = "Описание ...",
                Energy = 35,
                Mass = 75,
                Volume = 40,
                Price = 9000,
                Type = EquipmentType.Drill,
                Bonus = 0
            },

            #endregion
        };

        public static readonly Dictionary<EquipmentId, Equipment> EquipmentDatabase = EquipmentList.ToDictionary(i => i.Id);
    }
}
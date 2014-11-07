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
                Id = EquipmentId.JetEngine100,
                Name = "Реактивный двигатель J100",
                Description = "Недорогой и экономичный реактивный двигатель, которым оснащаются большинство кораблей начального уровня",
                Mass = 100,
                Volume = 40,
                Price = 10000,
                Type = EquipmentType.Engine,
                BonusAdd = 100
            },

            #endregion

            #region Mass

            new Equipment
            {
                Id = EquipmentId.MassKit100,
                Name = "Комплект для увеличения грузоподъемности корабля",
                Description = "Модернизация корабля с применением облегченных материалов позволяет увеличить грузоподъемность",
                Mass = 50,
                Volume = 20,
                Price = 4000,
                Type = EquipmentType.MassKit,
                BonusAdd = 200
            },

            #endregion

            #region Volume

            new Equipment
            {
                Id = EquipmentId.VolumeKit100,
                Name = "Комплект для увеличения вместительности грузового отсека",
                Description = "Модернизация грузового отсека и улучшение эргономики позволяет увеличить вместительность грузового отсека",
                Mass = 50,
                Volume = 20,
                Price = 4000,
                Type = EquipmentType.VolumeKit,
                BonusAdd = 200
            },

            #endregion

            #region Drill

            new Equipment
            {
                Id = EquipmentId.ImpulseDrill100,
                Name = "Импульсный бур для астеройдов",
                Description = "Описание ...",
                Mass = 60,
                Volume = 40,
                Price = 7500,
                Type = EquipmentType.Drill,
                BonusAdd = 0
            },

            new Equipment
            {
                Id = EquipmentId.LaserDrill100,
                Name = "Лазерный бур для астеройдов",
                Description = "Описание ...",
                Mass = 75,
                Volume = 40,
                Price = 9000,
                Type = EquipmentType.Drill,
                BonusAdd = 0
            },

            #endregion
        };

        public static readonly Dictionary<EquipmentId, Equipment> EquipmentDatabase = EquipmentList.ToDictionary(i => i.Id);
    }
}
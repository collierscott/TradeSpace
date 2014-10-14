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
            new Equipment { Id = EquipmentId.Armor, Description = "", Mass = 40, Volume = 40, Price = 1000,
                Type = EquipmentType.Armor, BonusAdd = 100 },
            new Equipment { Id = EquipmentId.EngineReactive, Description = "", Mass = 40, Volume = 40, Price = 1000,
                Type = EquipmentType.Engine, BonusAdd = 200 }
        };

        public static readonly Dictionary<EquipmentId, Equipment> EquipmentDatabase = EquipmentList.ToDictionary(i => i.Id);
    }
}
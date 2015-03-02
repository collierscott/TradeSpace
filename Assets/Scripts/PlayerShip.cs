using System;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts
{
    public class PlayerShip
    {
        private readonly MemoShip _ship;
        private readonly Ship _params;

        public PlayerShip(MemoShip ship)
        {
            _ship = ship;
            _params = Env.ShipDatabase[_ship.Id];
        }

        public string DisplayName
        {
            get { return Env.ShipDatabase[_ship.Id].DisplayName; }
        }

        public float Speed
        {
            get
            {
                var engine = _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).SingleOrDefault(i => i.Type == EquipmentType.Engine);

                if (engine == null)
                {
                    throw new Exception("engine == null");
                }

                return _params.Speed + engine.BonusAdd + (_params.Speed * engine.BonusMultiply);
            }
        }

        public float HyperSpeed
        {
            get
            {
                float speed = 0;

                foreach (var equipment in _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Where(i => i.Type == EquipmentType.Hyper))
                {
                    speed += equipment.BonusAdd;
                }

                return speed;
            }
        }

        public long Mass
        {
            get
            {
                var mass = _params.Mass;

                foreach (var equipment in _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Where(i => i.Type == EquipmentType.MassKit))
                {
                    mass += equipment.BonusAdd + (_params.Mass * equipment.BonusMultiply);
                }

                return mass;
            }
        }

        public long Volume
        {
            get
            {
                var volume = _params.Volume;

                foreach (var equipment in _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Where(i => i.Type == EquipmentType.VolumeKit))
                {
                    volume += equipment.BonusAdd + (_params.Volume * equipment.BonusMultiply);
                }

                return volume;
            }
        }

        public long EquipmentSlots
        {
            get
            {
                var slots = _params.EquipmentSlots;

                foreach (var equipment in _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Where(i => i.Type == EquipmentType.Common))
                {
                    slots += equipment.BonusAdd;
                }

                return slots;
            }
        }

        public long CargoMass
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Mass)
                + _ship.Equipment.Sum(i => i.Quantity.Long * Env.EquipmentDatabase[i.Id].Mass); }
        }

        public long CargoVolume
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Volume)
                + _ship.Equipment.Sum(i => i.Quantity.Long * Env.EquipmentDatabase[i.Id].Volume); }
        }

        public bool HasFreeSlot()
        {
            return _ship.InstalledEquipment.Count < EquipmentSlots;
        }

        public long FindFreeSlot()
        {
            for (var i = 0; i < EquipmentSlots; i++)
            {
                if (_ship.InstalledEquipment.Any(j => j.Index == i)) continue;

                return i;
            }

            throw new Exception();
        }

        public CargoStatus GetCargoStatus(MemoGoods add)
        {
            var volume = Env.GoodsDatabase[add.Id].Volume * add.Quantity.Long;
            var mass = Env.GoodsDatabase[add.Id].Mass*add.Quantity.Long;

            if (volume > Volume - CargoVolume)
            {
                return CargoStatus.NoVolume;
            }

            return mass > Mass - CargoMass ? CargoStatus.NoMass : CargoStatus.Ready;
        }

        public void AddGoods(MemoGoods goods)
        {
            var owned = _ship.Goods.SingleOrDefault(i => i.Id == goods.Id);

            if (owned != null)
            {
                owned.Quantity += goods.Quantity;
            }
            else
            {
                _ship.Goods.Add(goods);
            }
        }

        public void RemoveGoods(MemoGoods goods)
        {
            var item = _ship.Goods.Single(i => i.Id == goods.Id);

            if (item.Quantity.Long == 1)
            {
                _ship.Goods.Remove(item);
            }
            else
            {
                item.Quantity -= goods.Quantity;
            }
        }

        public DrillParams Drill
        {
            get
            {
                var drill = _ship.InstalledEquipment.Single(i => Env.EquipmentDatabase[i.Id].Type == EquipmentType.Drill);

                return Env.GetDrillParams(drill.Id);
            }
        }
    }
}
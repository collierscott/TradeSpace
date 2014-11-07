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
                var engine = _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Single(i => i.Type == EquipmentType.Engine);

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

        public long GoodsMass
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Mass); }
        }

        public long GoodsVolume
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Volume); }
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
        /// <summary>
        /// Проверка свободного места
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public ShipGoodsCheck CanAddGoods(MemoGoods goods)
        {
            long goodsVolume = Env.GoodsDatabase[goods.Id].Volume * goods.Quantity.Long;
            long goodsMass = Env.GoodsDatabase[goods.Id].Mass * goods.Quantity.Long;

            ShipGoodsCheck result = ShipGoodsCheck.Unknown;

            if (goodsVolume > Volume - GoodsVolume)
                result |= ShipGoodsCheck.NoVolume;

            if (goodsMass > Mass - GoodsMass)
                result |= ShipGoodsCheck.NoMass;

            if (result == ShipGoodsCheck.Unknown)
                result = ShipGoodsCheck.Success;

            return result;
        }

        public void AddGoods(MemoGoods goods)
        {
            if (_ship.Goods.Contains(goods.Id))
            {
                _ship.Goods.Single(goods.Id).Quantity.Long += goods.Quantity.Long;
            }
            else
            {
                _ship.Goods.Add(goods);
            }
        }

        public void RemoveGoods(MemoGoods goods)
        {
            var item = _ship.Goods.Single(goods.Id);

            if (_ship.Goods.Single(goods.Id).Quantity.Long == 1)
            {
                _ship.Goods.Remove(item);
            }
            else
            {
                item.Quantity.Long -= goods.Quantity.Long;
            }
        }
    }
}
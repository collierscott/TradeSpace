using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerShip // TODO: Refact, extract
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

        public long Speed
        {
            get { return GetValue(_params.Speed, EquipmentType.Engine); }
        }

        public float Hyper
        {
            get { return Math.Max(1, GetValue(0, EquipmentType.Hyper)); }
        }

        public long Energy
        {
            get { return GetValue(_params.Energy, EquipmentType.Generator); }
        }

        public long Mass
        {
            get { return GetValue(_params.Mass, EquipmentType.MassKit); }
        }

        public long Volume
        {
            get { return GetValue(_params.Volume, EquipmentType.VolumeKit); }
        }

        public long Equipment
        {
            get { return GetValue(_params.Equipment, EquipmentType.EquipmentExtend); }
        }

        public long FuelConsumption
        {
            get { return _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Sum(i => i.Fuel); }
        }

        public long MassUsed
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Mass) + _ship.Equipment.Sum(i => i.Quantity.Long * Env.EquipmentDatabase[i.Id].Mass); }
        }

        public long VolumeUsed
        {
            get { return _ship.Goods.Sum(i => i.Quantity.Long * Env.GoodsDatabase[i.Id].Volume) + _ship.Equipment.Sum(i => i.Quantity.Long * Env.EquipmentDatabase[i.Id].Volume); }
        }

        public long EnergyUsed
        {
            get { return _ship.Equipment.Sum(i => i.Quantity.Long * Env.EquipmentDatabase[i.Id].Energy); }
        }

        public bool HasFreeSlot()
        {
            return _ship.InstalledEquipment.Count < Equipment;
        }

        public long FindFreeSlot()
        {
            for (var i = 0; i < Equipment; i++)
            {
                if (_ship.InstalledEquipment.Any(j => j.Index == i)) continue;

                return i;
            }

            throw new Exception();
        }

        public CargoStatus GetCargoStatus(MemoGoods add)
        {
            var volume = Env.GoodsDatabase[add.Id].Volume * add.Quantity.Long;
            var mass = Env.GoodsDatabase[add.Id].Mass * add.Quantity.Long;

            if (volume > Volume - VolumeUsed)
            {
                return CargoStatus.NoVolume;
            }

            return mass > Mass - MassUsed ? CargoStatus.NoMass : CargoStatus.Ready;
        }

        public void AddGoods(MemoGoods goods)
        {
            if (GetCargoStatus(goods) != CargoStatus.Ready)
            {
                throw new Exception();
            }

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

        public bool CanInstallEquipment(EquipmentId equipment)
        {
            if (EnergyUsed + Env.EquipmentDatabase[equipment].Energy > Energy)
            {
                return false;
            }

            return true;
        }

        public long CalcRouteFuel(List<RouteNode> route)
        {
            double fuel = 0;
            var fpm = FuelConsumption;

            for (var i = 1; i < route.Count; i++)
            {
                var time = route[i].Time - route[i - 1].Time;

                if (route[i].System == route[i - 1].System)
                {
                    fuel += fpm * time.TotalMinutes;
                }
            }

            Debug.Log(string.Format("Fuel required: {0}", (long) fuel));

            return (long) fuel;
        }

        public bool HasEngine()
        {
            return _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Any(i => i.Type == EquipmentType.Engine);
        }

        public bool HasHyper()
        {
            return _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Any(i => i.Type == EquipmentType.Hyper);
        }

        private long GetValue(long value, EquipmentType type)
        {
            foreach (var equipment in _ship.InstalledEquipment.Select(i => Env.EquipmentDatabase[i.Id]).Where(i => i.Type == type))
            {
                value += equipment.Bonus + (long) (_params.Mass * equipment.BonusM);
            }

            return value;
        }
    }
}
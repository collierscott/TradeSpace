using System;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Environment;

namespace Assets.Scripts
{
    public class PlayerShip
    {
        private readonly MemoShip _ship;

        public PlayerShip(MemoShip ship)
        {
            _ship = ship;
        }

        public string DisplayName
        {
            get { return Env.ShipDatabase[_ship.Id].DisplayName; }
        }

        public long Mass
        {
            get { return Env.ShipDatabase[_ship.Id].Mass; }
        }

        public long Volume
        {
            get { return Env.ShipDatabase[_ship.Id].Volume; }
        }

        public long EquipmentSlots
        {
            get { return Env.ShipDatabase[_ship.Id].EquipmentSlots; }
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

        public void AppendGoods(MemoGoods goods)
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

        public void SubstractGoods(MemoGoods goods)
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
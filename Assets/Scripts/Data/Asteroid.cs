using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Data
{
    public class Asteroid : Location
    {
        public List<Lode> Parts;

        public List<GoodsId> Minerals
        {
            get { return Parts.Select(i => i.Mineral).Distinct().ToList(); }
        }

        public void MergeSample(List<int> exhausted) // TODO: just a sample for merging with profile data
        {
            foreach (var i in exhausted)
            {
                Parts[i] = null;
            }
        }
    }

    public class Lode
    {
        public GoodsId Mineral = GoodsId.Ferrum;
        /// <summary>
        /// Класс, определяет минимальный класс бура для бурения
        /// </summary>
        public AsteroidClass Class = AsteroidClass.A;
        /// <summary>
        /// Размер астеройда для отображения
        /// </summary>
        public int Size = 1;
        /// <summary>
        /// Радиус вращения
        /// </summary>
        public float Radius;
        /// <summary>
        /// Скорость вражения
        /// </summary>
        public int Speed = 10;
        /// <summary>
        /// Жесткость, влиляет на кол-во кликов
        /// </summary>
        public float Hardness = 1;
        /// <summary>
        /// Количество руды
        /// </summary>
        public int Quantity = 1;

        public float Structure
        {
            get { return Size * Env.GetMineralStructure(Mineral) * Hardness; }
        }

        public bool HasCore
        {
            get { return CRandom.Chance(Quantity * Size * Size / 1000); }
        }   
        /// <summary>
        /// Объем для грузового отсека
        /// </summary>
        public long Volume
        {
            get { return (long)(Quantity * Env.GoodsDatabase[Mineral].Volume); }
        }
        /// <summary>
        /// Масса для грузового отсека
        /// </summary>
        public long Mass
        {
            get { return (long)(Quantity * Env.GoodsDatabase[Mineral].Mass); }
        }
    }
}
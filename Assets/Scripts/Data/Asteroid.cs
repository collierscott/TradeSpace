using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class Asteroid : Location
    {
        public List<AsteroidPart> Parts;

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

    public class AsteroidPart
    {
        public GoodsId Mineral = GoodsId.Ferrum;
        public AsteroidClass Class = AsteroidClass.A;
        public long Size = 0;
        public long Speed = 10;
        public float Hardness = 1;
        public float Quantity = 1;

        public float Structure
        {
            get { return Size * Env.GetMineralStructure(Mineral) * Hardness; }
        }

        public bool HasCore
        {
            get { return CRandom.Chance(Quantity * Size * Size / 1000); }
        }

        public float GetVolumeSample // TODO: just a sample for scaling asteroid parts
        {
            get { return Mathf.Pow(Size, 0.33f); }
        }
    }
}
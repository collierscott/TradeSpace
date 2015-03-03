using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class Lode
    {
        public LodeClass Class;
        public GoodsId Mineral;
        public int Size;
        public float Radius;
        public float Speed;
        
        public float Structure
        {
            get
            {
                return Size * MiningParams.Hardness[Class] * MiningParams.Structure[Mineral];
            }
        }

        public float Scale
        {
            get
            {
                return 0.2f * Mathf.Pow(Size, 0.5f);
            }
        }

        public float CoreChance
        {
            get
            {
                return Size * Mathf.Pow(Size, 2) / 1000;
            }
        }
    }
}
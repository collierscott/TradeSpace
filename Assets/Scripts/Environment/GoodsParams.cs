using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static long GetMineralStructure(GoodsId mineral)
        {
            switch (mineral)
            {
                case GoodsId.Ferrum:
                    return 100;
                case GoodsId.Titanium:
                    return 200;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
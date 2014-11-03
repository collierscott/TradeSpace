using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static Dictionary<GoodsId, GoodsId> AsteroidCoreDatabase = new Dictionary<GoodsId, GoodsId>
        {
            {GoodsId.Ferrum, GoodsId.Processor },
            {GoodsId.Titanium, GoodsId.SmartTv },
        };
    }
}

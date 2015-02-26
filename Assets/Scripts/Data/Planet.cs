using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Data
{
    public class Planet : Location
    {
        public PlanetType Type;
        public bool Inhabited;
        public List<ShopGoods> Export = new List<ShopGoods>();
        public List<GoodsType> Import = new List<GoodsType>();
        public List<ShopGoods> SpecialImport = new List<ShopGoods>();
        public double ImportRate = 1;
        public double ExportRate = 1;
        public double Tax = 0.3f;
        public double PriceDelta = 0.1f;

        public long TechLevel
        {
            get { return Export.Select(i => i.Id).Select(i => Env.GoodsDatabase[i].TechLevel).Max(); }
        }

        public long ProductionLevel
        {
            get { return Export.Select(i => (i.Min + i.Max) / 2).Sum(); }
        }

        public List<GoodsType> ExportType
        {
            get { return Export.Select(i => Env.GoodsDatabase[i.Id].Type).Distinct().ToList(); }
        }
    }
}
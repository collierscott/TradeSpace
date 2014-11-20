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
        public List<ShopGoods> Export;
        public List<GoodsType> ImportType;
        public List<ShopGoods> SpecialImport;
        public float ImportPriceRate = 1;
        public float PriceRate = 1;

        public long TechLevel
        {
            get
            {
                return Export.Select(i => i.Id).Select(i => Env.GoodsDatabase[i].TechLevel).Max();
            }
        }

        public long ProductionLevel
        {
            get
            {
                return Export.Select(i => (i.MinQuantity + i.MaxQuantity) / 2).Sum();
            }
        }

        public List<GoodsType> ExportType
        {
            get
            {
                return Export.Select(i => Env.GoodsDatabase[i.Id].Type).Distinct().ToList();
            }
        }
    }
}
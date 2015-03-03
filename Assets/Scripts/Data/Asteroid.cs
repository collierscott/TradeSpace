using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class Asteroid : Location
    {
        public List<Lode> Lodes;

        public List<GoodsId> Minerals
        {
            get { return Lodes.Select(i => i.Mineral).Distinct().ToList(); }
        }

        public void Merge(List<int> exhausted) // TODO: just a sample for merging with profile data
        {
            foreach (var i in exhausted)
            {
                Lodes[i] = null;
            }
        }
    }
}
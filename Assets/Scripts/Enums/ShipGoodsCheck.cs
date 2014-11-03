using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enums
{
    public enum ShipGoodsCheck
    {
        Unknown = 0,
        Success = 1,
        NoVolume = 2,
        NoMass = 4,
    }
}

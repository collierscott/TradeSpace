using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enums
{
    public enum ShipGoodsCheck
    {
        Success = 0,
        NoVolume = 1,
        NoMass = 2,
        NoMassAndVolume = 3,
    }
}

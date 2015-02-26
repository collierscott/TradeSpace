using System;

namespace Assets.Scripts.Enums
{
    [Obsolete("Ненужная надстройка, просьба избавиться от нее")]
    public enum ShipGoodsCheck // TODO: Remove
    {
        Success = 0,
        NoVolume = 1,
        NoMass = 2,
        NoMassAndVolume = 3,
    }
}
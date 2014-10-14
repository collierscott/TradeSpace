using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public class Station : Location
    {
        public List<ShopEquipment> Equipments;
        public float PriceRate = 1;
    }
}
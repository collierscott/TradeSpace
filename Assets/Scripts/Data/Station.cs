using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public class Station : Location
    {
        public List<ShopEquipment> Equipment;
        public List<ShopShip> Ships;
        public double ImportRate = 1;
        public double ExportRate = 1;
        public double Tax = 0.3f;
        public double PriceDelta = 0.1f;
    }
}
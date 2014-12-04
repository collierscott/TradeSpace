using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public class Status : Base
    {
        public UILabel ShipName;
        public UILabel Credits;
        
        public void Refresh()
        {
            if (Current is ShipShop)
            {
                ShipName.text = null;
            }
            else
            {
                var ship = new PlayerShip(Profile.Instance.Ship);

                ShipName.SetText(ship.DisplayName);
            }

            Credits.SetText("{0} credits", Profile.Instance.Credits.Long);
        }
    }
}
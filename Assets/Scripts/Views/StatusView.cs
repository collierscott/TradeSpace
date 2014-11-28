using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public class StatusView : ViewBase
    {
        public UILabel ShipName;
        public UILabel Credits;
        
        public void Refresh()
        {
            if (Current is ShipShopView)
            {
                ShipName.SetText(null);
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
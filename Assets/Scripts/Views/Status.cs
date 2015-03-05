using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public class Status : UI
    {
        public UILabel ShipName;
        public UILabel Credits;
        
        public void Refresh()
        {
            if (UIScreen.Current is ShipShop)
            {
                ShipName.text = null;
            }
            else
            {
                ShipName.SetText(Profile.Instance.PlayerShip.DisplayName);
            }

            Credits.SetText("{0} credits", Profile.Instance.Credits.Long);
        }
    }
}
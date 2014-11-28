using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class CargoView : ViewBase
    {
        public UILabel Mass;
        public UISprite MassProgress;
        public UILabel Volume;
        public UISprite VolumeProgress;

        protected override void Initialize()
        {
            Refresh();
        }

        public void Refresh()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            Mass.SetText("{0}/{1}", ship.CargoMass, ship.Mass);
            MassProgress.transform.localScale = new Vector2((float)ship.CargoMass / ship.Mass, 1);

            Volume.SetText("{0}/{1}", ship.CargoVolume, ship.Volume);
            VolumeProgress.transform.localScale = new Vector2((float)ship.CargoVolume / ship.Volume, 1);
        }
    }
}
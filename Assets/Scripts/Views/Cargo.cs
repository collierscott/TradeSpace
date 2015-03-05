using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Cargo : UI
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
            var ship = Profile.Instance.PlayerShip;

            Mass.SetText("{0}/{1}", ship.MassUsed, ship.Mass);
            MassProgress.transform.localScale = new Vector2((float)ship.MassUsed / ship.Mass, 1);

            Volume.SetText("{0}/{1}", ship.VolumeUsed, ship.Volume);
            VolumeProgress.transform.localScale = new Vector2((float)ship.VolumeUsed / ship.Volume, 1);
        }
    }
}
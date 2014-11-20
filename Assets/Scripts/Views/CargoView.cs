using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class CargoView : ViewBase
    {
        public UILabel ShipNameText;
        public UILabel CreditsText;
        public UILabel MassText;
        public UISprite MassSprite;
        public UILabel VolumeText;
        public UISprite VolumeSprite;

        protected override void Initialize()
        {
            Refresh(0);
        }

        public void Refresh()
        {
            Refresh(0.4f);
        }

        private void Refresh(float delay)
        {
            var ship = new PlayerShip(Profile.Instance.Ship);

            ShipNameText.SetText(ship.DisplayName);
            CreditsText.SetText("{0} credits", Profile.Instance.Credits.Long);
            MassText.SetText("{0}/{1}", ship.CargoMass, ship.Mass);
            VolumeText.SetText("{0}/{1}", ship.CargoVolume, ship.Volume);
            MassSprite.transform.localScale = new Vector2((float) ship.CargoMass / ship.Mass, 1);
            VolumeSprite.transform.localScale = new Vector2((float) ship.CargoVolume / ship.Volume, 1);

            //TweenScale.Begin(MassSprite.gameObject, delay, new Vector2((float) ship.GoodsMass / ship.Mass, 1));
            //TweenScale.Begin(VolumeSprite.gameObject, delay, new Vector2((float) ship.GoodsVolume / ship.Volume, 1));

            //foreach (var sprite in new[] { MassSprite, VolumeSprite })
            //{
            //    var color = Color.green;

            //    if (sprite.transform.localScale.x > 0.8)
            //    {
            //        color = Color.red;
            //    }
            //    else if (sprite.transform.localScale.x > 0.5)
            //    {
            //        color = Color.yellow;
            //    }

            //    TweenColor.Begin(sprite.gameObject, delay, color);
            //}
        }
    }
}
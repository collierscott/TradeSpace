using System;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;

namespace Assets.Scripts.Behaviour
{
    public class ShipButton : Script
    {
        public SelectButton Button;
        public UISprite Image;
        public UISprite State;
        public UISprite Mass;
        public UISprite Volume;
        public UILabel Timer;

        public string UniqName { get; private set; }

        public void Initialize(string uniqName)
        {
            UniqName = uniqName;
            Button.Selected += () => SelectManager.SelectShip(uniqName);
            Button.Confirmed += Focus;

            if (Profile.Instance.SelectedShip == uniqName)
            {
                Button.Pressed = true;
            }

            Image.spriteName = Env.ShipDatabase[Profile.Instance.Ships[UniqName].Id].Image;
        }

        public void Update()
        {
            var ship = Profile.Instance.Ships[UniqName];

            if (ship.State == ShipState.InFlight)
            {
                var departure = ship.Route.First().Time;
                var arrival = ship.Route.Last().Time;
                var left = arrival - DateTime.UtcNow;

                if (left.TotalSeconds < 0)
                {
                    left = TimeSpan.FromSeconds(0);
                }

                Timer.SetText(string.Format("{0}:{1:D2}:{2:D2}", Math.Floor(left.TotalHours), left.Minutes, left.Seconds));
                State.fillAmount = (float) (left.TotalSeconds / (arrival - departure).TotalSeconds);
            }
            else
            {
                State.fillAmount = 0;
                Timer.SetText(null);
            }

            var playerShip = new PlayerShip(ship);

            Mass.fillAmount = 0.5f + 0.5f * playerShip.CargoMass / playerShip.Mass;
            Volume.fillAmount = 0.5f + 0.5f * playerShip.CargoVolume / playerShip.Volume;
        }

        private static void Focus()
        {
            var position = -SelectManager.Ship.Location.Position;

            if (UIScreen.Current is Galaxy)
            {
                if (SelectManager.Ship.Location.System != null)
                {
                    position = -Env.Galaxy[SelectManager.Ship.Location.System].Position;
                }

                Find<TweenMap>().Focus(position);
            }
            else
            {
                if (SelectManager.Ship.Location.System == SelectManager.System)
                {
                    Find<TweenMap>().Focus(position);
                }
                else if (SelectManager.Ship.Location.System == null)
                {
                    Find<Galaxy>().Open(() => Find<TweenMap>().Focus(position));
                }
                else
                {
                    Find<Systema>().Open(() => SelectManager.SelectSystem(SelectManager.Ship.Location.System), () => Find<TweenMap>().Focus(position));
                }
            }
        }
    }
}
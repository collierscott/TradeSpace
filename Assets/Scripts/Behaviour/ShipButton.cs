using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class ShipButton : Script
    {
        public SelectButton Button;
        public UISprite Image;
        public UISprite State;
        public UISprite Mass;
        public UISprite Volume;

        public string UniqName { get; private set; }

        public void Initialize(string uniqName)
        {
            UniqName = uniqName;
            Button.Selected += () => SelectManager.SelectShip(uniqName);

            if (Profile.Instance.SelectedShip == uniqName)
            {
                Button.Pressed = true;
            }

            var ship = new PlayerShip(Profile.Instance.Ships[UniqName]);

            Image.spriteName = Env.ShipDatabase[Profile.Instance.Ships[UniqName].Id].Image;
            Mass.transform.localScale = new Vector2(1, (float) ship.CargoMass / ship.Mass);
            Volume.transform.localScale = new Vector2(1, (float) ship.CargoVolume / ship.Volume);
        }

        public void Update()
        {
            switch (Ships.ShipBehaviours[UniqName].State)
            {
                case ShipState.InFlight:
                    State.color = Color.yellow;
                    break;
                default:
                    State.color = Color.green;
                    break;
            }
        }
    }
}
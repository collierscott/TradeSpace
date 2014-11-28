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

        public int Index { get; private set; }

        public void Initialize(int index)
        {
            Index = index;
            Button.Selected += () => SelectManager.SelectShip(index);

            if (Profile.Instance.SelectedShip == index)
            {
                Button.Pressed = true;
            }

            var ship = new PlayerShip(Profile.Instance.Ships[Index]);

            Image.spriteName = Env.ShipDatabase[Profile.Instance.Ships[Index].Id].Image;
            Mass.transform.localScale = new Vector2(1, (float) ship.CargoMass / ship.Mass);
            Volume.transform.localScale = new Vector2(1, (float) ship.CargoVolume / ship.Volume);
        }

        public void Update()
        {
            switch (ShipView.Ships[Index].State)
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
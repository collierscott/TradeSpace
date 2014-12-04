using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ShipSelect : Base
    {
        private readonly List<ShipButton> _buttons = new List<ShipButton>(); 

        public void Refresh()
        {
            foreach (var button in _buttons)
            {
                var ship = Ships.ShipBehaviours[button.UniqName];

                if (Current is Galaxy || Current is System)
                {
                    button.Button.Enabled = true;
                    button.GetComponent<UIWidget>().alpha = 1;
                }
                else
                {
                    button.Button.Enabled = ship.State == ShipState.Ready
                        && ship.Location.System == SelectManager.Location.System
                        && ship.Location.Name == SelectManager.Location.Name;
                    button.GetComponent<UIWidget>().alpha = button.Button.Enabled ? 1 : 0.5f;
                }
            }
        }

        protected override void Initialize()
        {
            for (var i = 0; i < Profile.Instance.Ships.Count; i++)
            {
                var selector = PrefabsHelper.InstantiateShipButton(Camera.main.transform);
                var button = selector.GetComponentInChildren<ShipButton>();

                selector.transform.parent = Panel;
                selector.transform.localPosition = new Vector2(0, 110 * ((Profile.Instance.Ships.Count - 1) / 2f) - 110 * i);
                button.Initialize(Profile.Instance.Ships.ElementAt(i).Key);

                _buttons.Add(button);
            }
        }

        protected override void Cleanup()
        {
            _buttons.Clear();
            Panel.Clear();
        }
    }
}
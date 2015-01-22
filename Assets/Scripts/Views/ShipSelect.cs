using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ShipSelect : UI
    {
        private readonly List<ShipButton> _buttons = new List<ShipButton>(); 

        public void Refresh()
        {
            foreach (var button in _buttons)
            {
                var ship = Ships.ShipBehaviours[button.UniqName];

                if (UIScreen.Current is Galaxy || UIScreen.Current is System)
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
                var uniqName = Profile.Instance.Ships.ElementAt(i).Key;
                var instance = PrefabsHelper.InstantiateShipButton(Camera.main.transform);
                var button = instance.GetComponent<ShipButton>();

                instance.name += "#" + uniqName;
                instance.transform.parent = Panel;
                instance.transform.localPosition = new Vector2(75, 150 * ((Profile.Instance.Ships.Count - 1) / 2f) - 150 * i);
                button.Initialize(uniqName);
                _buttons.Add(button);
            }
        }

        protected override void Cleanup()
        {
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
            }

            _buttons.Clear();
        }
    }
}
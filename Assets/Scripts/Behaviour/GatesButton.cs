using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class GatesButton : Script
    {
        public UILabel Name;
        public UISprite Image;
        public SelectButton Button;

        public void Initialize(Location location)
        {
            var connected = Env.Galaxy[location.ToGates().ConnectedSystem];
        
            Name.text = string.Format("Gate to {0}", connected.System);
            Image.color = connected.Color;
            transform.localPosition = location.Position;

            Button.Confirmed += () =>
            {
                SelectManager.SelectSystem(connected.System);
                FindObjectOfType<SystemView>().Open();
            };
        }

        public void Update()
        {
            Image.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
    }
}
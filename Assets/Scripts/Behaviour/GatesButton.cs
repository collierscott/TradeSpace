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
            var connectedSystem = Env.Galaxy[location.ToGates().ConnectedSystem];
        
            Name.text = string.Format("Gate to {0}", connectedSystem.System);
            Image.color = connectedSystem.Color;
            transform.localPosition = location.Position;

            Button.Confirmed += () =>
            {
                SelectManager.SelectSystem(connectedSystem.System);
                FindObjectOfType<Systema>().Open();
            };
        }

        public void Update()
        {
            Image.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
    }
}
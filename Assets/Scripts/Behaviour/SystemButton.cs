using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Behaviour
{
    public class SystemButton : Script
    {
        public UILabel Name;
        public UISprite Halo;
        public SelectButton Button;

        public void Initialize(Location location)
        {
            Name.text = location.System;
            Halo.color = location.Color;
            Button.ColorDown = location.Color.SetAlpha(1);
            transform.localPosition = location.Position;

            Button.Selected += () => SelectManager.SelectSystem(location.System);
            Button.Confirmed += Find<ActionManager>().Open;

            if (SelectManager.System == location.System)
            {
                Button.Pressed = true;
            }
        }
    }
}
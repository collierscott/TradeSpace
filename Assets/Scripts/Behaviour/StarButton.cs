using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class StarButton : Script
    {
        public UILabel Name;
        public UISprite Halo;
        public SelectButton Button;

        public void Initialize(Location location)
        {
            transform.localPosition = Vector2.zero;
            Name.text = location.System;
            Halo.color = location.Color;
            Button.ColorDown = location.Color.SetAlpha(1);
            Button.Confirmed += FindObjectOfType<ActionManager>().CloseScreen;
        }
    }
}
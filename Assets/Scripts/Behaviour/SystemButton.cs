using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class SystemButton : Script
    {
        public UILabel Name;
        public UISprite Halo;
        public SelectButton Button;
        public GameButton InfoButton;
        public GameButton MoveButton;
        public GameButton OpenButton;

        private const float TweenTime = 0.25f;
        private const float TweenScale = 1.2f;
        private bool _expanded;
    
        public void Initialize(Location location)
        {
            Name.text = location.System;
            Halo.color = location.Color;
            Button.ColorDown = location.Color.SetAlpha(1);
            transform.localPosition = location.Position;
        
            Button.Selected += () => Expand(location.System);
            Button.Unselected += Collapse;

            Button.Confirmed += FindObjectOfType<SystemView>().Open;
            OpenButton.Up += FindObjectOfType<SystemView>().Open;
        }

        public void Update()
        {
            if (!_expanded) return;

            InfoButton.Enabled = false;
            MoveButton.Enabled = false;
            OpenButton.Enabled = true;
        }

        private void Expand(string system)
        {
            _expanded = true;

            TweemButton(InfoButton);
            TweemButton(MoveButton);
            TweemButton(OpenButton);

            SelectManager.SelectSystem(system);
        }

        private void Collapse()
        {
            _expanded = false;

            TweemButton(InfoButton);
            TweemButton(MoveButton);
            TweemButton(OpenButton);
        }

        private void TweemButton(Component button)
        {
            button.gameObject.SetActive(true);
            TweenPosition.Begin(button.gameObject, TweenTime, _expanded ? button.transform.localPosition * TweenScale : button.transform.localPosition / TweenScale);
            TweenAlpha.Begin(button.gameObject, TweenTime, _expanded ? 1 : 0);
        }
    }
}
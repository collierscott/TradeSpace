using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class LocationButton : Script
    {
        public UILabel Name;
        public UISprite Image;
        public SelectButton Button;
        public GameButton InfoButton;
        public GameButton MoveButton;
        public GameButton OpenButton;

        private const float TweenTime = 0.25f;
        private const float TweenScale = 1.2f;
        private bool _expanded;
        private bool _traced;

        public virtual void Initialize(Location location)
        {
            Name.text = location.Name;

            if (Image != null)
            {
                Image.spriteName = location.Image;
            }

            transform.localPosition = location.Position;

            if (location.NamePosition != Vector2.right)
            {
                if (location.NamePosition == -Vector2.right)
                {
                    var p = Name.transform.localPosition;

                    Name.pivot = UIWidget.Pivot.Right;
                    Name.transform.localPosition = -p;
                }
            }

            Subscribe(location);
            InfoButton.Enabled = MoveButton.Enabled = OpenButton.Enabled = false;
        }

        public void Update()
        {
            if (!_expanded) return;

            var open = SelectManager.Ship != null
                && SelectManager.Ship.Location.Name == SelectManager.Location.Name;
            var move = SelectManager.Ship != null
                && SelectManager.Location.Name != SelectManager.Ship.Location.Name && SelectManager.Ship.State != ShipState.InFlight;

            InfoButton.Enabled = true;
            MoveButton.Enabled = move;
            OpenButton.Enabled = open;
        }

        protected void Expand()
        {
            if (_expanded) return;

            _expanded = true;

            TweemButton(InfoButton);
            TweemButton(MoveButton);
            TweemButton(OpenButton);
        }

        protected void Collapse()
        {
            _expanded = _traced = false;

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
    
        private void Subscribe(Location location)
        {
            Button.Selected += () =>
            {
                SelectManager.SelectLocation(location);
                Expand();
            };
            Button.Unselected += Collapse;
            InfoButton.Down += () => ViewBase.ActionManager.ShowInfo(location);
            MoveButton.Down += () =>
            {
                if (_traced)
                {
                    ViewBase.ActionManager.MoveShip(location);
                }
                else
                {
                    ViewBase.ActionManager.TraceRoute(location);
                    _traced = true;
                }
            };

            var open = new Action(() =>
            {
                if (OpenButton.Enabled)
                {
                    Open();
                }
            });

            Button.Confirmed += open;
            OpenButton.Down += open;
        }

        protected virtual void Open()
        {
            ViewBase.ActionManager.Open();
        }
    }
}
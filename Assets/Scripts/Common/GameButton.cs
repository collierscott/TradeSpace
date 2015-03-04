using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class GameButton : Script
    {
        public Color ColorDown = Color.white;
        public Color ColorDisabled = Color.gray;
        public float ScaleDown = 1.2f;
        public float TweenTimeDown = 0.1f;
        public float TweenTimeUp = 0.4f;
        public MonoBehaviour Listener;
        public string ListenerMethodDown;
        public string ListenerMethodUp;
        public string Params;
        public List<EventDelegate> EventDown = new List<EventDelegate>();
        public List<EventDelegate> EventUp = new List<EventDelegate>();
        public event Action Down = () => {};
        public event Action Up = () => {};
        public Color Color = Color.clear;

        private bool _enabled = true;

        public void Awake()
        {
            if (Color == Color.clear)
            {
                PickColor();
            }
        }

        private bool _down;

        public void Update()
        {
            if (_down)
            {
                if (!collider2D.bounds.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    OnPress(false);
                }
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value) return;

                _enabled = value;
                GetComponent<Collider2D>().enabled = _enabled;
                TweenEnabled(_enabled, TweenTimeDown);
            }
        }

        public void ChangeColor(Color color, float duration)
        {
            Color = color;
            TweenColor.Begin(gameObject, duration, Color);
        }

        protected virtual void OnPress(bool down)
        {
            if (!enabled) return;

            Tween(down);

            if (down)
            {
                ActionDown();
                Down();
            }
            else
            {
                if (_down)
                {
                    ActionUp();
                    Up();
                }
            }

            _down = down;
        }

        protected virtual void ActionDown()
        {
            if (Listener != null && !string.IsNullOrEmpty(ListenerMethodDown))
            {
                if (string.IsNullOrEmpty(Params))
                {
                    Listener.SendMessage(ListenerMethodDown);
                }
                else
                {
                    Listener.SendMessage(ListenerMethodDown, Params);
                }
            }

            if (EventUp.Count > 0)
            {
                EventDelegate.Execute(EventUp);
            }
        }

        protected virtual void ActionUp()
        {
            if (Listener != null && !string.IsNullOrEmpty(ListenerMethodUp))
            {
                if (string.IsNullOrEmpty(Params))
                {
                    Listener.SendMessage(ListenerMethodUp);
                }
                else
                {
                    Listener.SendMessage(ListenerMethodUp, Params);
                }
            }

            if (EventDown.Count > 0)
            {
                EventDelegate.Execute(EventDown);
            }
        }

        protected virtual void Tween(bool down)
        {
            if (down)
            {
                TweenColor.Begin(gameObject, TweenTimeDown, ColorDown);
                TweenScale.Begin(gameObject, TweenTimeDown, ScaleDown * Vector3.one);
            }
            else
            {
                TweenColor.Begin(gameObject, TweenTimeUp, Color);
                TweenScale.Begin(gameObject, TweenTimeUp, Vector3.one);
            }
        }

        private void TweenEnabled(bool value, float duration)
        {
            TweenColor.Begin(gameObject, duration, value ? Color : ColorDisabled);
        }

        private void PickColor()
        {
            var widget = GetComponent<UIWidget>();

            Color = widget != null ? widget.color : Color.white;
        }
    }
}
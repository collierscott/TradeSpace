using System;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class TweenMap : Script
    {
        public GameObject Environment;
        public GameObject Background;

        private bool _pressed;
        private Vector3 _mouse;
        private Vector3 _map;
        private Vector3 _background;

        public void Update()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0.001)
            {
                const float speed = 2.5f;
                var scale1 = Mathf.Max(0.5f, Environment.transform.localScale.x + scroll * speed);
                var scale2 = Mathf.Max(0.875f, Background.transform.localScale.x + scroll * speed / 4);

                scale1 = Mathf.Min(1.5f, scale1);
                scale2 = Mathf.Min(1.125f, scale2);

                var animationCurve = Environment.GetComponent<TweenScale>().animationCurve;
                var pos1 = Environment.transform.localPosition;
                var pos2 = Background.transform.localPosition;

                TweenPosition.Begin(Environment, 0.25f, scale1 / Environment.transform.localScale.x * pos1).animationCurve = animationCurve;
                TweenPosition.Begin(Background, 0.25f, scale2 / Background.transform.localScale.x * pos2).animationCurve = animationCurve;
                TweenScale.Begin(Environment, 0.25f, scale1 * Vector3.one).animationCurve = animationCurve;
                TweenScale.Begin(Background, 0.25f, scale2 * Vector3.one).animationCurve = animationCurve;
            }

            if (_pressed && Input.GetKey(KeyCode.Mouse0))
            {
                var delta = 500 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _mouse);

                Environment.transform.localPosition = _map + delta;
                Background.transform.localPosition = _background + 0.5f * delta;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero).collider != null)
                {
                    return;
                }

                _pressed = true;
                _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _map = Environment.transform.localPosition;
                _background = Background.transform.localPosition;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _pressed = false;
            }
        }

        public void Set(Vector2 position, float scale)
        {
            Environment.transform.localPosition = new Vector3(position.x, position.y, -1);
            Environment.transform.localScale = scale * Vector2.one;
        }
    }
}
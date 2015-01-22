using System;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class TweenMap : Script
    {
        public GameObject Environment;
        public GameObject Background;

        private const float Duration = 0.25f;
        private const float MovePerspective = 0.5f;
        private const float ScalePerspective = 0.25f;

        private bool _pressed;
        private Vector3 _mouse;
        private Vector3 _map;
        private Vector3 _background;
        private float _scaleGalaxy = 1;
        private float _scaleSystem = 1;

        public void Update()
        {
            if (!(UIScreen.Current is Galaxy || UIScreen.Current is Systema))
            {
                return;
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0.001)
            {
                const float speed = 2.5f;
                var scale1 = Environment.transform.localScale.x + scroll * speed;
                var scale2 = Background.transform.localScale.x + ScalePerspective * scroll * speed;

                scale1 = Mathf.Min(1.500f, Mathf.Max(0.500f, scale1));
                scale2 = Mathf.Min(1.125f, Mathf.Max(0.875f, scale2));

                var animationCurve = Environment.GetComponent<TweenScale>().animationCurve;

                TweenPosition.Begin(Environment, Duration, scale1 / Environment.transform.localScale.x * Environment.transform.localPosition).animationCurve = animationCurve;
                TweenPosition.Begin(Background, Duration, scale2 / Background.transform.localScale.x * Background.transform.localPosition).animationCurve = animationCurve;
                
                TweenScale.Begin(Environment, Duration, scale1 * Vector3.one).animationCurve = animationCurve;
                TweenScale.Begin(Background, Duration, scale2 * Vector3.one).animationCurve = animationCurve;

                if (UIScreen.Current is Galaxy)
                {
                    _scaleGalaxy = scale1;
                }
                else
                {
                    _scaleSystem = scale1;
                }
            }

            if (_pressed && Input.GetKey(KeyCode.Mouse0))
            {
                var delta = 500 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _mouse);

                Environment.transform.localPosition = _map + delta;
                Background.transform.localPosition = _background + MovePerspective * delta;
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

        public void Focus(Vector2 position)
        {
            var scale = UIScreen.Current is Galaxy ? _scaleGalaxy : _scaleSystem;
            var pos = scale * new Vector3(position.x, position.y, -1);
            var delta = pos - Environment.transform.localPosition;

            TweenPosition.Begin(Background, Duration, Background.transform.localPosition + MovePerspective * delta);
            TweenPosition.Begin(Environment, Duration, pos);
        }
    }
}
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
        private Vector3 _mouse, _map, _background;
        
        public void Update()
        {
            if (!(UIScreen.Current is Galaxy || UIScreen.Current is Systema))
            {
                return;
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0)
            {
                const float speed = 2.5f;
                var scaleEnv = Environment.transform.localScale.x + scroll * speed;
                var scaleBack = Background.transform.localScale.x + ScalePerspective * scroll * speed;

                scaleEnv = Mathf.Min(1.500f, Mathf.Max(0.500f, scaleEnv));
                scaleBack = Mathf.Min(1.125f, Mathf.Max(0.875f, scaleBack));

                var animationCurve = Environment.GetComponent<TweenScale>().animationCurve;

                TweenPosition.Begin(Environment, Duration, scaleEnv / Environment.transform.localScale.x * Environment.transform.localPosition).animationCurve = animationCurve;
                TweenPosition.Begin(Background, Duration, scaleBack / Background.transform.localScale.x * Background.transform.localPosition).animationCurve = animationCurve;
                
                TweenScale.Begin(Environment, Duration, scaleEnv * Vector3.one).animationCurve = animationCurve;
                TweenScale.Begin(Background, Duration, scaleBack * Vector3.one).animationCurve = animationCurve;

                Scale = scaleEnv;
            }

            if (_pressed && Input.GetKey(KeyCode.Mouse0))
            {
                var delta = 500 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _mouse);

                if (UIScreen.Current is Galaxy)
                {
                    delta = GalaxyBounds(delta);
                }
                else
                {
                    delta = SystemBounds(delta);
                }

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

        public void Set(Vector2 position)
        {
            var deltaPosition = (Vector3) (position * Scale - (Vector2) Environment.transform.localPosition);
            var deltaScale = (Vector3) (Scale * Vector2.one - (Vector2) Environment.transform.localScale);

            Environment.transform.localPosition += deltaPosition;
            Background.transform.localPosition += MovePerspective * deltaPosition;

            Environment.transform.localScale += deltaScale;
            Background.transform.localScale += ScalePerspective * deltaScale;
        }

        public void Focus(Vector2 position)
        {
            var deltaPosition = (Vector3) (position * Scale - (Vector2) Environment.transform.localPosition);

            TweenPosition.Begin(Environment, Duration, Environment.transform.localPosition + deltaPosition);
            TweenPosition.Begin(Background, Duration, Background.transform.localPosition + MovePerspective * deltaPosition);
        }

        private float _scaleGalaxy = 0.5f, _scaleSystem = 1;

        private float Scale
        {
            get { return UIScreen.Current is Galaxy ? _scaleGalaxy : _scaleSystem; }
            set
            {
                if (UIScreen.Current is Galaxy)
                {
                    _scaleGalaxy = value;
                }
                else
                {
                    _scaleSystem = value;
                }
            }
        }

        private Vector3 GalaxyBounds(Vector3 delta)
        {
            var xBounds = new Vector2(-2000, 2000) * Scale;
            var yBounds = new Vector2(-1500, 1500) * Scale;

            return FixDelta(delta, xBounds, yBounds);
        }

        private Vector3 SystemBounds(Vector3 delta)
        {
            var xBounds = new Vector2(-500, 500) * Scale;
            var yBounds = new Vector2(-500, 500) * Scale;

            return FixDelta(delta, xBounds, yBounds);
        }

        private Vector3 FixDelta(Vector3 delta, Vector2 xBounds, Vector2 yBounds)
        {
            if ((_map.x + delta.x) > xBounds.y)
            {
                delta.x = xBounds.y - _map.x;
            }
            else if ((_map.x + delta.x) < xBounds.x)
            {
                delta.x = xBounds.x - _map.x;
            }

            if ((_map.y + delta.y) > yBounds.y)
            {
                delta.y = yBounds.y - _map.y;
            }
            else if ((_map.y + delta.y) < yBounds.x)
            {
                delta.y = yBounds.x - _map.y;
            }

            return delta;
        }
    }
}
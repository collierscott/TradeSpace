using System;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class TweenMap : Script
    {
        public GameObject Map;

        private bool _pressed;
        private Vector3 _position;
        private Vector3 _p;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Profile.Instance.Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Profile.Load();
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0.001)
            {
                const float min = 0.5f;
                const int max = 2;
                const float speed = 10;
                var scale = Mathf.Max(min, Map.transform.localScale.x + scroll * speed);

                scale = Mathf.Min(max, scale);

                var animationCurve = Map.GetComponent<TweenScale>().animationCurve;

                TweenScale.Begin(Map, 0.25f, scale * Vector3.one).animationCurve = animationCurve;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _pressed = true;
                _position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _p = Map.transform.localPosition;
                //TweenPosition.Begin(Map, 0.25f, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _pressed = false;
            }

            if (_pressed && Input.GetKey(KeyCode.Mouse0))
            {
                Map.transform.localPosition = _p + 500 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _position);
                //_position = Input.mousePosition;
            }
        }

        public void Set(Vector2 position, float scale)
        {
            Map.transform.localPosition = new Vector3(position.x, position.y, -1);
            Map.transform.localScale = scale * Vector2.one;
        }
    }
}
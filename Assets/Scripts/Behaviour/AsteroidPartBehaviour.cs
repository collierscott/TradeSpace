using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class AsteroidPartBehaviour : Script
    {
        public UISprite Image;
        public UISprite Structure;
        public GameButton Button;
        public GameObject Item;
        public event Action<AsteroidPart, int> Crush = (p, i) => { };
        public event Action<AsteroidPartBehaviour> Click = p => { };
        public bool AllowHit = true;

        public enum HitMode
        {
            Click,
            Over
        }

        private const float ClickMult = 0.1f;
        private AsteroidPart _asteroidPart;
        private DrillParams _drillParams;
        private int _max = 1000;
        private int _clicks;
        private bool _pressed;
        private float _maxTimeSec = 1000;
        private float _curTimeSec;
        private DateTime _time = DateTime.MinValue;
        private PlayerShip _playerShip;
        private int _index;

        private HitMode _hitMode = HitMode.Click;

        public void Awake()
        {
            UpdateStructure();
            Button.Up += Dig;
        }

        private bool CheckContitions()
        {
            string error = null;

            if (_asteroidPart == null)
            {
                error = "You haven't equipment to dril this asteroid";
            }
            else if (_asteroidPart.Class > _drillParams.Class)
            {
                error = "You haven't actual equipment to dril this asteroid class '" + _asteroidPart.Class + "'";
            }
            else
            {
                var cargoStatus = _playerShip.GetCargoStatus(new MemoGoods { Id = _asteroidPart.Mineral, Quantity = _asteroidPart.Quantity });

                switch (cargoStatus)
                {
                    case CargoStatus.NoMass:
                        error = "You haven't available mass to dril this asteroid. Quantity:" + _asteroidPart.Quantity;
                        break;
                    case CargoStatus.NoVolume:
                        error = "You haven't available volume to dril this asteroid. Quantity:" + _asteroidPart.Quantity;
                        break;
                }                
            }

            if (error != null)
            {
                Find<ActionManager>().ShowInfo("Warning", error);
            }

            return error == null;
        }

        private void Dig()
        {
            Debug.Log("_asteroidPart.Quantity = " + _asteroidPart.Quantity);

            if (!CheckContitions() || _hitMode == HitMode.Over)
            {
                return;
            }

            Click(this);

            if (_clicks == _max) return;

            _clicks++;

            UpdateStructure();

            if (_clicks == _max)
            {
                Crush(_asteroidPart, _index);
                Item.SetActive(false);
            }
        }

        private void UpdateStructure()
        {
            var value = _hitMode == HitMode.Click ? 1 - 1.0f * _clicks / _max : 1 - 1.0f * (_curTimeSec>=0? _curTimeSec:0) / _maxTimeSec;
            
            Structure.fillAmount = value;

            if (value < 0.7f)
            {
                Structure.color = new Color(0.89f, 0.71f, 0.015f);
            }

            if (value < 0.3f)
            {
                Structure.color = new Color(0.79f, 0.043f, 0.043f);
            }
        }

        public void Update()
        {
            if (!_pressed)
            {
                _pressed = Input.GetMouseButtonDown(0);
            }
            else
            {
                _pressed = !Input.GetMouseButtonUp(0);
            }

            if (_hitMode == HitMode.Over && AllowHit && _pressed)
            {
                Debug.Log("Pressed");

                var start = _time;
                var end = DateTime.UtcNow;
                var calcTime = false;

                if (Button.collider2D.bounds.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Debug.Log("Over ...");
                    if (!CheckContitions()) return;

                    if (_time != DateTime.MinValue)
                    {
                        calcTime = true;
                    }
                    _time = end;
                }
                else
                {
                    if (_time != DateTime.MinValue)
                    {
                        calcTime = true;
                        _time = DateTime.MinValue;
                    }
                }

                if (calcTime)
                {
                    TimeSpan delta = end.Subtract(start);
                    float seconds = 1f*delta.Ticks/TimeSpan.TicksPerSecond;
                    _curTimeSec += seconds;

                    UpdateStructure();
                }

                if (_curTimeSec >= _maxTimeSec)
                {
                    Crush(_asteroidPart, _index);
                    Item.SetActive(false);
                }
            }
            else
            {
                _time = DateTime.MinValue;
            }
        }

        public void SetAsteroidPart(AsteroidPart part, DrillParams drillParams, int index)
        {
            _index = index;
            _playerShip = new PlayerShip(Profile.Instance.Ship);
            _drillParams = drillParams;
            _clicks = 0;
            _curTimeSec = 0;
            _asteroidPart = part;
            CalcData();
        }

        private void CalcData()
        {
            _hitMode = _drillParams.Type == DrillType.Impulse ? HitMode.Click : HitMode.Over;

            if (_hitMode == HitMode.Click)
            {
                _max = (int)(ClickMult * _asteroidPart.Structure / _drillParams.Power);
                Debug.Log(string.Format("Asteroid part clicks:{0}, size:{1}", _max, _asteroidPart.Size));
            }
            else
            {
                _maxTimeSec = ClickMult * _asteroidPart.Structure / _drillParams.Power;
                Debug.Log(string.Format("Asteroid part time:{0}, size:{1}", _maxTimeSec, _asteroidPart.Size));
            }
        }
    }
}
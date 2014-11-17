using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Engine;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class AsteroidPartBehaviour : Script
    {
        /// <summary>
        /// Режим работы с жизнью астеройда
        /// </summary>
        public enum HitMode
        {
            Click,
            Over
        }

        /// <summary>
        /// Множитель кликов на часть астеройда
        /// </summary>
        private const float ClickMult = 0.1f;

        public UISprite Image;
        public UISprite Health;
        public UISprite HealthBG;

        public GameButton Button;

        public GameObject Transform;
        public GameObject Item;
        /// <summary>
        /// Событие при разбиении части астеройда
        /// </summary>
        public event Action<AsteroidPart, int> Crush = (p, i) => { };
        public event Action<AsteroidPartBehaviour> Click = (p) => { };

        public bool AllowHit = true;

        private AsteroidPart _asteroidPart;
        private DrillParams _drillParams;
        
        private int _maxClicks = 1000;
        private int _curClicks = 0;
        private bool _isMouseDown = false;

        private float _maxTimeSec = 1000;
        private float _curTimeSec = 0;
        private DateTime _curTime = DateTime.MinValue;

        private PlayerShip _playerShip = null;
        private int _index = 0;

        private HitMode _hitMode = HitMode.Click;

        public void Awake()
        {
            this.HealthIsVisible = false;
            Button.Up += Button_Up;
        }

        public bool HealthIsVisible
        {
            get { return Health.gameObject.activeSelf; }
            set
            {
                HealthBG.gameObject.SetActive(value);
                Health.gameObject.SetActive(value);
            }
        }

        private bool IsValidMassAndVolume()
        {
            var ps = new PlayerShip(Profile.Instance.Ship);

            long avaibleMass = ps.Mass - ps.GoodsMass;
            long avaibleVolume = ps.Volume - ps.GoodsVolume;

            return _asteroidPart.Volume <= avaibleVolume && _asteroidPart.Mass <= avaibleMass;
        }
        private bool IsValidparams()
        {
            string errMsg = null;

            if (_asteroidPart == null)
                errMsg = "You haven't equipment to dril this asteroid";
            else if ((int)_asteroidPart.Class > (int)_drillParams.Class)
                errMsg = "You haven't actual equipment to dril this asteroid class '" + _asteroidPart.Class + "'";
            else
            {
                var volMassCheck = _playerShip.CanAddGoods(new MemoGoods { Id = _asteroidPart.Mineral, Quantity = _asteroidPart.Quantity });

                switch(volMassCheck)
                {
                    case ShipGoodsCheck.NoMass:
                        errMsg = "You haven't available mass to dril this asteroid. Quantity:" + _asteroidPart.Quantity;
                        break;
                    case ShipGoodsCheck.NoVolume:
                        errMsg = "You haven't available volume to dril this asteroid. Quantity:" + _asteroidPart.Quantity;
                        break;
                    case ShipGoodsCheck.NoMassAndVolume:
                        errMsg = "You haven't available mass and volume to dril this asteroid. Quantity:" + _asteroidPart.Quantity;
                        break;
                }                
            }

            if (errMsg != null)
                Debug.LogWarning("DIALOG! " + errMsg);

            return errMsg==null;
        }
        void Button_Up()
        {
            Debug.Log("Asteroid part click Quantity:" + _asteroidPart.Quantity);

            if (!IsValidparams() || _hitMode== HitMode.Over)
                return;

            Click(this);

            ShowHealth();

            if (_curClicks == _maxClicks) return;

            _curClicks++;

            Update_Health();

            if (_curClicks == _maxClicks)
            {
                Crush(_asteroidPart, _index);
                Item.SetActive(false);
            }
        }

        private void ShowHealth()
        {
            if (!this.HealthIsVisible)
            {
                var parts = this.gameObject.transform.parent.gameObject.GetComponentsInChildren<AsteroidPartBehaviour>();

                Debug.Log("Parts count=" + parts.Length);

                foreach (var p in parts)
                    if (p != this)
                        p.HealthIsVisible = false;

                this.HealthIsVisible = true;
            }
        }
        private void Update_Health()
        {
            float value = _hitMode == HitMode.Click ? 1 - 1.0f * _curClicks / _maxClicks : 1 - 1.0f * (_curTimeSec>=0? _curTimeSec:0) / _maxTimeSec;
            Health.fillAmount = value;

            if (value < 0.7f)
                Health.color = new Color(0.89f, 0.71f, 0.015f);
            if (value < 0.3f)
                Health.color = new Color(0.79f, 0.043f, 0.043f);
        }

        public void Update()
        {
            if (!_isMouseDown)
                _isMouseDown = Input.GetMouseButtonDown(0);
            else
                _isMouseDown = !Input.GetMouseButtonUp(0);

            if (_hitMode == HitMode.Over && AllowHit && _isMouseDown)
            {
                Debug.Log("Is mouse down");
                DateTime start = _curTime;
                DateTime end = DateTime.UtcNow;
                bool calcTime = false;

                if(Button.collider2D.bounds.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Debug.Log("Over ...");
                    if (!IsValidparams()) return;

                    if (_curTime != DateTime.MinValue)
                    {
                        calcTime = true;                        
                    }
                    _curTime = end;
                }
                else
                {
                    if (_curTime != DateTime.MinValue)
                    {
                        calcTime = true;
                        _curTime = DateTime.MinValue;
                    }
                }

                if(calcTime)
                {
                    ShowHealth();

                    TimeSpan delta = end.Subtract(start);
                    float seconds = 1f* delta.Ticks / TimeSpan.TicksPerSecond;
                    _curTimeSec += seconds;

                    Update_Health();
                }

                if (_curTimeSec >= _maxTimeSec)
                {
                    Crush(_asteroidPart, _index);
                    Item.SetActive(false);
                }
            }
        }

        public void SetAsteroidPart(AsteroidPart part, DrillParams drillParams, int index)
        {
            _index = index;
            _playerShip = new PlayerShip(Profile.Instance.Ship);
            _drillParams = drillParams;
            _curClicks = 0;
            _curTimeSec = 0;

            _asteroidPart = part;

            CalcData();
        }

        private void CalcData()
        {
            _hitMode = _drillParams.Type == DrillType.Impulse ? HitMode.Click : HitMode.Over;

            if (_hitMode == HitMode.Click)
            {
                _maxClicks = (int)(ClickMult * _asteroidPart.Structure / _drillParams.Power);
                Debug.Log(string.Format("Asteroid part clicks:{0}, size:{1}", _maxClicks, _asteroidPart.Size));
            }
            else
            {
                _maxTimeSec = ClickMult * _asteroidPart.Structure / _drillParams.Power;
                Debug.Log(string.Format("Asteroid part time:{0}, size:{1}", _maxTimeSec, _asteroidPart.Size));
            }
        }
    }
}
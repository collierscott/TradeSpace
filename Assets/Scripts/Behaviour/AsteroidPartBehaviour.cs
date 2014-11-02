using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class AsteroidPartBehaviour : Script
    {
        public UISprite Image;
        public UISprite Health;
        public UISprite HealthBG;
        
        public GameButton Button;

        public GameObject Transform;
        public GameObject Item;
        /// <summary>
        /// Событие при разбиении части астеройда
        /// </summary>
        public event Action<AsteroidPart> Crush = (p) => { };
        public event Action<AsteroidPartBehaviour> Click = (p) => { };

        private AsteroidPart _asteroidPart;
        private int _maxClicks = 1000;
        private int _curClicks = 0;
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
        void Button_Up()
        {
            Debug.Log("Asteroid part click Quantity:"+_asteroidPart.Quantity);
            if (_asteroidPart==null)
            {
                Debug.LogWarning("You haven't equipment to dril this asteroid");
                return;
            }

            if (!IsValidMassAndVolume())
            {
                Debug.LogWarning("You haven't available mass or volume to dril this asteroid. Quantity:"+_asteroidPart.Quantity);
                return;
            }

            Click(this);

            if (!this.HealthIsVisible)
            {
                var parts = this.gameObject.transform.parent.gameObject.GetComponentsInChildren<AsteroidPartBehaviour>();

                Debug.Log("Parts count=" + parts.Length);

                foreach (var p in parts)
                    if (p != this)
                        p.HealthIsVisible = false;

                this.HealthIsVisible = true;
            }

            if (_curClicks == _maxClicks) return;

            _curClicks++;

            Update_Health();

            if (_curClicks == _maxClicks)
            {
                Crush(_asteroidPart);
                Item.SetActive(false);
            }
        }

        private void Update_Health()
        {
            float value = 1- 1.0f * _curClicks / _maxClicks;
            Health.fillAmount = value;

            if (value < 0.7f)
                Health.color = new Color(0.89f,0.71f,0.015f);
            if (value < 0.3f)
                Health.color = new Color(0.79f, 0.043f, 0.043f);
        }

        public void Update()
        {
            //Image.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }

        public void SetAsteroidPart(AsteroidPart part, int clicks)
        {
            _maxClicks = clicks;
            _curClicks = 0;
            _asteroidPart = part;
        }
    }
}
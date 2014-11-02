using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Behaviour;

using UnityEngine;

namespace Assets.Scripts.Views
{
    public class AsteroidView : ViewBase, IScreenView
    {
        private class AstPart
        {
            //public GameObject GameObject;
            public AsteroidPartBehaviour Apb;
            public long RotationSpeed;
            //public UISprite Sprite;
            //public UIProgressBar Transform;
        }
        /// <summary>
        /// Множитель кликов на часть астеройда
        /// </summary>
        private const float ClickMult = 0.1f;

        private DateTime _lastLaser = DateTime.MinValue;
        private int _laserLengthMS = 100;

        private int _drillPower = 0;

        private Asteroid _asteroid = null;

        private PlayerShip _ship = null;

        private float _prevAngel = 0;

        public UISprite Laser;

        public UIProgressBar Bar;
        public UILabel BarCount;

        public CargoView CargoView;
        public UISprite Background;

        private List<AstPart> _astParts = new List<AstPart>();

        float sizeMult = 20f;
        float minSize = 50f;
        float maxSize = 200f;

        private int GetDrillPower()
        {
            int ret = 0;

            foreach (var m in Profile.Instance.Ship.InstalledEquipment)
                switch(m.Id)
                {
                    case Enums.EquipmentId.ImpulseDrill100: ret = 1; break;
                    case Enums.EquipmentId.LaserDrill100: ret = 5; break;
                }

            return ret;
        }
        protected override void Initialize()
        {
            Debug.Log(Profile.Instance.SelectedShip + " " + Profile.Instance.Ship.InstalledEquipment.Count);

            Laser.gameObject.SetActive(false);
            _drillPower = GetDrillPower();

            if(_drillPower==0)
            {
                //TODO - Dialog
                Debug.Log("msg: You don't have drill equipment!");
                return;
            }

            Debug.Log("DrillPower=" + _drillPower);

            _ship = new PlayerShip(Profile.Instance.Ship);

            

            _asteroid = SelectManager.Location as Asteroid;
            //AsteroidSprite.spriteName = _asteroid.Image;
            // _usedQuantity = _asteroid.Quantity - Profile.Instance.Asteroids.FindQuantity(_asteroid);

            _astParts.Clear();

            System.Random rnd = new System.Random();

            float prevRad = 50f;

            int refRad = 256;

            for (int p = 0; p < _asteroid.Parts.Count; p++)
            {
                AsteroidPart ap = _asteroid.Parts[p];

                var obj = PrefabsHelper.InstantiateAsteroidPart(Panel);
                obj.transform.localPosition = new Vector3(0, 0);

                AsteroidPartBehaviour apb = obj.GetComponent<AsteroidPartBehaviour>();

                apb.SetAsteroidPart(ap, CalcClicks(ap));
                //apb.Click += apb_Click;
                apb.Crush += apb_Crush;
                
                AstPart part = new AstPart
                {
                    Apb = apb,
                    //GameObject = obj,
                    RotationSpeed = 20 + rnd.Next(50),
                };
                _astParts.Add(part);


                //GameButton btn = obj.GetComponentInChildren<GameButton>();

                //btn.Up += () => AsteroidPart_Click(part);

                //part.Sprite = obj.GetComponentInChildren<UISprite>();
                //part.Sprite.spriteName = _asteroid.Image;

                //part.Transform = obj.GetComponentInChildren<UIProgressBar>();

                float size = AstSizeToScreen(ap.Size);
                float scale = size / refRad;

                float curRad = (float)Math.Sqrt(size * size / 2);

                //obj.transform.Rotate(0, 0, -1.5f + 3.14f * rnd.Next(100) / 100);
                //obj.transform.Rotate(0, 0, -1.5f + 3.14f * ap.Speed*5 / 100);

                part.RotationSpeed = ap.Speed;


                apb.Transform.transform.localScale = new Vector3(scale, scale);
                apb.Transform.transform.localPosition = new Vector3(prevRad + curRad, prevRad + curRad);

                Debug.Log("asteroid:add part scale=" + scale + ", prevRad=" + prevRad + ", curRad=" + curRad+", quantity="+ap.Quantity);

                prevRad += curRad;
                //Debug.Log("asteroid:add part " + prevWidth);                
            }
            
            CargoView.Open();
        }

        private int CalcClicks(AsteroidPart part)
        {
            int res = (int)(ClickMult * part.Structure / _drillPower);

            Debug.Log(string.Format("Asteroid part clicks:{0}, size:{1}", res, part.Size));

            return res;
        }
        public float AstSizeToScreen(long originalSize)
        {
            float size = Mathf.Pow(originalSize, 0.33f) * sizeMult;

            if (size < minSize) size = minSize;
            if (size > maxSize) size = maxSize;

            return size;
        }
        ////Эмуляция лазера, отключено
        //void apb_Click(AsteroidPartBehaviour apb)
        //{
        //    Laser.gameObject.SetActive(false);
        //    var pos = apb.Transform.transform.position;
        //    int len = (int)(Math.Sqrt(pos.x * pos.x * 1920 * 1920 / 4 + pos.y * pos.y * 1920 * 1920 / 4)) / 2;

        //    float angel = 0;
        //    if (pos.x == 0)
        //    {
        //        angel = pos.y > 0 ? 90f : -90f;
        //    }
        //    else
        //    {
        //        angel = (float)Math.Atan(pos.y / pos.x);
        //    }

        //    angel = angel * 180f / 3.14f;

        //    if (pos.x < 0) angel += 180;

        //    Laser.width = len;
        //    Laser.transform.Rotate(0, 0, -_prevAngel);
        //    Laser.transform.Rotate(0, 0, angel);

        //    _prevAngel = angel;

        //    Laser.gameObject.SetActive(true);
        //    _lastLaser = DateTime.UtcNow;

        //    Debug.Log(len + " " + angel);
        //    //var parent = apb.Transform.transform.parent.gameObject.
        //}

        void apb_Crush(AsteroidPart obj)
        {
            _ship.AddGoods(new MemoGoods { Id = obj.Mineral, Quantity = obj.Quantity.Encrypt() });
            //throw new NotImplementedException();
            CargoView.Refresh();
        }

        private void UpdateBar()
        {
            //Bar.value = 1.0f * (_asteroid.Quantity - _usedQuantity) / _asteroid.Quantity;
            //BarCount.text = (_asteroid.Quantity - _usedQuantity).ToString() + "/" + _asteroid.Quantity;
        }


        private void SaveData()
        {
            //if (_asteroid.Quantity - _usedQuantity == 0) return;

            //long userPie = _asteroid.Quantity > _userPickSpeed ? _userPickSpeed : _asteroid.Quantity;

            //long freeVolume = _ship.Volume - _ship.GoodsVolume - userPie * _asteroid.Ore.Volume;
            //long freeMass = _ship.Mass - _ship.GoodsMass - userPie * _asteroid.Ore.Mass;

            ////Если нет места в трюме
            //if (freeMass < 0 || freeVolume < 0)
            //    return;

            //_usedQuantity += userPie;

            //var ship = new PlayerShip(Profile.Instance.Ship);
            //ship.AppendGoods(new MemoGoods { Id = _asteroid.Ore.Id, Quantity = userPie.Encrypt() });
            //Profile.Instance.Asteroids.Update(_asteroid.Name, _asteroid.Quantity - _usedQuantity);

        }

        public void Update()
        {
            foreach (var p in _astParts)
            {
                p.Apb.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
                //p.Sprite.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
            }

            if (Laser.gameObject.activeSelf && _lastLaser.AddMilliseconds(_laserLengthMS) < DateTime.UtcNow)
                Laser.gameObject.SetActive(false);
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }
    }
}
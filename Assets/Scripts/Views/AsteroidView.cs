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
        
        private Asteroid _asteroid = null;

        private long _usedQuantity = 0;
        private int _userPickSpeed = 1;
        private PlayerShip _ship = null;

        public UISprite Laser;

        public UIProgressBar Bar;
        public UILabel BarCount;
        
        public CargoView CargoView;
        public UISprite Background;

        private List<AstPart> _astParts = new List<AstPart>();

        protected override void Initialize()
        {
            _ship = new PlayerShip(Profile.Instance.Ship);

            _asteroid = SelectManager.Location as Asteroid;
            //AsteroidSprite.spriteName = _asteroid.Image;
            // _usedQuantity = _asteroid.Quantity - Profile.Instance.Asteroids.FindQuantity(_asteroid);

            _astParts.Clear();

            System.Random rnd = new System.Random();

            float prevRad = 100f;
            float sizeMult = 20f;

            int refRad = 256;

            float minRad = 50f;
            float maxRad = 200f;

            for (int p = 0; p < _asteroid.Parts.Count; p++)
            {
                AsteroidPart ap = _asteroid.Parts[p];

                var obj = PrefabsHelper.InstantiateAsteroidPart(Panel);
                obj.transform.localPosition = new Vector3(0,0);

                AsteroidPartBehaviour apb = obj.GetComponent<AsteroidPartBehaviour>();
                apb.SetAsteroidPart(ap, 30);
                apb.Click += apb_Click;
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
                
                float size = ap.Size * sizeMult;

                if (size < minRad) size = minRad;
                if (size > maxRad) size = maxRad;

                float scale = size/refRad;

                float curRad = (float)Math.Sqrt(size * size / 2);

                obj.transform.Rotate(0, 0, -1.5f + 3.14f * rnd.Next(100) / 100);
                //obj.transform.Rotate(0, 0, -1.5f + 3.14f * ap.Speed*5 / 100);

                part.RotationSpeed = ap.Speed;


                apb.Transform.transform.localScale = new Vector3(scale, scale);
                apb.Transform.transform.localPosition = new Vector3(prevRad, prevRad);

                Debug.Log("asteroid:add part scale=" + scale + ", prevRad=" + prevRad + ", curRad=" + curRad);

                prevRad += curRad;
                //Debug.Log("asteroid:add part " + prevWidth);                
            }



            //UpdateBar();

        

            //CargoView.Open();
        }
        void apb_Click(AsteroidPartBehaviour apb)
        {
            var pos = apb.Transform.transform.position;
            int len = (int)(Math.Sqrt(pos.x * pos.x*1920*1920/4 + pos.y * pos.y*1080*1080/4))/2;

            float angel = 0;
            if(pos.x==0)
            {
                angel = pos.y > 0 ? 1.52f : -1.52f;
            }
            else
            {
                angel = (float)Math.Atan(pos.y / pos.x);
            }

            angel = angel * 180f / 3.14f;

            Laser.width = len;
            Laser.transform.Rotate(0, 0, angel);
            Debug.Log(len + " " + angel);
            //var parent = apb.Transform.transform.parent.gameObject.
        }

        void apb_Crush(AsteroidPart obj)
        {
            //throw new NotImplementedException();
        }

        private void AsteroidPart_Click( AstPart part)
        {
            //var pos = GetComponent<UICamera>().lastHit.point;

            //Debug.Log("asteroid:click " + part.Apb.transform.localPosition + " " + part.Sprite.transform.localPosition + " " + part.Transform.transform.localPosition);  
        }
        private void UpdateBar()
        {
            return;
            CargoView.Refresh();
            //Bar.value = 1.0f * (_asteroid.Quantity - _usedQuantity) / _asteroid.Quantity;
            //BarCount.text = (_asteroid.Quantity - _usedQuantity).ToString() + "/" + _asteroid.Quantity;
        }


        void PickButton_Up()
        {
            SaveData();
            UpdateBar();
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
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }
    }
}
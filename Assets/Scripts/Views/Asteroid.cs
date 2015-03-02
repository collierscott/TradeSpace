using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Behaviour;

using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Asteroid : UIScreen
    {
        public UISprite HeatingBar;
        public GameObject HeatingNode;
        public UISprite Background;

        private class AstPart
        {
            public LodeButton Apb;
            public long RotationSpeed;
        }

        private bool _isMouseDown;
        private float _curHeatingValue;

        private DateTime _curTime = DateTime.UtcNow;

        private DrillParams _drillParams;
        
        private Data.Asteroid _asteroid;

        private PlayerShip _ship;

        

        private List<AstPart> _astParts = new List<AstPart>();

        float sizeMult = 30f;
        float minSize = 50f;
        float maxSize = 200f;

        private DrillParams GetDrillParams()
        {
            DrillParams ret = null;

            foreach (var m in Profile.Instance.Ship.InstalledEquipment)
                switch(m.Id)
                {
                    case Enums.EquipmentId.ImpulseDrill100: 
                    case Enums.EquipmentId.LaserDrill100: 
                        ret = Environment.Env.GetDrillParams(m.Id); break;
                }

            return ret;
        }

        protected override void Initialize()
        {
            _drillParams = GetDrillParams();

            if (_drillParams == null)
            {
                Find<ActionManager>().ShowInfo("Warning", "You don't have drill equipment!");
                return;
            }

            Debug.Log("DrillParams " + _drillParams);

            _ship = new PlayerShip(Profile.Instance.Ship);

            

            _asteroid = (Data.Asteroid) SelectManager.Location;
            foreach (var p in _astParts)
                DestroyImmediate(p.Apb.gameObject);

            Debug.Log("_astParts.Count:" + _astParts.Count);
            _astParts.Clear();

            var astMemo = Profile.Instance.Asteroids.ContainsKey(_asteroid.Name) ? Profile.Instance.Asteroids[_asteroid.Name] : null;

            Debug.Log("AstMemo: " + (astMemo != null ? astMemo.ToString() : "null"));

            global::System.Random rnd = new global::System.Random();

            float prevRad = 50f;

            int refRad = 256;

            var ship = new PlayerShip(Profile.Instance.Ship);
            

            for (int p = 0; p < _asteroid.Parts.Count; p++)
            {
                Lode lode = _asteroid.Parts[p];

                var obj = PrefabsHelper.InstantiateLode(Panel);
                obj.transform.localPosition = new Vector3(0, 0);

                LodeButton apb = obj.GetComponent<LodeButton>();

               
                AstPart part = new AstPart
                {
                    Apb = apb,
                    //GameObject = obj,
                    RotationSpeed = 20 + rnd.Next(50),
                };
                _astParts.Add(part);


                float size = AstSizeToScreen(lode.Size);
                float scale = size / refRad;

                float curRad = (float)Math.Sqrt(size * size / 2);

                //obj.transform.Rotate(0, 0, -1.5f + 3.14f * rnd.Next(100) / 100);
                //obj.transform.Rotate(0, 0, -1.5f + 3.14f * ap.Speed*5 / 100);

                part.RotationSpeed = lode.Speed;


                transform.localScale = new Vector3(scale, scale);
                transform.localPosition = new Vector3(prevRad + curRad, prevRad + curRad);

                Debug.Log("asteroid:add part scale=" + scale + ", prevRad=" + prevRad + ", curRad=" + curRad+", quantity="+lode.Quantity);

                prevRad += curRad;

                if (astMemo == null || !astMemo.EmptyParts.Contains(p))
                    apb.Initialize(lode, ship);
                else
                    obj.SetActive(false);
            }

            Open<Cargo>();
        }

        
        public float AstSizeToScreen(long originalSize)
        {
            float size = Mathf.Pow(originalSize, 0.33f) * sizeMult;

            if (size < minSize) size = minSize;
            if (size > maxSize) size = maxSize;

            return size;
        }

        public void apb_Crush(Lode mineral, int index)
        {
            _ship.AddGoods(new MemoGoods { Id = mineral.Mineral, Quantity = mineral.Quantity });

            if(mineral.HasCore)
            {
                var core = new MemoGoods
                {
                    Id = Environment.Env.GetMineralCore(mineral.Mineral),
                    Quantity = 1
                };

                var checkResult = _ship.GetCargoStatus(core);

                if(checkResult == Enums.CargoStatus.Ready)
                {
                    _ship.AddGoods(core);
                    Find<ActionManager>().ShowInfo("Information", "Congratulations! You got asteroid core:" + core.Id);
                    //Debug.LogWarning("Congratulations! You got asteroid core:" + core.Id);
                }
                else if(checkResult == Enums.CargoStatus.NoMass)
                {
                    //Debug.LogWarning("You got asteroid core:" + core.Id + " but you don't have enough mass.");
                    Find<ActionManager>().ShowInfo("Warning", "You got asteroid core:" + core.Id + " but you don't have enough mass.");
                }
                else if ( checkResult == Enums.CargoStatus.NoVolume)
                {
                    //Debug.LogWarning("You got asteroid core:" + core.Id + " but you don't have enough volume.");
                    Find<ActionManager>().ShowInfo("Warning", "You got asteroid core:" + core.Id + " but you don't have enough volume.");
                }
            }

            if (Profile.Instance.Asteroids.ContainsKey(_asteroid.Name))
            {
                Profile.Instance.Asteroids[_asteroid.Name].EmptyParts.Add(index);
            }
            else
            {
                Profile.Instance.Asteroids.Add(_asteroid.Name, new MemoAsteroid
                {
                    Name = name, EmptyParts = new List<int> { index }
                });
            }

            GetComponent<Cargo>().Refresh();
        }

        public void Update()
        {
            foreach (var p in _astParts)
            {
                p.Apb.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
                //p.Sprite.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
            }

            if (_drillParams.Type == Enums.DrillType.Laser)
            {
                if (!_isMouseDown)
                    _isMouseDown = Input.GetMouseButtonDown(0);
                else
                    _isMouseDown = !Input.GetMouseButtonUp(0);

                HeatingNode.SetActive(true);

                DateTime start = _curTime;
                DateTime end = DateTime.UtcNow;

                _curTime = end;

                TimeSpan delta = end.Subtract(start);
                float seconds = 1f * delta.Ticks / TimeSpan.TicksPerSecond;

                if (_isMouseDown)
                    _curHeatingValue += seconds * _drillParams.HeatingRate;
                else
                    _curHeatingValue -= seconds * _drillParams.CoolingRate;

                if (_curHeatingValue < 0)
                    _curHeatingValue = 0;
                if (_curHeatingValue > _drillParams.HeatingTo)
                    _curHeatingValue = _drillParams.HeatingTo;

                float barValue =  _curHeatingValue / _drillParams.HeatingTo;

                //Debug.Log(string.Format("Heating curValue:{0}, max:{1}, isDown:{2}", _curHeatingValue, _drillParams.HeatingTo, _isMouseDown));

                HeatingBar.fillAmount = barValue;

                bool allowHit = _curHeatingValue != _drillParams.HeatingTo;


                if (!allowHit)
                    Find<ActionManager>().ShowInfo("Warning", "You boer temperature is to high!");
                    //Debug.LogWarning("DIALOG! You boer temperature is to high!");
            }
        }

        protected override void Cleanup()
        {
            Close<Cargo>();
        }
    }
}
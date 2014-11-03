using System;
using System.Linq;
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
            public AsteroidPartBehaviour Apb;
            public long RotationSpeed;
        }
        

        private DrillParams _drillParams = null;
        
        private Asteroid _asteroid = null;

        private PlayerShip _ship = null;

        public UIProgressBar Bar;
        public UILabel BarCount;

        public CargoView CargoView;
        public UISprite Background;

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
            Debug.Log(Profile.Instance.SelectedShip + " " + Profile.Instance.Ship.InstalledEquipment.Count);

            _drillParams = GetDrillParams();

            if (_drillParams == null)
            {
                //TODO - Dialog
                Debug.LogWarning("msg: You don't have drill equipment!");
                return;
            }

            Debug.Log("DrillParams " + _drillParams);

            _ship = new PlayerShip(Profile.Instance.Ship);

            

            _asteroid = SelectManager.Location as Asteroid;
            
            _astParts.Clear();

            MemoAsteroid astMemo = Profile.Instance.Asteroids.Where(a => a.Name == _asteroid.Name).FirstOrDefault();

            Debug.Log("AstMemo: " + (astMemo != null ? astMemo.ToString() : "null"));

            System.Random rnd = new System.Random();

            float prevRad = 50f;

            int refRad = 256;

            for (int p = 0; p < _asteroid.Parts.Count; p++)
            {
                AsteroidPart ap = _asteroid.Parts[p];

                var obj = PrefabsHelper.InstantiateAsteroidPart(Panel);
                obj.transform.localPosition = new Vector3(0, 0);

                AsteroidPartBehaviour apb = obj.GetComponent<AsteroidPartBehaviour>();

                //apb.Click += apb_Click;
                apb.Crush += apb_Crush;
                
                AstPart part = new AstPart
                {
                    Apb = apb,
                    //GameObject = obj,
                    RotationSpeed = 20 + rnd.Next(50),
                };
                _astParts.Add(part);


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

                if (astMemo == null || !astMemo.EmptyParts.Contains(p))
                    apb.SetAsteroidPart(ap, _drillParams, p);
                else
                    obj.SetActive(false);
            }
            
            CargoView.Open();
        }

        
        public float AstSizeToScreen(long originalSize)
        {
            float size = Mathf.Pow(originalSize, 0.33f) * sizeMult;

            if (size < minSize) size = minSize;
            if (size > maxSize) size = maxSize;

            return size;
        }

        void apb_Crush(AsteroidPart obj, int index)
        {
            _ship.AddGoods(new MemoGoods { Id = obj.Mineral, Quantity = obj.Quantity });

            if(obj.HasCore)
            {
                var coreGoods = Environment.Env.AsteroidCoreDatabase[obj.Mineral];
                var coreValue = new MemoGoods { Id = coreGoods, Quantity = obj.Quantity };

                var checkResult = _ship.CanAddGoods(coreValue);

                if(checkResult == Enums.ShipGoodsCheck.Success)
                {
                    _ship.AddGoods(coreValue);
                    Debug.LogWarning("Congratulations! You got asteroid core:" + coreGoods);
                }
                else if((checkResult & Enums.ShipGoodsCheck.NoMass)== Enums.ShipGoodsCheck.NoMass)
                {
                    Debug.LogWarning("You got asteroid core:" + coreGoods + " but you don't have enough mass.");
                }
                else if ((checkResult & Enums.ShipGoodsCheck.NoVolume) == Enums.ShipGoodsCheck.NoVolume)
                {
                    Debug.LogWarning("You got asteroid core:" + coreGoods + " but you don't have enough volume.");
                }
            }

            Profile.Instance.Asteroids.AddEmptyPart(_asteroid.Name, index);

            CargoView.Refresh();
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
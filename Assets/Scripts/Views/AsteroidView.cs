using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class AsteroidView : ViewBase, IScreenView
    {
        private class AstPart
        {
            public GameObject GameObject;
            public int RotationSpeed;
            public UISprite Sprite;
        }
        private bool _isInit = false;

        private Asteroid _asteroid = null;

        private long _usedQuantity = 0;
        private int _userPickSpeed = 1;
        private PlayerShip _ship = null;

        public GameButton PickButton;
        public UIProgressBar Bar;
        public UILabel BarCount;
        public UISprite AsteroidSprite;

        public CargoView CargoView;
        public UISprite Background;

        private List<AstPart> _astParts = new List<AstPart>();

        protected override void Initialize()
        {
            //Show();

            //if (!_isInit)
            //{
            //    _isInit = true;
            //    InitEvents();
            //}
            //_ship = new PlayerShip(Profile.Instance.Ship);

            //_asteroid = (Asteroid)SelectManager.Location;
            ////AsteroidSprite.spriteName = _asteroid.Image;
            //// _usedQuantity = _asteroid.Quantity - Profile.Instance.Asteroids.FindQuantity(_asteroid);

            //_astParts.Clear();

            //System.Random rnd = new System.Random();

            //float maxSize = _asteroid.Quantity.Sum();
            //long prevRad = 50;
            //float minScale = 0.25f;

            //for (int p = 0; p < _asteroid.Ore.Length; p++)
            //{
            //    var obj = PrefabsHelper.InstantiateAsteroidPart(Panel);

            //    AstPart part = new AstPart
            //    {
            //        GameObject = obj,
            //        RotationSpeed = 20 + rnd.Next(50),
            //    };
            //    _astParts.Add(part);

            //    GameButton btn = obj.GetComponentInChildren<GameButton>();
            //    part.Sprite = obj.GetComponentInChildren<UISprite>();
            //    part.Sprite.spriteName = _asteroid.Image;

            //    float scale = _asteroid.Quantity[p] / maxSize;
            //    if (scale < minScale) scale = minScale;

            //    long curRad = (long)(scale * Math.Sqrt(part.Sprite.width * part.Sprite.width / 2));

            //    obj.transform.Rotate(0, 0, -1.5f + 3.14f * rnd.Next(100)/100);
            //    part.Sprite.transform.localScale = new Vector3(scale, scale);
            //    part.Sprite.transform.localPosition = new Vector3(prevRad + curRad, prevRad + curRad);
            //    btn.SetBaseScale(part.Sprite.transform.localScale);

            //    prevRad += curRad;
            //    //Debug.Log("asteroid:add part " + prevWidth);
            //    Debug.Log("asteroid:add part " + prevRad + " " + curRad);
            //}



            //UpdateBar();

        

            //CargoView.Open();
        }
        private void UpdateBar()
        {
            return;
            CargoView.Refresh();
            //Bar.value = 1.0f * (_asteroid.Quantity - _usedQuantity) / _asteroid.Quantity;
            //BarCount.text = (_asteroid.Quantity - _usedQuantity).ToString() + "/" + _asteroid.Quantity;
        }

        private void InitEvents()
        {
            PickButton.Up += PickButton_Up;
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
                p.GameObject.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
                p.Sprite.transform.Rotate(0, 0, p.RotationSpeed * Time.deltaTime);
            }
        }

        protected override void Cleanup()
        {
            CargoView.Close();
        }
    }
}
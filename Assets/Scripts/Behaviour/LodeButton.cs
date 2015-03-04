using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class LodeButton : Script
    {
        public UILabel Info;
        public UILabel Structure;
        public Transform Scalable;
        public UISprite Progress;
        public GameButton Button;

        public static bool Overheating;

        private Lode _lode;
        private int _index;
        private Drill _drill;
        private float _structure;
        private long _rotation;

        public void Initialize(Lode lode, int index, Drill drill)
        {
            _lode = lode;
            _index = index;
            _drill = drill;
            _structure = _lode.Structure;
            _rotation = CRandom.GetRandom(100000);

            const int taskId = 525;

            Button.Down += () =>
            {
                TaskScheduler.Kill(taskId);

                if (_drill.Overheating) return;

                _drill.Active = true;

                if (_drill.Params.Type == DrillType.Impulse)
                {
                    Mine(_drill.Params.Power, _drill.Params.Heating);
                }
            };

            Button.Up += () =>
            {
                if (_drill.Params.Type == DrillType.Impulse)
                {
                    TaskScheduler.CreateTask(() => _drill.Active = false, taskId, 1);
                }
                else
                {
                    _drill.Active = false;
                }
            };

            Button.Enabled = _drill.Params.Class >= _lode.Class;

            UpdateInfo();
            UpdateStructure();
        }

        public void Update()
        {
            var angle = _lode.Speed * (Time.time + _rotation);
            var rotation = 200 * _lode.Speed * Time.time * (_rotation % 2 == 0 ? 1 : -1);

            transform.localPosition = _lode.Radius * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
            Button.transform.localRotation = Quaternion.Euler(0, 0, rotation);

            if (_drill.Params.Type == DrillType.Laser && _drill.Active && !_drill.Overheating)
            {
                Mine(_drill.Params.Power * Time.deltaTime, _drill.Params.Heating * Time.deltaTime);
            }
        }

        private void Mine(float damage, float heading)
        {
            if (_drill.Overheating)
            {
                return;
            }

            _drill.Heating += heading;

            if (_drill.Heating > 1)
            {
                _drill.Overheating = true;
            }

            _structure -= damage;

            if (_structure > 0)
            {
                UpdateStructure();
            }
            else
            {
                Extract();
            }
        }

        private void Extract()
        {
            var ship = new PlayerShip(Profile.Instance.Ship);
            var minerals = new List<GoodsId>();

            Debug.Log(string.Format("Core chance = {0} * {1} = {2}", _lode.CoreChance, _drill.Params.Efficiency, _lode.CoreChance * _drill.Params.Efficiency));

            if (MiningParams.Core.ContainsKey(_lode.Mineral) && CRandom.Chance(_lode.CoreChance * _drill.Params.Efficiency))
            {
                minerals.Add(MiningParams.Core[_lode.Mineral]);

                Debug.Log("Core extracted");
            }

            for (var i = 0; i < _lode.Size * _drill.Params.Efficiency; i++)
            {
                minerals.Add(_lode.Mineral);
            }

            Debug.Log(string.Format("Extracted: {0}", minerals.Count));

            foreach (var goods in minerals.Select(id => new MemoGoods { Id = id, Quantity = 1 }))
            {
                if (ship.GetCargoStatus(goods) == CargoStatus.Ready)
                {
                    ship.AddGoods(goods);
                }
                else
                {
                    Find<Dialog>().Open("Warning", "The cargo bay is filled");
                    break;
                }
            }

            if (Profile.Instance.Asteroids.ContainsKey(SelectManager.Location.Name))
            {
                Profile.Instance.Asteroids[SelectManager.Location.Name].Extracted.Add(_index);
            }
            else
            {
                Profile.Instance.Asteroids.Add(SelectManager.Location.Name, new MemoAsteroid { Extracted = new List<int> { _index } });
            }

            Destroy(gameObject);
        }

        private void UpdateInfo()
        {
            Scalable.localScale = _lode.Scale * Vector2.one;
            Info.SetText("{0} {1}", _lode.Mineral, _lode.Class);

            foreach (var t in new[] { Info.transform, Structure.transform })
            {
                t.localPosition = new Vector3(Progress.width / 2f * _lode.Scale + 20, t.localPosition.y);
            }

            Info.color = Structure.color = Button.Enabled ? Color.white : ColorHelper.GetColor(255, 0, 0);
        }

        private void UpdateStructure()
        {
            var fillAmount = _structure / _lode.Structure;

            Structure.SetText((long) _structure);
            Progress.fillAmount = fillAmount;

            if (!Button.Enabled)
            {
                Progress.color = ColorHelper.GetColor(120, 120, 120);
            }
            else if (fillAmount > 0.7f)
            {
                Progress.color = ColorHelper.GetColor(0, 120, 255);
            }
            else if (fillAmount > 0.3f)
            {
                Progress.color = Color.yellow;
            }
            else
            {
                Progress.color = Color.red;
            }
        }
    }
}
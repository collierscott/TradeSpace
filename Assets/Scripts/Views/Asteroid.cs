using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Asteroid : UIScreen
    {
        public Transform Space;
        public UISprite HeatingLevel;

        private Drill _drill;
        private List<LodeButton> _lodes;
 
        public void Update()
        {
            if (_drill.Heating > 0)
            {
                if (!_drill.Active)
                {
                    _drill.Heating -= _drill.Params.Cooling*(_drill.Overheating ? 0.25f : 1) * Time.deltaTime;
                }
            }
            else
            {
                _drill.Heating = 0;
                _drill.Overheating = false;
            }

            HeatingLevel.fillAmount = _drill.Heating;
            HeatingLevel.color = _drill.Overheating ? Color.red : new Color(1, 1 - _drill.Heating, 0);
        }

        protected override void Initialize()
        {
            var asteroid = (Data.Asteroid) Env.Systems[SelectManager.Location.System][SelectManager.Location.Name];
            var equipment = Profile.Instance.MemoShip.InstalledEquipment.Single(i => Env.EquipmentDatabase[i.Id].Type == EquipmentType.Drill);

            _drill = new Drill { Params = MiningParams.DrillParams[equipment.Id] };
            _lodes = new List<LodeButton>();

            if (Profile.Instance.Asteroids.ContainsKey(asteroid.Name))
            {
                asteroid.Merge(Profile.Instance.Asteroids[asteroid.Name].Extracted);
            }
            
            for (var i = 0; i < asteroid.Lodes.Count; i++)
            {
                if (asteroid.Lodes[i] != null)
                {
                    var lode = PrefabsHelper.InstantiateLode(Space).GetComponent<LodeButton>();

                    lode.Initialize(asteroid.Lodes[i], i, _drill);
                    _lodes.Add(lode);
                }
            }

            Open<Cargo>();
            GetComponent<ShipSelect>().Refresh();
        }

        protected override void Cleanup()
        {
            Close<Cargo>();
            Space.Clear();
        }
    }
}
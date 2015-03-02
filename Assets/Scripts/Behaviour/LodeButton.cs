using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class LodeButton : Script
    {
        public UISprite Structure;
        public GameButton Button;

        private PlayerShip _ship;
        private DrillParams _drill;
        private float _structure;
        private Lode _lode;

        public void Initialize(Lode lode, PlayerShip ship)
        {
            _ship = ship;
            _drill = ship.Drill;
            _lode = lode;
            _structure = _lode.Structure;

            UpdateStructure();
            Button.Up += Dig;
            Button.Enabled = Enabled;
        }

        public void Update()
        {
            var angle = _lode.Speed * Time.time;

            transform.localPosition = _lode.Radius * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        }

        private void Dig()
        {
            _structure -= _drill.Power;
            
            if (_structure > 0)
            {
                UpdateStructure();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void UpdateStructure()
        {
            var fillAmount = _structure / _lode.Structure;
            
            Structure.fillAmount = fillAmount;

            if (fillAmount > 0.7f)
            {
                Structure.color = Color.green;
            }
            else if (fillAmount > 0.3f)
            {
                Structure.color = Color.yellow;
            }
            else
            {
                Structure.color = Color.red;
            }
        }

        private bool Enabled
        {
            get
            {
                if (_lode.Class > _drill.Class)
                {
                    return false;
                }

                var cargoStatus = _ship.GetCargoStatus(new MemoGoods { Id = _lode.Mineral, Quantity = _lode.Quantity });
                    
                return cargoStatus == CargoStatus.Ready;
            }
        }
    }
}
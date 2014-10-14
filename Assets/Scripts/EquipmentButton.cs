using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class EquipmentButton : Script
    {
        public UISprite Image;
        public UILabel NameText;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public EquipmentId EquipmentId { get; private set; }

        public void Initialize(EquipmentId equipmentId, long quantity)
        {
            EquipmentId = equipmentId;
            Image.spriteName = equipmentId.ToString();
            NameText.SetText(equipmentId.ToString());
            QuantityText.SetText(string.Format("x{0}", quantity));
            Button.Selected += () => FindObjectOfType<HangarView>().SelectEquipmentToInstall(equipmentId);
            NameText.transform.localPosition -= new Vector3(0, 15);
        }

        public void Initialize(EquipmentId equipmentId, long quantity, long price)
        {
            Initialize(equipmentId, quantity);
            PriceText.SetText(price);
            NameText.transform.localPosition += new Vector3(0, 15);
        }
    }
}
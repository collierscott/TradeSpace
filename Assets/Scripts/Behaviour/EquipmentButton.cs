using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;

namespace Assets.Scripts.Behaviour
{
    public class EquipmentButton : Script
    {
        public UISprite Image;
        //public UILabel NameText;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public EquipmentId EquipmentId { get; private set; }

        public void Initialize(EquipmentId equipmentId, long quantity)
        {
            EquipmentId = equipmentId;
            Image.spriteName = equipmentId.ToString();
            //NameText.SetText(equipmentId.ToString());
            QuantityText.SetText(string.Format("x{0}", quantity));
            Button.Selected += () => FindObjectOfType<HangarView>().SelectEquipmentToInstall(equipmentId);
        }

        public void Initialize(EquipmentId equipmentId, long quantity, long price)
        {
            Initialize(equipmentId, quantity);
            PriceText.SetText(price);
        }
    }
}
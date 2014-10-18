using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;

namespace Assets.Scripts.Behaviour
{
    public class InstalledEquipmentButton : Script
    {
        public UISprite Image;
        //public UILabel NameText;
        public SelectButton Button;

        public EquipmentId EquipmentId { get; private set; }
        public long Index { get; private set; }

        public void Initialize(EquipmentId equipmentId, long index)
        {
            EquipmentId = equipmentId;
            Index = index;
            Image.spriteName = equipmentId.ToString();
            //NameText.SetText(equipmentId.ToString());
            Button.Selected += () => FindObjectOfType<HangarView>().SelectEquipmentToRemove(equipmentId, index);
        }
    }
}
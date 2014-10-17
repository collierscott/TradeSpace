using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;

namespace Assets.Scripts.Behaviour
{
    public class GoodsButton : Script
    {
        public UISprite Image;
        public UILabel NameText;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public GoodsId GoodsId { get; private set; }
        public EquipmentId EquipmentId { get; private set; }

        public void Initialize(GoodsId goodsId, long quantity, long price)
        {
            GoodsId = goodsId;
            Image.spriteName = goodsId.ToString();
            NameText.SetText(goodsId.ToString());
            QuantityText.SetText(string.Format("x{0}", quantity));
            PriceText.SetText(price);
            Button.Selected += () => FindObjectOfType<ShopView>().SelectGoods(goodsId);
        }

        public void Initialize(EquipmentId equipmentId, long quantity, long price)
        {
            Initialize(equipmentId, quantity);
            PriceText.SetText(price);
        }

        public void Initialize(EquipmentId equipmentId, long quantity)
        {
            Initialize(equipmentId);
            QuantityText.SetText(string.Format("x{0}", quantity));
        }

        public void Initialize(EquipmentId equipmentId)
        {
            EquipmentId = equipmentId;
            Image.spriteName = equipmentId.ToString();
            NameText.SetText(equipmentId.ToString());
            Button.Selected += () => FindObjectOfType<EquipmentShopView>().SelectEquipment(equipmentId);
        }

        public bool Pressed
        {
            get { return Button.Pressed; }
            set { Button.Pressed = value; }
        }
    }
}
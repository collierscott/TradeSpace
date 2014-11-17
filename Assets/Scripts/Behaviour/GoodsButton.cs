using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Behaviour
{
    public class GoodsButton : Script
    {
        public UISprite Image;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public GoodsId GoodsId { get; private set; }
        public EquipmentId EquipmentId { get; private set; }

        public void Initialize(GoodsId goodsId, long quantity, long price, Action action)
        {
            Initialize(goodsId, quantity, action);
            PriceText.SetText(price + " $");
        }

        public void Initialize(GoodsId goodsId, long quantity, Action action)
        {
            GoodsId = goodsId;
            Image.spriteName = goodsId.ToString();
            QuantityText.SetText(string.Format("x{0}", quantity));
            Button.Selected += action;
        }

        public bool Pressed
        {
            get { return Button.Pressed; }
            set { Button.Pressed = value; }
        }
    }
}
using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Behaviour
{
    public class ShopItemButton : Script
    {
        public UISprite Image;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public ProtectedValue Id { get; private set; }
        public GoodsId GoodsId { get { return Id.String.ToEnum<GoodsId>(); } }
        public EquipmentId EquipmentId { get { return Id.String.ToEnum<EquipmentId>(); } }
        public bool Pressed { set { Button.Pressed = value; } }

        public void Initialize(ProtectedValue id, Action action, ProtectedValue quantity, ProtectedValue price = null)
        {
            Id = id.Copy();
            Image.spriteName = id.ToString();
            CommonInitialize(action, quantity, price);
        }

        public void Initialize(GoodsId goodsId, Action action, ProtectedValue quantity, ProtectedValue price = null)
        {
            Id = (long) goodsId;
            Image.spriteName = goodsId.ToString();
            CommonInitialize(action, quantity, price);
        }

        public void Initialize(EquipmentId equipmentId, Action action, ProtectedValue quantity, ProtectedValue price = null)
        {
            Id = (long) equipmentId;
            Image.spriteName = equipmentId.ToString();
            CommonInitialize(action, quantity, price);
        }

        private void CommonInitialize(Action action, ProtectedValue quantity, ProtectedValue price)
        {
            QuantityText.SetText(string.Format("x{0}", quantity));
            Button.Selected += action;

            if (price != null)
            {
                PriceText.SetText(price.Long + " $");
            }
        }
    }
}
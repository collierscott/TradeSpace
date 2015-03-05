using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Behaviour
{
    public class ShopItemButton : Script
    {
        public UISprite Image;
        public UILabel QuantityText;
        public UILabel PriceText;
        public SelectButton Button;

        public ProtectedValue Key { get; private set; }
        public ProtectedValue Id { get; private set; }
        public GoodsId GoodsId { get { return Id.String.ToEnum<GoodsId>(); } }
        public EquipmentId EquipmentId { get { return Id.String.ToEnum<EquipmentId>(); } }
        public bool Pressed { set { Button.Pressed = value; } }

        public void Initialize(ProtectedValue key, ProtectedValue id, Action action, ProtectedValue quantity, ProtectedValue price)
        {
            Key = key;
            Id = id;
            Image.spriteName = GetImageById(id.String);
            CommonInitialize(action, quantity, price);
        }

        public void Initialize(ProtectedValue key, ProtectedValue id, Action action, ProtectedValue quantity)
		{
            Initialize(key, id, action, quantity, null);
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

        private static string GetImageById(string id) // TODO: Fix me
        {
            try { return Env.GoodsDatabase[id.ToEnum<GoodsId>()].Image; } catch {}
            try { return Env.EquipmentDatabase[id.ToEnum<EquipmentId>()].Image; } catch {}
            try { return Env.ShipDatabase[id.ToEnum<ShipId>()].Image; } catch {}

            throw new Exception(id);
        }
    }
}
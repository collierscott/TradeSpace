using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class IngameMenu : UI
    {
        public GameButton InfoButton;
        public GameButton OpenButton;
        public GameButton MoveButton;
        public GameButton ReturnButton;
        public GameButton ShopButton;
        public GameButton EquipmentShopButton;
        public GameButton HangarButton;
        public GameButton StorageButton;
        public GameButton ShipShopButton;

        protected static class Colors
        {
            public static readonly Color Blue = ColorHelper.GetColor(0, 160, 255);
            public static readonly Color Green = ColorHelper.GetColor(0, 255, 0);
            public static readonly Color Red = ColorHelper.GetColor(255, 50, 0);
            public static readonly Color White = ColorHelper.GetColor(255, 255, 255);
        }

        private List<GameButton> GalaxyButtons
        {
            get { return new List<GameButton> { OpenButton }; }
        }

        private List<GameButton> SystemButtons
        {
            get { return new List<GameButton> { InfoButton, MoveButton, OpenButton }; }
        }

        private List<GameButton> PlanetButtons
        {
            get
            {
                return new List<GameButton>
                {
                    ShopButton,
                    HangarButton,
                    StorageButton,
                };
            }
        }

        private List<GameButton> StationButtons
        {
            get
            {
                return new List<GameButton>
                {
                    EquipmentShopButton,
                    ShipShopButton,
                    HangarButton,
                    StorageButton,
                };
            }
        }

        private List<GameButton> AsteroidButtons
        {
            get { return new List<GameButton> { HangarButton, EquipmentShopButton }; }
        }
      
        public void Start()
        {
            InfoButton.Up += () => ActionManager.ShowInfo(SelectManager.Location);
            MoveButton.Up += MoveButtonPressed;
            OpenButton.Up += ActionManager.Open;
            ReturnButton.Up += ActionManager.CloseScreen;
            ShopButton.Up += () => GetComponent<Shop>().Open();
            EquipmentShopButton.Up += () => GetComponent<EquipmentShop>().Open();
            HangarButton.Up += () => GetComponent<Workshop>().Open();
            StorageButton.Up += () => GetComponent<Storage>().Open();
            ShipShopButton.Up += () => GetComponent<ShipShop>().Open();

            Reset();
        }

        public void Reset()
        {
            foreach (var button in new[]
            {
                InfoButton,
                OpenButton,
                MoveButton,
                ReturnButton,
                ShopButton,
                EquipmentShopButton,
                HangarButton,
                StorageButton,
                ShipShopButton
            })
            {
                button.gameObject.SetActive(false);
            }

            var buttons = new List<GameButton>();

            if (UIScreen.Current is Galaxy)
            {
                buttons = GalaxyButtons;
            }
            else if (UIScreen.Current is Systema)
            {
                buttons = SystemButtons;
            }
            else if (UIScreen.Current is Planet)
            {
                buttons = PlanetButtons;
            }
            else if (UIScreen.Current is Station)
            {
                buttons = StationButtons;
            }
            else if (UIScreen.Current is Asteroid)
            {
                buttons = AsteroidButtons;  
            }

            buttons.Add(ReturnButton);

            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].transform.localPosition = new Vector3(40, 130 * ((buttons.Count - 1) / 2f) - 130 * i);
            }

            Refresh();
        }

        public void Refresh()
        {
            if (UIScreen.Current is Galaxy)
            {
                OpenButton.Enabled = SelectManager.System != null;
            }
            else if (UIScreen.Current is Systema)
            {
                InfoButton.Enabled = SelectManager.Location != null;

                if (SelectManager.Ship != null && SelectManager.Location != null)
                {
                    MoveButton.Enabled = SelectManager.Ship.State != ShipState.InFlight && SelectManager.Location.Name != SelectManager.Ship.Location.Name;
                    OpenButton.Enabled = SelectManager.Ship.Location.Name == SelectManager.Location.Name;

                    if (MoveButton.Enabled)
                    {
                        MoveButton.ChangeColor(ShipReady ? Colors.Green : Colors.Blue, 0.25f);
                    }
                }
                else
                {
                    MoveButton.Enabled = OpenButton.Enabled = false;
                }
            }
        }

        private static void MoveButtonPressed()
        {
            if (ShipReady)
            {
                ActionManager.MoveShip(SelectManager.Location);
            }
            else
            {
                ActionManager.TraceRoute(SelectManager.Location);
            }
        }

        private static bool ShipReady
        {
            get
            {
                return SelectManager.Ship.State == ShipState.Ready && SelectManager.Ship.Trace.Count > 0
                    && SelectManager.Ship.Trace.Last().LocationName == SelectManager.Location.Name;
            }
        }
    }
}
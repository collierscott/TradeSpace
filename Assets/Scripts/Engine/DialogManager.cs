using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class DialogManager : Script
    {
        public GameObject Container;
        public UILabel TitleText;
        public UILabel MessageText;
        public GameButton CloseButton;
        
        public void Awake()
        {
            CloseButton.Up += CloseScreen;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseScreen();
            }
        }

        public void CloseScreen()
        {
            Container.SetActive(false);
        }
        public void Show(string title, string msg)
        {
            Debug.LogWarning("DILAOG: " + title + " " + msg);

            Container.SetActive(true);

            TitleText.text = title;
            MessageText.text = msg;
                       
        }
    }
}
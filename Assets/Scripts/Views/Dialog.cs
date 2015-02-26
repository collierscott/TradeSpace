using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Dialog : Script
    {
        public GameObject Panel;
        public UILabel Subject;
        public UILabel Message;

        private const float TweenSpeed = 0.2f;

        public void Start()
        {
            Panel.GetComponent<UIPanel>().alpha = 0;
            Panel.SetActive(true);
        }

        public void Open(string subject, string message)
        {
            Subject.SetText(subject);
            Message.SetText(message);

            Panel.collider2D.enabled = true;
            TweenAlpha.Begin(Panel, TweenSpeed, 1);
        }

        public void Close()
        {
            Panel.collider2D.enabled = false;
            TweenAlpha.Begin(Panel, TweenSpeed, 0);
        }
    }
}
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class Base : Script
    {
        public Transform Panel;
        public static Base Previous;
        public static Base Current;
        public static SelectManager SelectManager;
        public static ActionManager ActionManager;

        public void Awake()
        {
            if (SelectManager != null) return;

            SelectManager = GetComponent<SelectManager>();
            ActionManager = GetComponent<ActionManager>();
        }

        public virtual void Open()
        {
            Initialize();
            Panel.gameObject.SetActive(true);
            enabled = true;
        }

        public void Close()
        {
            Cleanup();
            Panel.gameObject.SetActive(false);
            enabled = false;
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void Cleanup()
        {
        }

        protected void Open<T>() where T : Base
        {
            GetComponent<T>().Open();
        }

        protected void Close<T>() where T : Base
        {
            GetComponent<T>().Close();
        }
    }
}
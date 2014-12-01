using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class BaseView : Script
    {
        public Transform Panel;
        public static BaseView Previous;
        public static BaseView Current;
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

        protected void Open<T>() where T : BaseView
        {
            GetComponent<T>().Open();
        }

        protected void Close<T>() where T : BaseView
        {
            GetComponent<T>().Close();
        }
    }
}
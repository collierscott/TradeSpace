using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class ViewBase : Script
    {
        public Transform Panel;

        public static ViewBase Previous;
        public static ViewBase Current;

        public static SelectManager SelectManager;
        public static ActionManager ActionManager;

        public void Awake()
        {
            if (SelectManager != null) return;

            SelectManager = GetComponent<SelectManager>();
            ActionManager = GetComponent<ActionManager>();
        }

        public void Open()
        {
            if (this is IScreenView)
            {
                if (Current != null)
                {
                    Current.Close();
                }

                Previous = Current;
                Current = this;
            }

            Initialize();
            Panel.gameObject.SetActive(true);
            enabled = true;

            if (this is IScreenView)
            {
                GetComponent<IngameMenu>().Reset();
            }
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

        protected void Open<T>() where T : ViewBase
        {
            GetComponent<T>().Open();
        }

        protected void Close<T>() where T : ViewBase
        {
            GetComponent<T>().Close();
        }
    }
}
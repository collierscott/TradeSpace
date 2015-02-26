using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class UI : Script
    {
        public Transform Panel;
        public bool Opened { get; private set; }

        public virtual void Open()
        {
            if (Opened) return;

            Log.Debug("Opening view: {0}", GetType().Name);

            Initialize();
            Panel.gameObject.SetActive(true);
            enabled = Opened = true;
        }

        public void Close()
        {
            Log.Debug("Closing view: {0}", GetType().Name);
            
            Cleanup();
            Panel.gameObject.SetActive(false);
            enabled = Opened = false;
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void Cleanup()
        {
        }

        protected void Open<T>() where T : UI
        {
            GetComponent<T>().Open();
        }

        protected void Close<T>() where T : UI
        {
            GetComponent<T>().Close();
        }
    }
}
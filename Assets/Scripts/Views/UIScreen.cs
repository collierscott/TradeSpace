using System;
using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public abstract class UIScreen : UI
    {
        public static UIScreen Prev;
        public static UIScreen Current;
        private const float LoadingTime = 0.25f;

        public override void Open()
        {
            GetComponent<Loading>().Open(LoadingTime);
            TaskScheduler.CreateTask(OpenScreen, LoadingTime);
        }

        public void Open(Action callback)
        {
            GetComponent<Loading>().Open(LoadingTime);
            TaskScheduler.CreateTask(OpenScreen, LoadingTime, callback);
        }

        public void Open(Action prepare, Action callback)
        {
            GetComponent<Loading>().Open(LoadingTime);
            TaskScheduler.CreateTask(() => { prepare(); OpenScreen(); }, LoadingTime, callback);
        }

        private void OpenScreen()
        {
            GetComponent<Loading>().Close(LoadingTime);

            Prev = Current;
            Current = this;

            if (Prev != null)
            {
                Prev.Close();
            }

            base.Open();
            GetComponent<IngameMenu>().Reset();
        }
    }
}
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
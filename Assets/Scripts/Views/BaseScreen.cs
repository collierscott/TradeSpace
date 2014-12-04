using Assets.Scripts.Common;

namespace Assets.Scripts.Views
{
    public class BaseScreen : Base
    {
        private const float LoadingTime = 0.5f;

        public override void Open()
        {
            GetComponent<Loading>().Open(LoadingTime);

            TaskScheduler.CreateTask(() =>
            {
                GetComponent<Loading>().Close(LoadingTime);

                if (Current != null)
                {
                    Current.Close();
                }

                Previous = Current;
                Current = this;

                Initialize();
                Panel.gameObject.SetActive(true);
                enabled = true;
                GetComponent<IngameMenu>().Reset();
            }, LoadingTime);
        }
    }
}
using Assets.Scripts.Views;

namespace Assets.Scripts.Behaviour
{
    public class AsteroidButton : LocationButton
    {
        protected override void Open()
        {
            FindObjectOfType<AsteroidView>().Open();
        }
    }
}
namespace Assets.Scripts.Common
{
	public static class Finder
	{
        public static T FindObjectOfType<T>()
        {
            return (T) (object) UnityEngine.Object.FindObjectOfType(typeof(T));
        }

        public static T[] FindObjectsOfType<T>()
        {
            return (T[]) (object) UnityEngine.Object.FindObjectsOfType(typeof(T));
        }
	}
}

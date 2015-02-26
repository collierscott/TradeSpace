using UnityEngine;

public class Script : MonoBehaviour
{
    public T Get<T>() where T : MonoBehaviour
    {
        return GetComponent<T>();
    }

    public static T Find<T>() where T : MonoBehaviour
    {
        return FindObjectOfType<T>();
    }
}
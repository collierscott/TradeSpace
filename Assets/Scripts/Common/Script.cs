using System;
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

    protected static string TimespanToString(TimeSpan timeSpan)
    {
        return string.Format("{0}:{1:D2}:{2:D2}", Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);
    }
}
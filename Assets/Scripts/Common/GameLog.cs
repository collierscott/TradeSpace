using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class GameLog
    {
        private const string LogKey = "GameLog";

        public static void Write(string message, params object[] args)
        {
            message = string.Format(message, args);

            if (!PlayerPrefs.HasKey(LogKey))
            {
                PlayerPrefs.SetString(LogKey, "");
            }

            var log = PlayerPrefs.GetString(LogKey);

            if (log.Length > 1024*1024)
            {
                log = "";
            }

            PlayerPrefs.SetString(LogKey, string.Format("{0}\r\n[{1}] {2}", log, DateTime.Now, message));
        }

        public static void Write(Exception exception)
        {
            Write(exception.ToString());
        }

        public static string Read()
        {
            return PlayerPrefs.GetString(LogKey);
        }
    }
}
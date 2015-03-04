using System;
using Assets.Scripts.Common;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class Engine : Script
    {
        public void Awake()
        {
            Env.Initialize();
            AwakeAsteroid();
        }

        public void AwakeDefault()
        {
            SelectManager.SelectSystem(Env.SystemNames.Alpha);
            GetComponent<Galaxy>().Open(() => GetComponent<TweenMap>().Set(-Env.Galaxy["Spider"].Position));
        }

        public void AwakeAsteroid()
        {
            SelectManager.SelectSystem(Env.SystemNames.Alpha);
            SelectManager.SelectLocation(Env.Systems[Env.SystemNames.Alpha]["A100200"]);
            GetComponent<Asteroid>().Open();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Profile.Instance.Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Profile.Load();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Profile.Reset();
            }
        }

        public void UpdateShops()
        {
            if (DateTime.UtcNow >= Profile.Instance.InitShopsTime.DateTime.AddHours(12))
            {
                GetComponent<EnvironmentManager>().InitShops();
            }

            TaskScheduler.CreateTask(UpdateShops, 10);
        }
    }
}
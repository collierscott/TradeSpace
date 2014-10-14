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
            SelectManager.SelectSystem(Env.SystemNames.Andromeda);
            GetComponent<GalaxyView>().Open();
        }

        //public void Awake() // Test asteroid
        //{
        //    Env.Initialize();
        //    //Тест для диалога астеройда
        //    SelectManager.SelectLocation(
        //        Env.Systems[Env.SystemNames.Andromeda][Andromeda.A100200.Name]);
        //    GetComponent<ActionManager>().OpenPlayerScreen(PlayerScreen.Asteroid);
        //    return;
        //    GetComponent<ActionManager>().OpenPlayerScreen(PlayerScreen.Galaxy);
        //}

        //public void Awake() // Test hangar
        //{
        //    Env.Initialize();

        //    GetComponent<SelectManager>().SelectSystem(SystemId.Andromeda);
        //    GetComponent<SelectManager>().SelectLocation(Env.Systems[SystemId.Andromeda][LocationId.Netune]);
        //    GetComponent<HangarView>().Open();
        //}

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
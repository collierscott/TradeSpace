﻿using System;
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
            SelectManager.SelectSystem(Env.SystemNames.Alpha);
            GetComponent<Galaxy>().Open(() => GetComponent<TweenMap>().Set(-Env.Galaxy["Spider"].Position));
        }


        //public void Awake() //тест диалог
        //{
        //    GetComponent<ActionManager>().ShowInfo("Тест заголовок 111", "Тест сообщение  222");

        //}

        //public void Awake() // Test asteroid
        //{
        //    Env.Initialize();

        //    //SelectManager.SelectShip(0);
        //    SelectManager.SelectSystem(Env.SystemNames.Andromeda);
        //    SelectManager.SelectLocation(Env.Systems[Env.SystemNames.Andromeda][Assets.Scripts.Environment.AndromedaSystem.Andromeda.A100200.Name]);
        //    GetComponent<AsteroidView>().Open();
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
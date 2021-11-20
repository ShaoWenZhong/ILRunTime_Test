using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HotFix_Project.Scrips.UI;
using HotFix_Project.Scrips.Utils;
using HotFix_Project.Scrips.Managers;

namespace HotFix_Project.Scrips
{
    class Main
    {
        public static void Init()
        {
            Debug.Log("hot fix init");
            GameLanch.Instance.Test();
            EventUtil.SendMessage(EventType.GameStart);
           UIManager.Instance.ShowOrLoadView(UIType.LoginUI);
        }

        public static void Update(float deltaTime)
        {
            UIManager.Instance.Update(deltaTime);
        }

        public static void LateUpdate()
        {
            //Debug.Log("LateUpdate");
        }

        public static void FixedUpdate()
        {
            //Debug.Log("FixedUpdate");
        }
    }
}

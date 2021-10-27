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
            UIManager uIManager = UIManager.Instance;
        }

        public static void Update()
        {
            //Debug.Log("Update");
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

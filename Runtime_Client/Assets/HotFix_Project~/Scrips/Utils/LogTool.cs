using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project.Scrips.Utils
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public static class LogTool
    {
        public static void LogError(string msg)
        {
            Debug.LogError(msg);
        }

        public static void Log(string msg)
        {
            Debug.Log(msg);
        }

    }
}

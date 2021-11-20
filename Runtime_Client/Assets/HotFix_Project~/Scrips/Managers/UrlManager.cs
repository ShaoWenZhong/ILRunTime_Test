using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.Scrips.Managers
{
    public static class UrlManager
    {
        public static string GetUIPath(string path)
        {
            string full_path = "Assets/AssetsPackage/UI/Panel/" + path + ".prefab";
            return full_path;
        }
    }
}

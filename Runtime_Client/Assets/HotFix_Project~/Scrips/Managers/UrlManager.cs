using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.Scrips.Managers
{
    public static class UrlManager
    {
        public static string GetUIPrefabPath(string path)
        {
            string full_path = "Assets/AssetsPackage/UI/Panel/" + path + ".prefab";
            return full_path;
        }

        public static string GetUITexturePath(string path)
        {
            string full_path = "Assets/AssetsPackage/UI/Texture/" + path;
            return full_path;
        }

        public static string GESpriteAtlasPath(string path)
        {
            string full_path = "Assets/AssetsPackage/UI/SpriteAtlas/" + path + ".prefab";
            return full_path;
        }
    }
}

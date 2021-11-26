using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class AtlasBuild
{
    [MenuItem("Assets/Build Or Update Sprite Atlas")]
    public static void Main()
    {
        Object taject = Selection.activeObject;
        Create(taject,null);
    }

    public static void Create(Object target, string filePathWithName)
    {
        if (target == null || target.GetType() != typeof(Texture2D))
            return;

        Texture2D sourceTex = target as Texture2D;
        //整体路劲
        if (string.IsNullOrEmpty(filePathWithName))
            filePathWithName = AssetDatabase.GetAssetPath(sourceTex);

        //带后缀的文件名 
        string fileNameWithExtension = Path.GetFileName(filePathWithName);
        //不带后缀文件名
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePathWithName);
        //不带文件名的路劲
        string filePath = filePathWithName.Replace(fileNameWithExtension,"");

        string assetPath = filePath + fileNameWithoutExtension + ".prefab";
        SpriteAssest spriteAssest = AssetDatabase.LoadAssetAtPath(assetPath, typeof(SpriteAssest)) as SpriteAssest;
        if (spriteAssest == null)
        {
            GameObject prefab = null;
            GameObject tempGo = new GameObject(fileNameWithExtension);
            prefab = PrefabUtility.SaveAsPrefabAsset(tempGo,assetPath);
            spriteAssest = prefab.AddComponent<SpriteAssest>();
            GameObject.DestroyImmediate(tempGo);
        }

        spriteAssest.texture = sourceTex;
        spriteAssest.spriteInfos = GetSpritesInfo(sourceTex);
        // EditorUtility.SetDirty ：这个函数告诉引擎，相关对象所属于的Prefab已经发生了更改。方便，当我们更改了自定义对象的属性的时候，自动更新到所属的Prefab中
        EditorUtility.SetDirty(spriteAssest.gameObject);
        AssetDatabase.SaveAssets();
    }

    public static List<SpriteInfo> GetSpritesInfo(Texture2D tex)
    {
        List<SpriteInfo> spriteInfos = new List<SpriteInfo>();
        string filePath = UnityEditor.AssetDatabase.GetAssetPath(tex);
        Object[] objects = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(filePath);
        DebugUtil.Log(objects.Length.ToString());
        DebugUtil.Log(filePath);
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetType() == typeof(Sprite))
            {
                SpriteInfo temp = new SpriteInfo();
                Sprite sprite = objects[i] as Sprite;
                temp.ID = i;
                temp.name = sprite.name;
                spriteInfos.Add(temp);
            }
        }
        return spriteInfos;
    }
}

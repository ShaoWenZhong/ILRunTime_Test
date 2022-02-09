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
        //����·��
        if (string.IsNullOrEmpty(filePathWithName))
            filePathWithName = AssetDatabase.GetAssetPath(sourceTex);

        //����׺���ļ��� 
        string fileNameWithExtension = Path.GetFileName(filePathWithName);
        //������׺�ļ���
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePathWithName);
        //�����ļ�����·��
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
        // EditorUtility.SetDirty ����������������棬��ض��������ڵ�Prefab�Ѿ������˸��ġ����㣬�����Ǹ������Զ����������Ե�ʱ���Զ����µ�������Prefab��
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
                temp.sprite = sprite;
                spriteInfos.Add(temp);
            }
        }
        return spriteInfos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteAssest : MonoBehaviour
{
    public Texture texture;

    public List<SpriteInfo> spriteInfos;

    public Sprite GetSpriteWithName(string name)
    {
        DebugUtil.Log("GetSpriteWithName:"+ name);
        for (int i = 0; i < spriteInfos.Count; i++)
        {
            DebugUtil.Log("==========:" + spriteInfos[i].name);
            if (spriteInfos[i].name.Equals(name))
                return spriteInfos[i].sprite;
        }
        return null;
    }
}

[System.Serializable]
public class SpriteInfo
{
    public int ID;
    public string name;
    public Sprite sprite;
}

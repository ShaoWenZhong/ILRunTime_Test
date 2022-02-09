using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project.Scrips.Managers
{
    /// <summary>
    /// 图集管理
    /// </summary>
    public class AtlasManager:Utils.Singleton_HotFix<AtlasManager>
    {
        private Dictionary<string, SpriteAssest> atlasDic = new Dictionary<string, SpriteAssest>(); 

        public void LoadSprite(string atlas,string spriteName, AtlasSprite atlasSprite, bool isSetNativeSize = false)
        {
            DebugUtil.Log("LoadSprite::"+ atlas);
            SpriteAssest _spriteAssest;
            if (atlasDic.TryGetValue(atlas,out _spriteAssest))
            {
                DebugUtil.Log("LoadSprite::TryGetValue");
                var _image = _spriteAssest.GetSpriteWithName(spriteName);
                if (_image == null)
                {
                    DebugUtil.LogError("图集中缺少图片:"+atlas+"::"+spriteName);
                    return;
                }
                atlasSprite.atlasSourceImage.sprite = _image;
                if (isSetNativeSize)
                    atlasSprite.MakePixelPrefect();
            }
            else
            {
                string atlasPath = UrlManager.GESpriteAtlasPath(atlas);
                AddressableManager.Instance.LoadAssetAsync<UnityEngine.Object>(atlasPath, new Action<UnityEngine.Object>((obj) =>
                {
                    GameObject go = obj as GameObject;
                    DebugUtil.Log("LoadSprite::LoadAsset");
                    SpriteAssest _ass = go.GetComponent<SpriteAssest>();
                    if (_ass == null)
                    {
                        DebugUtil.LogError("没有挂在图集资源：" + atlas);
                        return;
                    }
                    atlasDic.Add(atlas, _ass);
                    atlasSprite.atlas = _ass;
                    atlasSprite.Sprite = spriteName;
                    if (isSetNativeSize)
                        atlasSprite.MakePixelPrefect();
                }));
            }
        }
    }
}

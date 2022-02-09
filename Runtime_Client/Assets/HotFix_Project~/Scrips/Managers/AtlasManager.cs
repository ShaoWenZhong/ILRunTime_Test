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

        public void LoadSprite(string atlas,string spriteName, AtlasSprite atlasSprite, bool isSetNativeSize)
        {
            SpriteAssest _spriteAssest;
            if (atlasDic.TryGetValue(atlas,out _spriteAssest))
            {
                var _image = _spriteAssest.GetSpriteWithName(spriteName);
                if (_image == null)
                {
                    DebugUtil.LogError("图集中缺少图片:"+atlas+"::"+spriteName);
                    return;
                }
                atlasSprite.atlasSourceImage.sprite = _image;
            }
            else
            {
                string atlasPath = UrlManager.GESpriteAtlasPath(atlas);
                AddressableManager.Instance.LoadAsset<GameObject>(atlasSprite,new UnityEngine.ac);
            }

        }
    }
}

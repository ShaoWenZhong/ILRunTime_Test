using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HotFix_Project.Scrips.Managers
{
    /// <summary>
    /// UI基类
    /// </summary>
    public class UIBase
    {

        public UIViewBase parent_view;

        protected GameObject panelGO;

        protected bool isInitialize = false;
        protected bool isPop = false;
        protected bool isOpen = false;
        protected bool lastOpen = false;
        protected bool isActive = false;
        protected string assetPath;
        protected bool isLoadComplete = false;
        protected bool isDeletening = false;
        //缓存窗体的RectTransform组件
        protected RectTransform thisTrans;
        protected Action onLoadedComlete;

        protected string ui_name;

        protected List<UIBase> subItemList;
        private bool isChecking_Recursive = false;

        protected List<Texture> texture_list = new List<Texture>();

        protected virtual void Initlialize() {
            isInitialize = true;
            if (onLoadedComlete != null)
            {
                onLoadedComlete();
                onLoadedComlete = null;
            }
        }

        protected virtual void OnShowSelf(){ }

        protected virtual void OnHideSelf(){ }

        public virtual bool IsView(){return false;}

        public virtual bool IsItem(){return false;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignore_avtive">忽略显示隐藏操作，省性能</param>
        public virtual void SetIsPop(bool value,bool ignore_avtive = false) { }

        protected virtual void SetGoToDelete()
        {
            if (isDeletening)
                return;

            lastOpen = false;
            isDeletening = true;
            onLoadedComlete = null;
            SetPopSubItem(false);
        }

        protected virtual void SetIsPopRecursive(bool value)
        {
            if (value)
            {
                if (lastOpen)
                    SetIsPop(true, true);
                lastOpen = false;
            }
            else
            {
                lastOpen = isOpen;
                SetIsPop(false, true);
            }
            SetPopSubItem(value);
        }

        protected virtual void SetPopSubItem(bool value)
        {
            if (isChecking_Recursive)
                return;

            isChecking_Recursive = true;
            if (subItemList.Count > 0)
            {
                foreach (var item in subItemList)
                {
                    if (isDeletening)
                        item.SetGoToDelete();
                    else
                        item.SetIsPopRecursive(value);
                }
            }
            isChecking_Recursive = false;
        }


        protected virtual void OnLoadedPrefab(UnityEngine.Object obj)
        {
            if (null == obj)
            {
                Debug.LogError("面板加载失败path::" + assetPath);
                return;
            }
            GameObject gameObject = GameObject.Instantiate(obj) as GameObject;
            panelGO = gameObject;
            thisTrans = gameObject.GetComponent<RectTransform>();
        }

        protected virtual void InitUI(string path,string _ui_name = null)
        {
            ui_name = _ui_name;
            assetPath = UrlManager.GetUIPrefabPath(path);
        }

        public virtual void LoadUI()
        {
            AddressableManager.Instance.LoadGameObject(assetPath, OnLoadedPrefab);
        }

        /// <summary>
        /// 添加界面加载完回调
        /// </summary>
        /// <param name="action"></param>
        public virtual void AddLoadedComplete(Action action)
        {
            if (isLoadComplete)
                action();
            else
                onLoadedComlete += action;
        }

        public virtual void Dispose(){}

        public virtual void DeleteMe()
        {
            onLoadedComlete = null;
            thisTrans = null;
            DebugUtil.Log("DeleteMe UI:" + ui_name);
            DisposeTexture();
            if (panelGO)
                GameObject.DestroyImmediate(panelGO);
            panelGO = null;
        }

        protected void DisposeImage()
        {
            
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="url"></param>
        protected void LoadImage(string url,RawImage rawImage,bool setNativeSize = false)
        {
            string path = UrlManager.GetUITexturePath(url);
            DebugUtil.Log(path);
            AddressableManager.Instance.LoadAssetAsync<UnityEngine.Object>(path, new Action<UnityEngine.Object>((tex)=> {
                var _tex = tex as Texture;
                if (rawImage != null && _tex != null)
                {
                    rawImage.texture = _tex;
                    texture_list.Add(_tex);
                }
                else
                {
                    AddressableManager.Instance.Release<Texture>(_tex);
                }
            }));
        }


        private void DisposeTexture()
        {
            DebugUtil.Log("dispose_texture:"+ui_name);
            Texture texture;
            int len = texture_list.Count;
            for (int i = 0; i < len; i++)
            {
                texture = texture_list[i];
                AddressableManager.Instance.Release<Texture>(texture);
                texture_list.RemoveAt(i);
            }
        }
    }
}

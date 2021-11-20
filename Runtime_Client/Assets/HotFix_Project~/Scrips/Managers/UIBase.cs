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

        protected List<RawImage> rawImages;

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
            assetPath = UrlManager.GetUIPath(path);
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
            DisposeAllImage();
            if (panelGO)
                GameObject.DestroyImmediate(panelGO);
            panelGO = null;
        }

        /// <summary>
        /// 释放所有图片资源
        /// </summary>
        protected void DisposeAllImage()
        {
            
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="url"></param>
        protected void DisposeImage(string url)
        {
            
        }
    }
}

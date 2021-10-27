using HotFix_Project.Scrips.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HotFix_Project.Scrips.Managers
{

    public enum UIRootType
    {
        Botton,//底部
        Main,// 主界面
        Pop,// 弹窗
        Pop1,
        Guild,//引导
        Loading,//加载界面
        Alert,//警告弹窗
        Message,//飘字信息
        Cheat,//GM
    }

    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager:Utils.Singleton_HotFix<UIManager>
    {
        //缓存所有打开过的窗体
        private Dictionary<UIType, UIBase> dicAllUI;
        //缓存正在显示的窗体
        private Dictionary<UIType, UIBase> dicShowUI;


        //缓存最近显示出来的窗体
        private UIBase currentUI = null;
        //缓存上一个窗体
        // private BaseUI beforeUI = null;
        private UIType beforeUiId = UIType.NullUI;

        //缓存画布
        private Transform canvas;

        //画布层级
        private Dictionary<UIRootType, GameObject> rootDic;

        private int dispose_time = 10;
        private int close_time = 30;
        private bool isDisposing = false;

        protected override void Init()
        {
            base.Init();
            dicAllUI = new Dictionary<UIType, UIBase>();
            dicShowUI = new Dictionary<UIType, UIBase>();
            rootDic = new Dictionary<UIRootType, GameObject>();
            canvas = GameObject.Find("DontDestroy/Canvas").transform;
            for (int i = 0; i < 9; i++)
            {
                UIRootType _rootType = (UIRootType)i;
                string root_name = _rootType.ToString();
                GameObject rootGo = new GameObject(root_name);
                rootGo.transform.SetParent(canvas,false);
                RectTransform rectTransform = rootGo.AddComponent<RectTransform>();
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.sizeDelta = new Vector2(0,0);
                rootGo.layer = LayerMask.NameToLayer("UI");
                rootDic.Add(_rootType, rootGo);
            }
        }

        /// <summary>
        /// 层级节点
        /// </summary>
        /// <param name="uIRootType"></param>
        /// <returns></returns>
        public Transform GetUIRootParent(UIRootType uIRootType)
        {
            GameObject gameObject;
            if (rootDic.TryGetValue(uIRootType, out gameObject))
                return gameObject.transform;
            return null;
        }

        /// <summary>
        /// 自动销毁时间
        /// </summary>
        /// <param name="disTime"></param>
        public void SetDisposeTime(int disTime)
        {
            dispose_time = disTime;
        }

        public UIBase ShowOrLoadView(UIType uIType)
        {
            UIBase uIBase;
            if (dicAllUI.TryGetValue(uIType, out uIBase))
            {
                uIBase.SetIsPop(true);
            }
            else
            {
                return CreatView(uIType);
            }
            return uIBase;
        }

        private UIBase CreatView(UIType uIType)
        {
            UIBase _class;
            if (dicAllUI.TryGetValue(uIType, out _class) == false)
            {
                _class = GetUIClass(uIType);
                dicAllUI.Add(uIType,_class);
                _class.LoadPanel();
            }
            return _class;
        }

        private void DisposeUI()
        {
            isDisposing = true;
            float now = Time.realtimeSinceStartup;
            foreach (var view_item in dicAllUI)
            {
                if (view_item.Value.closeTime > 0 && view_item.Value.GetIsPop() == false)
                {
                    if (now - view_item.Value.closeTime > close_time)
                        DestroyView(view_item.Value);
                }
            }
            isDisposing = false;
        }

        private void DestroyView(UIBase uiBase)
        {
            
        }

        public void HideOtherView(UIRootType uIRootType,UIBase ignoreView)
        {
            foreach (var item in dicAllUI)
            {
                UIBase uIBase = item.Value;
                if (uIBase.rootType == uIRootType && uIBase != ignoreView)
                    uIBase.SetUIActive(false);
            }
        }

        public void CloseOtherView(UIRootType uIRootType, UIBase ignoreView)
        {
            foreach (var item in dicAllUI)
            {
                UIBase uIBase = item.Value;
                if (uIBase.rootType == uIRootType && uIBase != ignoreView)
                    uIBase.SetIsPop(false);
            }
        }


        private UIBase GetUIClass(UIType uIType)
        {
            switch (uIType)
            {
                case UIType.NullUI:
                    return null;
                case UIType.LoginUI:
                    //return typeof(LoginView);
                    return new LoginView();
                case UIType.MainUI:
                    return null;
                default:
                    break;
            }
            return null;
        }
    }
}

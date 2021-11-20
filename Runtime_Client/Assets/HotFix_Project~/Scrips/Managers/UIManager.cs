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
        private Dictionary<UIType, UIViewBase> dicAllUI;
        //缓存正在显示的窗体
        private Dictionary<UIType, UIViewBase> dicShowUI;


        //缓存最近显示出来的窗体
        private UIViewBase currentUI = null;
        //缓存上一个窗体
        // private BaseUI beforeUI = null;
        private UIType beforeUiId = UIType.NullUI;

        //缓存画布
        private Transform canvas;

        //画布层级
        private Dictionary<UIRootType, GameObject> rootDic;

        private float dispose_time = 0;

        private int dispos_duration = 10;
        //自动销毁时间
        //private int close_time = 50;
        private int close_time = 10;

        private bool isDisposing = false;


        //根据机型优化下
        private void InitDataByMachine()
        {
            if (SystemInfo.systemMemorySize < 1224)
            {
                close_time = 30;
                dispos_duration = 10;
            }
        }

        protected override void Init()
        {
            base.Init();
            dispose_time = 0;
            InitDataByMachine();
            dicAllUI = new Dictionary<UIType, UIViewBase>();
            dicShowUI = new Dictionary<UIType, UIViewBase>();
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

        public UIViewBase ShowOrLoadView(UIType uIType,Action loadComplete = null)
        {
            UIViewBase uIBase;
            if (dicAllUI.TryGetValue(uIType, out uIBase))
            {
                uIBase.SetIsPop(true);
            }
            else
            {
                uIBase = CreatView(uIType);
            }
            if(loadComplete != null)
                uIBase.AddLoadedComplete(loadComplete);
            return uIBase;
        }

        private UIViewBase CreatView(UIType uIType)
        {
            UIViewBase _class;
            if (dicAllUI.TryGetValue(uIType, out _class) == false)
            {
                _class = GetUIClass(uIType);
                dicAllUI.Add(uIType,_class);
                _class.LoadUI();
            }
            return _class;
        }

        public void Update(float deltatime)
        {
            dispose_time += deltatime;
            if (dispose_time > dispos_duration)
            {
                dispose_time = 0;
                DisposeInactiveView();
            }
        }

        private void DisposeInactiveView()
        {
            isDisposing = true;
            float now = Time.realtimeSinceStartup;
            if (dicAllUI.Count > 0)
            {
                var _list = dicAllUI.Values.ToList();
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i].closeTime > 0 && _list[i].GetIsPop() == false)
                    {
                        if (now - _list[i].closeTime > close_time)
                            DestroyView(_list[i]);
                    }
                }
            }
            isDisposing = false;
        }

        private void DestroyView(UIViewBase uiBase)
        {
            UIViewBase _view;
            if (dicAllUI.TryGetValue(uiBase.GetUIType,out _view))
            {
                DebugUtil.Log("DestroyView::" + _view.GetUIType);
                _view.DeleteMe();
                dicAllUI.Remove(_view.GetUIType);
            }
        }

        public void HideOtherView(UIRootType uIRootType,UIViewBase ignoreView)
        {
            foreach (var item in dicAllUI)
            {
                UIViewBase uIBase = item.Value;
                if (uIBase.rootType == uIRootType && uIBase != ignoreView)
                    uIBase.SetUIActive(false);
            }
        }

        public void CloseOtherView(UIRootType uIRootType, UIViewBase ignoreView)
        {
            foreach (var item in dicAllUI)
            {
                UIViewBase uIBase = item.Value;
                if (uIBase.rootType == uIRootType && uIBase != ignoreView)
                    uIBase.SetIsPop(false);
            }
        }

        private UIViewBase GetUIClass(UIType uIType)
        {
            switch (uIType)
            {
                case UIType.NullUI:
                    return null;
                case UIType.LoginUI:
                    return new LoginView();
                case UIType.SelectRoleView:
                    return new SelectRoleView();
                case UIType.MainUI:
                    return null;
                default:
                    break;
            }
            return null;
        }
    }
}

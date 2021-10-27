using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HotFix_Project.Scrips.Utils;

namespace HotFix_Project.Scrips.Managers
{
    /// <summary>
    /// UI基类
    /// </summary>
    public class UIBase
    {
        //窗体类型
        protected ShowUIMode showUIMode;

        public float closeTime;

        public UIRootType rootType;

        //缓存窗体的RectTransform组件
        protected GameObject panelGO;

        protected RectTransform thisTrans;

        protected bool isFullScren = false;

        protected bool isHideMainUI = false;

        protected bool isCLoseOtherView = false;


        private bool isInitialize = false;
        private bool isPop = false;
        private bool isOpen = false;
        private bool isActive = false;
        private bool isHideCamera = false;
        private int openOhterCount = 0;
        private string assetPath;

        //当前窗体的ID
        protected UIType curUIType = UIType.NullUI;
        //上一个窗体的ID
        protected UIType beforeUiId = UIType.NullUI;

        //获取当前窗体的ID
        public UIType GetUiId
        {
            get
            {
                return curUIType;
            }
        }

        //用于判断窗体显示出来的时候，是否需要去隐藏其他窗体
        public bool IsHideOtherUI()
        {
            if (showUIMode == ShowUIMode.DoNothing)
            {
                return false;//不需要隐藏其他窗体
            }
            else
            {
                //需要去处理隐藏其他窗体的逻辑
                return true;// E_ShowUIMode.HideOther与  E_ShowUIMode.HideAll
            }
        }

        public virtual void InitPanel(string path, UIType uIType, UIRootType uIRootType)
        {
            assetPath = path;
            curUIType = uIType;
            rootType = uIRootType;
            //GameObject gameObject = ResManager.Instance.LoadUI();
            //OnLoadedPrefab(gameObject);
        }

        /// <summary>
        /// 加载完
        /// </summary>
        /// <param name="gameObject"></param>
        private void OnLoadedPrefab(GameObject gameObject)
        {
            if (null == gameObject)
            {
                Debug.LogError("面板加载失败" + assetPath);
                return;
            }
            panelGO = gameObject;
            thisTrans = gameObject.GetComponent<RectTransform>();
            thisTrans.SetParent(UIManager.Instance.GetUIRootParent(rootType));
            panelGO.layer = LayerMask.NameToLayer("UI");
            Initlialize();
            isInitialize = true;
            if (isOpen == false)
            {
                SetUIActive(false);
                closeTime = Time.realtimeSinceStartup;
            }
            else
            {
                isOpen = false;
                SetIsPop(true, true);
            }
        }

        public bool GetIsPop()
        {
            return isPop;
        }

        public void SetIsPop(bool value, bool fromInitPrefab = false)
        {
            if (isInitialize == false)
            {
                isOpen = value;
                return;
            }

            if (isPop != value)
            {
                isOpen = value;
                if (value)
                    PopUp();
                else
                    PopDown();
                SetPopSubItem(value);
            }
            else if (value)
            {
                SetUIActive(true);
            }
        }

        private void PopUp()
        {
            if (panelGO == null)
            {
                Debug.LogError("UIBase PopUp panelGo == null" + curUIType.ToString());
                return;
            }

            if (isFullScren && showUIMode == ShowUIMode.HideOther)
            {
                UIManager.Instance.HideOtherView(UIRootType.Pop, this);
                openOhterCount = openOhterCount + 1;
            }

            if (isCLoseOtherView)
                UIManager.Instance.CloseOtherView(UIRootType.Pop, this);
            SetUIActive(true);
            closeTime = 0;
            OnShowSelf();
            EventUtil.SendMessage(EventType.UIViewPopChange, curUIType,true);
        }

        private void PopDown()
        {
            
        }

        //窗体的显示
        public void SetUIActive(bool value)
        {
            if (isInitialize == false || isActive == false)
                return;
            isActive = value;
            if (value)
            {
                if (isFullScren && isHideCamera)
                {
                    CameraManager.Instance.HideCamera();
                }
            }
            else
            {
                if (isFullScren && isHideCamera)
                    CameraManager.Instance.ShowCamera();
            }
            if(panelGO)
                panelGO.SetActive(value);
        }

        private void SetPopSubItem(bool value)
        {

        }

        private void OnSetIsPop(bool value)
        {

        }

        public void LoadPanel()
        {
            GameObject gameObject = ResManager.Instance.LoadUI(assetPath);
            OnLoadedPrefab(gameObject);
        }

        //初始化界面元素
        protected virtual void Initlialize()
        {
        }
        ////初始化数据
        //protected virtual void InitDataOnAwake()
        //{
        //}

        protected virtual void Start()
        {
            OnShowSelf();
        }

        //初始化相关逻辑
        protected virtual void OnShowSelf()
        {

        }

        protected virtual void OnHideSelf()
        {

        }

        protected void beforeDelete()
        {

        }
    }

    //窗体ID
    public enum UIType
    {
        NullUI,
        LoginUI,//登录界面
        MainUI,//主界面
    }

    //窗体的显示方式
    public enum ShowUIMode
    {
        //  窗体显示出来的时候，不会去隐藏任何窗体
        DoNothing,
        //  窗体显示出来的时候,会隐藏掉所有的普通窗体，但是不会隐藏保持在最前方的窗体
        HideOther,
        //  窗体显示出来的时候,会隐藏所有的窗体，不管是普通的还是保持在最前方的
        HideAll
    }


    public enum UIClass
    {
        
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using HotFix_Project.Scrips.Utils;

namespace HotFix_Project.Scrips.Managers
{
    /// <summary>
    /// UI基类
    /// </summary>
    public class UIViewBase: UIBase
    {
        //窗体类型
        protected ShowUIMode showUIMode;
        public float closeTime;
        public UIRootType rootType;
        protected bool isFullScren = false;
        protected bool isHideMainUI = false;
        protected bool isCLoseOtherView = false;
        private bool isHideCamera = false;
        private int openOhterCount = 0;
        //当前窗体的ID
        protected UIType curUIType = UIType.NullUI;
        //上一个窗体的ID
        protected UIType beforeUiId = UIType.NullUI;

        public override bool IsView()
        {
            return true;
        }

        //获取当前窗体的ID
        public UIType GetUIType
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

        protected virtual void InitView(string path)
        {
            base.InitUI(path);
        }

        /// <summary>
        /// 加载完
        /// </summary>
        /// <param name="gameObject"></param>
        protected override void OnLoadedPrefab(UnityEngine.Object obj)
        {
            base.OnLoadedPrefab(obj);
            if (obj == null)
                return;
            thisTrans.SetParent(UIManager.Instance.GetUIRootParent(rootType),false);
            panelGO.layer = LayerMask.NameToLayer("UI");
            Initlialize();
            SetIsPop(true);

            //if (isOpen == false)
            //{
            //    SetUIActive(false);
            //    closeTime = Time.realtimeSinceStartup;
            //}
            //else
            //{
            //    isOpen = false;
            //    SetIsPop(true, true);
            //}
        }

        public bool GetIsPop()
        {
            return isPop;
        }

        public override void SetIsPop(bool value, bool ignore_avtive = false)
        {
            base.SetIsPop(value, ignore_avtive);
            if (isInitialize == false)
            {
                isOpen = value;
                return;
            }
            if (isOpen != value)
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
                DebugUtil.LogError("UIBase PopUp panelGo == null::" + curUIType.ToString());
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
            DebugUtil.Log("UI PopUp::" + curUIType.ToString());
            EventUtil.SendMessage(EventType.UIViewPopChange, curUIType,true);
        }

        private void PopDown()
        {
            if (panelGO == null)
                return;
            SetUIActive(false);
            closeTime = Time.realtimeSinceStartup;
            DebugUtil.Log("UI Popdown::" + curUIType.ToString());
            OnHideSelf();
            EventUtil.SendMessage(EventType.UIViewPopChange,curUIType,false);
        }

        //窗体的显示
        public void SetUIActive(bool value)
        {
            if (isInitialize == false || isActive == value)
                return;
            isActive = value;
            if (value)
            {

                if (isFullScren)
                    AspectScreen();

                if (isFullScren && isHideCamera)
                    CameraManager.Instance.HideCamera();
            }
            else
            {

                if (isFullScren && isHideCamera)
                    CameraManager.Instance.ShowCamera();
            }
            if(panelGO)
                panelGO.SetActive(value);
        }

        //锚点设置
        private void AspectScreen()
        {
            DebugUtil.Log("AspectScreen");
            thisTrans.pivot = new Vector2(0.5f,0.5f);
            thisTrans.anchorMin = new Vector2(0,0);
            thisTrans.anchorMax = new Vector2(1,1);
            thisTrans.offsetMin = new Vector2(0,0);
            thisTrans.offsetMax = new Vector2(0,0);
        }

        //初始化界面元素
        protected override void Initlialize()
        {
            base.Initlialize();
        }

        //初始化相关逻辑
        protected override void OnShowSelf()
        {
            base.OnShowSelf();
        }

        protected override void OnHideSelf()
        {
            base.OnHideSelf();
        }

        public override void DeleteMe()
        {
            base.DeleteMe();
        }

        /// <summary>
        /// 删除前操作
        /// </summary>
        protected void BeforeDelete()
        {
            isDeletening = true;
            if (GetIsPop())
                SetIsPop(false);
            else
                SetPopSubItem(false);
        }

        protected T GetChild<T>(string childPath)
        {
            T _type = default;
            if (panelGO == null)
            {
                DebugUtil.LogError("面板尚未加载完成");
                return _type;
            }
            return CustomUtil.GetChild<T>(panelGO,childPath);
        }

        protected void AddCloseBtn(string childPath)
        {
            Button button = GetChild<Button>(childPath);
            if (button)
            {
                button.onClick.AddListener(delegate() {SetIsPop(false); });
            }
        }

    }

    //窗体ID
    public enum UIType
    {
        NullUI,
        LoginUI,//登录界面
        MainUI,//主界面
        SelectRoleView,//选角
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

}

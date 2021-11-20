using System;
using HotFix_Project.Scrips.Managers;
using UnityEngine.UI;

namespace HotFix_Project.Scrips.UI
{
    public class LoginView: UIViewBase
    {

        private Button loginBtn;

        public LoginView()
        {
            isFullScren = true;
            curUIType = UIType.LoginUI;
            rootType = UIRootType.Pop;
            base.InitView("Login/LoginView");
        }

        protected override void Initlialize()
        {
            base.Initlialize();
            DebugUtil.Log("Initlialize");
            AddCloseBtn("closeBtn");
            loginBtn = GetChild<Button>("loginBtn");
            loginBtn.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
            {
                OnLoginCLick();
            }));
        }

        private void OnLoginCLick()
        {
            DebugUtil.Log("登录按钮点击");
            UIManager.Instance.ShowOrLoadView(UIType.SelectRoleView);
        }

        protected override void OnShowSelf()
        {
            base.OnShowSelf();
        }

        protected override void OnHideSelf()
        {
            base.OnHideSelf();
        }

    }
}

using System;
using HotFix_Project.Scrips.Managers;

namespace HotFix_Project.Scrips.UI
{
    public class LoginView: UIBase
    {
        public LoginView()
        {
            base.InitPanel("",UIType.LoginUI,Managers.UIRootType.Pop);
        }

        protected override void OnShowSelf()
        {
            base.OnShowSelf();
            Utils.EventUtil.AddListener<UIType>(Utils.EventType.UIViewPopChange, OnViewChange);
        }

        protected override void OnHideSelf()
        {
            base.OnHideSelf();
        }


        private void OnViewChange(UIType uitype)
        {

        }

    }
}

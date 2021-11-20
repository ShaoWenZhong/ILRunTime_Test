using System;
using HotFix_Project.Scrips.Managers;
using UnityEngine.UI;

namespace HotFix_Project.Scrips.UI
{
    public class SelectRoleView : UIViewBase
    {

        public SelectRoleView()
        {
            isFullScren = true;
            curUIType = UIType.SelectRoleView;
            rootType = UIRootType.Pop;
            base.InitView("SelectRole/SelectRoleView");
        }

        protected override void Initlialize()
        {
            base.Initlialize();
            AddCloseBtn("closeBtn");
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

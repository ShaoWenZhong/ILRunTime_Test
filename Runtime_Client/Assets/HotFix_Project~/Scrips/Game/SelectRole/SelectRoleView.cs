using System;
using HotFix_Project.Scrips.Managers;
using UnityEngine.UI;

namespace HotFix_Project.Scrips.UI
{
    public class SelectRoleView : UIViewBase
    {
        AtlasSprite role1_sprite;
        RawImage bg;
        private Button role1Btn;

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
            role1_sprite = GetChild<AtlasSprite>("role1");
            //role1_sprite.Sprite = "notice";
            AtlasManager.Instance.LoadSprite("common/common", "TextBTN_New-Start_Pressed",role1_sprite);
            bg = GetChild<RawImage>("BG");
            LoadImage("commonBg/Large_parchment", bg);
            role1Btn = GetChild<Button>("role1");
            role1Btn.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
            {
                OnRole1Click();
            }));

        }

        private void OnRole1Click()
        {
            LoadImage("commonBg/Large_parchment", bg);
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

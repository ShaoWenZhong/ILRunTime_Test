using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project.Scrips.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class UIItem:UIBase
    {
        private UIBase parent_base;

        public override bool IsItem()
        {
            return true;
        }

        protected virtual void InitItem(GameObject parent, string path, UIBase _parent_base, bool _load_async = false, string _ui_name = null)
        {
            parent_base = _parent_base;

            if (parent_view.IsItem())
                parent_view = _parent_base.parent_view;
            else
                parent_view = _parent_base as UIViewBase;

            base.InitUI(path, _ui_name);
        }

        protected override void Initlialize()
        {
            base.Initlialize();
        }

        public override void SetIsPop(bool value,bool ignore_avtive = false)
        {
            base.SetIsPop(value, ignore_avtive);
        }

        protected override void SetPopSubItem(bool value)
        {
            base.SetPopSubItem(value);
        }

        public override void DeleteMe()
        {
            base.DeleteMe();
        }
    }

}

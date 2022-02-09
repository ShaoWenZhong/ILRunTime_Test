

/**
 * Auto generated, do not edit it
 *
 */
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Data.Config
{
    public class itemItem
    {
    
        private int m_id;
        public int id
		{      //唯一ID
             get{return m_id;}         
        }    
        private int m_nameId;
        public int nameId
		{      //名字id
             get{return m_nameId;}         
        }   
        private string m_icon;
        public string icon
		{      //图标
             get{return m_icon;}         
        }   
        private string m_bgIcon;
        public string bgIcon
		{      //背景图标
             get{return m_bgIcon;}         
        }   
        private bool m_showInBackage;
        public bool showInBackage
		{      //是否背包显示
             get{return m_showInBackage;}         
        }   
        private string m_connectPotName;
        public string connectPotName
		{      //挂载点名字
             get{return m_connectPotName;}         
        }   
        private string m_prefabPath;
        public string prefabPath
		{      //prefabPath
             get{return m_prefabPath;}         
        }   
        private float m_addHp;
        public float addHp
		{      //增加血量
             get{return m_addHp;}         
        }   
        private string m_eftPrefab;
        public string eftPrefab
		{      //特效
             get{return m_eftPrefab;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_nameId=dis.ReadInt(); 
                m_icon=dis.ReadUTF(); 
                m_bgIcon=dis.ReadUTF(); 
                m_showInBackage=dis.ReadBoolean(); 
                m_connectPotName=dis.ReadUTF(); 
                m_prefabPath=dis.ReadUTF(); 
                m_addHp=dis.ReadFloat(); 
                m_eftPrefab=dis.ReadUTF(); 
            }
        }
    }
}
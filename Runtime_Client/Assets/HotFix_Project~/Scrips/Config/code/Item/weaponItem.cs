

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
    public class weaponItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private string m_prefabPath;
        public string prefabPath
		{      //prefab
             get{return m_prefabPath;}         
        }   
        private string m_connectName;
        public string connectName
		{      //挂点名字
             get{return m_connectName;}         
        }   
        private float m_time;
        public float time
		{      //使用一次用时
             get{return m_time;}         
        }   
        private string m_attackAnims;
        public string attackAnims
		{      //攻击动作名字
             get{return m_attackAnims;}         
        }   
        private List<string> m_fireNodes;
        public List<string> fireNodes
		{      //子弹发射节点数组
             get{return m_fireNodes;}         
        }   
        private List<float> m_bulletAngles;
        public List<float> bulletAngles
		{      //子弹角度
             get{return m_bulletAngles;}         
        }   
        private List<int> m_bulletIds;
        public List<int> bulletIds
		{      //子弹列表
             get{return m_bulletIds;}         
        }   
        private List<float> m_attackApplyTimes;
        public List<float> attackApplyTimes
		{      //技能1攻击生效时间
             get{return m_attackApplyTimes;}         
        }   
        private float m_hurtValue;
        public float hurtValue
		{      //伤害
             get{return m_hurtValue;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_prefabPath=dis.ReadUTF(); 
                m_connectName=dis.ReadUTF(); 
                m_time=dis.ReadFloat(); 
                m_attackAnims=dis.ReadUTF(); 
                m_fireNodes=dis.ReadListString(); 
                m_bulletAngles=dis.ReadListFloat(); 
                m_bulletIds=dis.ReadListInt(); 
                m_attackApplyTimes=dis.ReadListFloat(); 
                m_hurtValue=dis.ReadFloat(); 
            }
        }
    }
}
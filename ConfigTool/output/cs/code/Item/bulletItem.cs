

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
    public class bulletItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private string m_prefabPath;
        public string prefabPath
		{      //资源路径
             get{return m_prefabPath;}         
        }   
        private BulletPathType m_pathType;
        public BulletPathType pathType
		{      //类型id
             get{return m_pathType;}         
        }   
        private float m_speed;
        public float speed
		{      //速度
             get{return m_speed;}         
        }   
        private float m_maxLifeTime;
        public float maxLifeTime
		{      //子弹存活时间
             get{return m_maxLifeTime;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_prefabPath=dis.ReadUTF(); 
                m_pathType=dis.ReadBulletPathType(); 
                m_speed=dis.ReadFloat(); 
                m_maxLifeTime=dis.ReadFloat(); 
            }
        }
    }
}
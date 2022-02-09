

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
    public class actorAttributeItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private int m_hp;
        public int hp
		{      //血量
             get{return m_hp;}         
        }   
        private float m_moveSpeed;
        public float moveSpeed
		{      //移动速度
             get{return m_moveSpeed;}         
        }   
        private float m_chaseMaxDistanceWithInitPos;
        public float chaseMaxDistanceWithInitPos
		{      //追击最大距离
             get{return m_chaseMaxDistanceWithInitPos;}         
        }   
        private float m_chaseIgnoreOffset;
        public float chaseIgnoreOffset
		{      //追击超过距离返回途中的无视再次追击偏移
             get{return m_chaseIgnoreOffset;}         
        }   
        private float m_idleSleepTime;
        public float idleSleepTime
		{      //追击超过距离返回途中的无视再次追击偏移
             get{return m_idleSleepTime;}         
        }   
        private float m_warningRange;
        public float warningRange
		{      //警戒范围
             get{return m_warningRange;}         
        }   
        private int m_beHitBackWeights;
        public int beHitBackWeights
		{      //被击退概率(0-100)
             get{return m_beHitBackWeights;}         
        }   
        private float m_hpResume;
        public float hpResume
		{      //回血速率
             get{return m_hpResume;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_hp=dis.ReadInt(); 
                m_moveSpeed=dis.ReadFloat(); 
                m_chaseMaxDistanceWithInitPos=dis.ReadFloat(); 
                m_chaseIgnoreOffset=dis.ReadFloat(); 
                m_idleSleepTime=dis.ReadFloat(); 
                m_warningRange=dis.ReadFloat(); 
                m_beHitBackWeights=dis.ReadInt(); 
                m_hpResume=dis.ReadFloat(); 
            }
        }
    }
}
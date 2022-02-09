

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
    public class levelItem
    {
    
        private int m_id;
        public int id
		{      //关卡ID
             get{return m_id;}         
        }   
        private int m_bossId;
        public int bossId
		{      //BOSS编号
             get{return m_bossId;}         
        }   
        private int m_time;
        public int time
		{      //时长
             get{return m_time;}         
        }   
        private List<int> m_rewards;
        public List<int> rewards
		{      //成功奖励
             get{return m_rewards;}         
        }   
        private List<int> m_failRewards;
        public List<int> failRewards
		{      //失败奖励
             get{return m_failRewards;}         
        }   
        private int m_unlockAniId;
        public int unlockAniId
		{      //解锁动物
             get{return m_unlockAniId;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_bossId=dis.ReadInt(); 
                m_time=dis.ReadInt(); 
                m_rewards=dis.ReadListInt(); 
                m_failRewards=dis.ReadListInt(); 
                m_unlockAniId=dis.ReadInt(); 
            }
        }
    }
}
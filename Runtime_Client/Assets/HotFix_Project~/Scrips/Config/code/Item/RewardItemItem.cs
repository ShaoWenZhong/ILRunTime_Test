

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
    public class RewardItemItem
    {
    
        private int m_id;
        public int id
		{      //唯一ID
             get{return m_id;}         
        }    
        private int m_itemId;
        public int itemId
		{      //商品id 
             get{return m_itemId;}         
        }   
        private List<int> m_count;
        public List<int> count
		{      //数量
             get{return m_count;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_itemId=dis.ReadInt(); 
                m_count=dis.ReadListInt(); 
            }
        }
    }
}
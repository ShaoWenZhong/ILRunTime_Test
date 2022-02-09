

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
    public class actorItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private string m_model;
        public string model
		{      //模型
             get{return m_model;}         
        }   
        private int m_weaponId;
        public int weaponId
		{      //武器id
             get{return m_weaponId;}         
        }   
        private List<int> m_attributeIds;
        public List<int> attributeIds
		{      //属性id
             get{return m_attributeIds;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_model=dis.ReadUTF(); 
                m_weaponId=dis.ReadInt(); 
                m_attributeIds=dis.ReadListInt(); 
            }
        }
    }
}
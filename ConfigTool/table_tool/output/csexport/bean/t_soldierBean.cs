

/**
 * Auto generated, do not edit it
 *
 */
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Data.Beans
{
    public class t_soldierBean
    {
    
        private int m_t_id;
        public int t_id
		{      //兵种ID
             get{return m_t_id;}         
        }   
        private int m_arms;
        public int arms
		{      //兵种类型（1-机甲 2-坦克 3-战机 -1-英雄）
             get{return m_arms;}         
        }    
        private string m_name;
        public string name
		{      //兵种名称
            get
        {
            int ret;
            bool flag = int.TryParse(m_name, out ret);
            if(flag)
            {
                string tempstr = BeanFactory.getLanguageContent(ret);
                if (tempstr == "") return m_name;
                else return tempstr;
            }
            else
                return m_name;
        }         
        }   
        private float m_crystal;
        public float crystal
		{      //水晶
             get{return m_crystal;}         
        }   
        private bool m_H2;
        public bool H2
		{      //重氢消耗
             get{return m_H2;}         
        }   
        private long m_water;
        public long water
		{      //粮食消耗
             get{return m_water;}         
        }   
        private string m_waterCost;
        public string waterCost
		{      //每100兵水产量下降
             get{return m_waterCost;}         
        }   
        private list<int> m_waterCost2;
        public list<int> waterCost2
		{      //每100兵水产量下降
             get{return m_waterCost2;}         
        }                                                  

        public void LoadData(DataInputStream dis)
        {
            if (dis != null)
            { 
                m_t_id=dis.ReadInt(); 
                m_arms=dis.ReadInt(); 
                m_name=dis.ReadUTF(); 
                m_crystal=dis.ReadFloat(); 
                m_H2=dis.ReadBoolean(); 
                m_water=dis.ReadLong(); 
                m_waterCost=dis.ReadUTF(); 
                m_waterCost2=dis.ReadListInt(); 
            }
        }
    }
}
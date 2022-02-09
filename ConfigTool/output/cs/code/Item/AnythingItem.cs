

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
    public class AnythingItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private int m_intValue;
        public int intValue
		{      //数值
             get{return m_intValue;}         
        }   
        private List<int> m_intArray;
        public List<int> intArray
		{      //数值2
             get{return m_intArray;}         
        }   
        private float m_floatValue;
        public float floatValue
		{      //数值3
             get{return m_floatValue;}         
        }   
        private List<float> m_floatArray;
        public List<float> floatArray
		{      //数值4
             get{return m_floatArray;}         
        }   
        private string m_stringValue;
        public string stringValue
		{      //文本
             get{return m_stringValue;}         
        }   
        private List<string> m_stringArray;
        public List<string> stringArray
		{      //文本
             get{return m_stringArray;}         
        }   
        private bool m_boolValue;
        public bool boolValue
		{      //文本
             get{return m_boolValue;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_intValue=dis.ReadInt(); 
                m_intArray=dis.ReadListInt(); 
                m_floatValue=dis.ReadFloat(); 
                m_floatArray=dis.ReadListFloat(); 
                m_stringValue=dis.ReadUTF(); 
                m_stringArray=dis.ReadListString(); 
                m_boolValue=dis.ReadBoolean(); 
            }
        }
    }
}
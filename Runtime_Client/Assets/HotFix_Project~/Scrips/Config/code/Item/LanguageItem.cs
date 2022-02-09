

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
    public class LanguageItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private string m_chinese;
        public string chinese
		{      //中文
             get{return m_chinese;}         
        }   
        private string m_chinese_tw;
        public string chinese_tw
		{      //繁體
             get{return m_chinese_tw;}         
        }   
        private string m_english;
        public string english
		{      //英文
             get{return m_english;}         
        }   
        private string m_arabic;
        public string arabic
		{      //阿拉伯语
             get{return m_arabic;}         
        }   
        private string m_french;
        public string french
		{      //法语
             get{return m_french;}         
        }   
        private string m_spanish;
        public string spanish
		{      //西班牙语
             get{return m_spanish;}         
        }   
        private string m_japanese;
        public string japanese
		{      //日文
             get{return m_japanese;}         
        }   
        private string m_korean;
        public string korean
		{      //韩文
             get{return m_korean;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_chinese=dis.ReadUTF(); 
                m_chinese_tw=dis.ReadUTF(); 
                m_english=dis.ReadUTF(); 
                m_arabic=dis.ReadUTF(); 
                m_french=dis.ReadUTF(); 
                m_spanish=dis.ReadUTF(); 
                m_japanese=dis.ReadUTF(); 
                m_korean=dis.ReadUTF(); 
            }
        }
    }
}
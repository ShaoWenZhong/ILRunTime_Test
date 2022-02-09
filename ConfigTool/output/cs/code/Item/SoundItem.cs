

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
    public class SoundItem
    {
    
        private int m_id;
        public int id
		{      //id
             get{return m_id;}         
        }    
        private string m_clipPath;
        public string clipPath
		{      //资源路径
             get{return m_clipPath;}         
        }   
        private float m_delay;
        public float delay
		{      //延迟
             get{return m_delay;}         
        }   
        private float m_volume;
        public float volume
		{      //音量（0-1）
             get{return m_volume;}         
        }   
        private bool m_IsLoop;
        public bool IsLoop
		{      //是否循环
             get{return m_IsLoop;}         
        }   

        public void Load(DataStream dis)
        {
            if (dis != null)
            { 
                m_id=dis.ReadInt(); 
                m_clipPath=dis.ReadUTF(); 
                m_delay=dis.ReadFloat(); 
                m_volume=dis.ReadFloat(); 
                m_IsLoop=dis.ReadBoolean(); 
            }
        }
    }
}
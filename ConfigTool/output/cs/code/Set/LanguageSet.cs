
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class LanguageSet
	{
		private List<LanguageItem> list = new List<LanguageItem>();
		private Dictionary<int, LanguageItem> map = new Dictionary<int, LanguageItem>();

		private bool isLoad = false;

		public List<LanguageItem> getList()
		{
			return list;
		}

		public Dictionary<int, LanguageItem> getMap()
		{
			return map;
		}

		public LanguageItem GetItem(int id)
        {
            if (map.ContainsKey(id))
            {
                return map[id];
            }

            return null;
        }


		public void Load(DataStream dis,int rowCount)
		{
			isLoad = true;
			list.Clear();
			map.Clear();  
			try
			{
				while (rowCount-->0)
				{
					LanguageItem Item = new LanguageItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("LanguageSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "LanguageItem"+ ".bytes");
            }
		}
	}
}

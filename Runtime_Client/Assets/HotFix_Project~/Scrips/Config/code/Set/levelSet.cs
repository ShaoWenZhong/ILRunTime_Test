
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class levelSet
	{
		private List<levelItem> list = new List<levelItem>();
		private Dictionary<int, levelItem> map = new Dictionary<int, levelItem>();

		private bool isLoad = false;

		public List<levelItem> getList()
		{
			return list;
		}

		public Dictionary<int, levelItem> getMap()
		{
			return map;
		}

		public levelItem GetItem(int id)
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
					levelItem Item = new levelItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("levelSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "levelItem"+ ".bytes");
            }
		}
	}
}

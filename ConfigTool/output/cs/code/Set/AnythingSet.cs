
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class AnythingSet
	{
		private List<AnythingItem> list = new List<AnythingItem>();
		private Dictionary<int, AnythingItem> map = new Dictionary<int, AnythingItem>();

		private bool isLoad = false;

		public List<AnythingItem> getList()
		{
			return list;
		}

		public Dictionary<int, AnythingItem> getMap()
		{
			return map;
		}

		public AnythingItem GetItem(int id)
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
					AnythingItem Item = new AnythingItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("AnythingSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "AnythingItem"+ ".bytes");
            }
		}
	}
}

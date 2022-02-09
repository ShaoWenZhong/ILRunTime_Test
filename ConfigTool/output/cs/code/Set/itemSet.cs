
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class itemSet
	{
		private List<itemItem> list = new List<itemItem>();
		private Dictionary<int, itemItem> map = new Dictionary<int, itemItem>();

		private bool isLoad = false;

		public List<itemItem> getList()
		{
			return list;
		}

		public Dictionary<int, itemItem> getMap()
		{
			return map;
		}

		public itemItem GetItem(int id)
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
					itemItem Item = new itemItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("itemSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "itemItem"+ ".bytes");
            }
		}
	}
}

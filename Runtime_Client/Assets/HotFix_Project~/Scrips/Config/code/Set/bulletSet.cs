
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class bulletSet
	{
		private List<bulletItem> list = new List<bulletItem>();
		private Dictionary<int, bulletItem> map = new Dictionary<int, bulletItem>();

		private bool isLoad = false;

		public List<bulletItem> getList()
		{
			return list;
		}

		public Dictionary<int, bulletItem> getMap()
		{
			return map;
		}

		public bulletItem GetItem(int id)
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
					bulletItem Item = new bulletItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("bulletSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "bulletItem"+ ".bytes");
            }
		}
	}
}

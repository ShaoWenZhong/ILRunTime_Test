
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class weaponSet
	{
		private List<weaponItem> list = new List<weaponItem>();
		private Dictionary<int, weaponItem> map = new Dictionary<int, weaponItem>();

		private bool isLoad = false;

		public List<weaponItem> getList()
		{
			return list;
		}

		public Dictionary<int, weaponItem> getMap()
		{
			return map;
		}

		public weaponItem GetItem(int id)
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
					weaponItem Item = new weaponItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("weaponSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "weaponItem"+ ".bytes");
            }
		}
	}
}

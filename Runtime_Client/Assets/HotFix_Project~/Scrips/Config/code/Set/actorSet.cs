
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class actorSet
	{
		private List<actorItem> list = new List<actorItem>();
		private Dictionary<int, actorItem> map = new Dictionary<int, actorItem>();

		private bool isLoad = false;

		public List<actorItem> getList()
		{
			return list;
		}

		public Dictionary<int, actorItem> getMap()
		{
			return map;
		}

		public actorItem GetItem(int id)
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
					actorItem Item = new actorItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("actorSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "actorItem"+ ".bytes");
            }
		}
	}
}

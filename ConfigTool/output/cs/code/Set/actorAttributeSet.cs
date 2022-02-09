
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class actorAttributeSet
	{
		private List<actorAttributeItem> list = new List<actorAttributeItem>();
		private Dictionary<int, actorAttributeItem> map = new Dictionary<int, actorAttributeItem>();

		private bool isLoad = false;

		public List<actorAttributeItem> getList()
		{
			return list;
		}

		public Dictionary<int, actorAttributeItem> getMap()
		{
			return map;
		}

		public actorAttributeItem GetItem(int id)
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
					actorAttributeItem Item = new actorAttributeItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("actorAttributeSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "actorAttributeItem"+ ".bytes");
            }
		}
	}
}

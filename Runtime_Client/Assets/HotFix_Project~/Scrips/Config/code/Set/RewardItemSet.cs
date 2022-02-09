
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class RewardItemSet
	{
		private List<RewardItemItem> list = new List<RewardItemItem>();
		private Dictionary<int, RewardItemItem> map = new Dictionary<int, RewardItemItem>();

		private bool isLoad = false;

		public List<RewardItemItem> getList()
		{
			return list;
		}

		public Dictionary<int, RewardItemItem> getMap()
		{
			return map;
		}

		public RewardItemItem GetItem(int id)
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
					RewardItemItem Item = new RewardItemItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("RewardItemSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "RewardItemItem"+ ".bytes");
            }
		}
	}
}

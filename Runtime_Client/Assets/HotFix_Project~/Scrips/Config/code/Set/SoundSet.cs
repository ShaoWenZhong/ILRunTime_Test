
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data.Config
{
	public class SoundSet
	{
		private List<SoundItem> list = new List<SoundItem>();
		private Dictionary<int, SoundItem> map = new Dictionary<int, SoundItem>();

		private bool isLoad = false;

		public List<SoundItem> getList()
		{
			return list;
		}

		public Dictionary<int, SoundItem> getMap()
		{
			return map;
		}

		public SoundItem GetItem(int id)
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
					SoundItem Item = new SoundItem();
					Item.Load(dis);
					list.Add(Item);
					if (map.ContainsKey(Item.id))
                    {
                    	Debug.LogError("SoundSet same key:" + Item.id);
					}
					map.Add(Item.id, Item);
				}
			}
            catch (Exception ex)
            {
                Debug.LogError("import data error: " + ex.ToString() + "SoundItem"+ ".bytes");
            }
		}
	}
}

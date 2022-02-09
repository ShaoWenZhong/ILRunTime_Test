
/**
 * Auto generated, do not edit it
 */

using System;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{
	public class t_soldierContainer
	{
		private List<t_soldierBean> list = new List<t_soldierBean>();
		private Dictionary<int, t_soldierBean> map = new Dictionary<int, t_soldierBean>();

		private bool isLoad = false;

		public List<t_soldierBean> getList()
		{
			if(!isLoad)
			{
				loadDataFromBin();
			}
			return list;
		}

		public Dictionary<int, t_soldierBean> getMap()
		{
			if(!isLoad)
			{
				loadDataFromBin();
			}
			return map;
		}

		public t_soldierBean GetBean(int id)
        {
			if(!isLoad)
			{
				loadDataFromBin();
			}

            if (map.ContainsKey(id))
            {
                return map[id];
            }

            return null;
        }


		public void loadDataFromBin()
		{
			isLoad = true;
			list.Clear();
			map.Clear();
			Stream ms = ConfLoader.Singleton.getStreamByteName(typeof(t_soldierBean).Name + ".bytes");
			if(ms != null)
			{
				DataInputStream dis = new DataInputStream(ms);
				try
				{
					while (dis.Available() != 0)
					{
						t_soldierBean bean = new t_soldierBean();
						bean.LoadData(dis);
						list.Add(bean);
						if (map.ContainsKey(bean.t_id))
                        {
                            Debug.LogError("t_soldierContainer same key:" + bean.t_id);
                        }

						map.Add(bean.t_id, bean);
					}
				}
				catch (Exception ex)
				{
					Logger.err("import data error: " + ex.ToString() + typeof(t_soldierBean).Name + ".bytes");
				}

				dis.Close();
				ms.Dispose();
			}
			else
			{
				Logger.err("找不到配置数据：" + typeof(t_soldierBean).Name + ".bytes");
			}
		}
	}

}

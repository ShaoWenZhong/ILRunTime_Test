
/**
 * Auto generated, do not edit it
 */ 
using System.IO;

namespace Data.Config
{
	public class ConfigsManager
	{

		public LanguageSet LanguageSet = new LanguageSet();

		public bulletSet bulletSet = new bulletSet();

		public weaponSet weaponSet = new weaponSet();

		public AnythingSet AnythingSet = new AnythingSet();

		public actorSet actorSet = new actorSet();

		public actorAttributeSet actorAttributeSet = new actorAttributeSet();

		public SoundSet SoundSet = new SoundSet();

		public levelSet levelSet = new levelSet();

		public RewardItemSet RewardItemSet = new RewardItemSet();

		public itemSet itemSet = new itemSet();


		public void loadAll(byte[] data)
		{  
			DataStream dis = new DataStream(new MemoryStream(data)); 
    		while (dis.Available() > 0)
            {
                bool isProcess = false;

                int id = dis.ReadInt();
                int size = dis.ReadInt();
                int rowCount = dis.ReadInt();

 				if (id==0)
                {
                    dis.Skip(size);
                    continue;
                }
                else if (id == 87213260)
                {
					LanguageSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 421295507)
                {
					bulletSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 859927194)
                {
					weaponSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 700424081)
                {
					AnythingSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 1490990920)
                {
					actorSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 709552008)
                {
					actorAttributeSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 407425029)
                {
					SoundSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 1829605692)
                {
					levelSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 769485369)
                {
					RewardItemSet.Load(dis,rowCount);
                    isProcess = true;
                }
                else if (id == 55452937)
                {
					itemSet.Load(dis,rowCount);
                    isProcess = true;
                }

                if (!isProcess)
                {
                    dis.Skip(size);
                }
            } 
            dis.Close();
		}

	   private ConfigsManager()
	   {
	   }

	   public static readonly ConfigsManager Instance = new ConfigsManager();

	}
}

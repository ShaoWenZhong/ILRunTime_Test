
/**
 * Auto generated, do not edit it
 */
using Data.Beans;

namespace Data.Containers
{
	public class GameDataManager
	{

		public t_soldierContainer t_soldierContainer = new t_soldierContainer();


		public void loadAll()
		{

		t_soldierContainer.loadDataFromBin();

		}

	   private GameDataManager()
	   {
	   }

	   public static readonly GameDataManager Instance = new GameDataManager();

	}
}

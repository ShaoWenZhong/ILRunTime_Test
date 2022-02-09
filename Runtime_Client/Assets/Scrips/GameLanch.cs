using System.Collections;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

public class GameLanch : Singleton_Mono<GameLanch>
{
    void Awake()
    {
        GameObject go = GameObject.Find("DontDestroy");
        if (go != null)
            DontDestroyOnLoad(go);

        //初始化游戏框架，资源管理，网络管理，日志。。。。。
        this.gameObject.AddComponent<ResManager>();
        this.gameObject.AddComponent<ILRuntimeManager>();
        this.gameObject.AddComponent<TimerManager>();
        this.gameObject.AddComponent<CameraManager>();
        CheckHotDownLoad();
    }

    //addressable 预下载
    private void CheckHotDownLoad()
    {
        GameObject canva = GameObject.Find("Canvas");
        Object updateAsset = Resources.Load("UIPanel/PreUpdate/PreUpdateView");
        GameObject panel = GameObject.Instantiate(updateAsset) as GameObject;
        panel.transform.SetParent(canva.transform);
        panel.AddComponent<PreUpdateView>();
    }

    public void StartLoadHotCore(GameObject panel)
    {
        this.StartCoroutine(this.CheckHotUpdate());
        GameObject.DestroyImmediate(panel);
    }

    IEnumerator CheckHotUpdate()
    {
        //服务器上下载最新的ab包
        //
        //热更代码已经打成bundler包进行加载

#if UNITY_ANDROID
        //Task<byte[]> task = AddressableManager.Instance.LoadFile_Addressable("Hot_FixDLL");

        //WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");

        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
#else
        //WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
        //Task<byte[]> task = AddressableManager.Instance.LoadFile_Addressable("Hot_FixDLL");
#endif
        //while (!task.IsCompleted)
        //    yield return null;
        //byte[] dll = task.Result;

        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if UNITY_ANDROID
        //www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
        //Task<byte[]> task_pdb = AddressableManager.Instance.LoadFile_Addressable("Hot_FixPDB");

        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.pdb");

#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.pdb");
        //Task<byte[]> task_pdb = AddressableManager.Instance.LoadFile_Addressable("Hot_FixPDB");
#endif

        //while (!task_pdb.IsCompleted)
        //    yield return null;
        //byte[] pdb = task_pdb.Result;

        while (!www.isDone)
           yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        www.Dispose();

        ILRuntimeManager.Instance.LoadHotFixAssembly(dll,pdb);
        ILRuntimeManager.Instance.EnterGame();
        yield break;
    }

    public void Test()
    {
        Debug.Log("test");
    }

}

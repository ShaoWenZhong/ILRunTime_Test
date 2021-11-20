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

        //��ʼ����Ϸ��ܣ���Դ�������������־����������
        this.gameObject.AddComponent<ResManager>();
        this.gameObject.AddComponent<ILRuntimeManager>();
        this.gameObject.AddComponent<TimerManager>();
        this.gameObject.AddComponent<CameraManager>();
        CheckHotDownLoad();
    }

    //addressable Ԥ����
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
        //���������������µ�ab��
        //
        //�ȸ������Ѿ����bundler�����м���

#if UNITY_ANDROID
        Task<byte[]> task = AddressableManager.Instance.LoadFile_Addressable("Hot_FixDLL");
        //WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
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

        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��������LoadAssembly��ʱ��pdb��null����
#if UNITY_ANDROID
        //www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
        Task<byte[]> task_pdb = AddressableManager.Instance.LoadFile_Addressable("Hot_FixPDB");
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

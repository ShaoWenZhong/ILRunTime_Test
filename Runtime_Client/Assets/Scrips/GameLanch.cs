using System.Collections;
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
        this.StartCoroutine(this.CheckHotUpdate());
    }

    IEnumerator CheckHotUpdate()
    {
        //���������������µ�ab��

        //
#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB�ļ��ǵ������ݿ⣬����Ҫ����־����ʾ������кţ�������ṩPDB�ļ����������ڻ��������ڴ棬��ʽ����ʱ�뽫PDBȥ��������LoadAssembly��ʱ��pdb��null����
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.pdb");
#endif
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

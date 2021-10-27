using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ILRuntime.Runtime.Enviorment;

public class ILRuntimeManager : Singleton_Mono<ILRuntimeManager>
{
    ILRuntime.Runtime.Enviorment.AppDomain domain;
    MemoryStream fs;
    MemoryStream p;
    private bool isStart = false;


    private void Awake()
    {
        isStart = false;
        domain = new ILRuntime.Runtime.Enviorment.AppDomain();
    }

    public void LoadHotFixAssembly(byte[] dll,byte[] pdb)
    {
        this.fs = new MemoryStream(dll);
        this.p = new MemoryStream(pdb);

        try
        {
            //this.domain = new ILRuntime.Runtime.Enviorment.AppDomain();
            this.domain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch (System.Exception)
        {
            Debug.LogError("�����ȸ�DLLʧ�ܣ���ȷ���Ѿ�ͨ��VS��Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln������ȸ�DLL");
        }

        InitializeILRuntime();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
        domain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //������һЩILRuntime��ע�ᣬHelloWorldʾ����ʱû����Ҫע���
    }

    /// <summary>
    /// �����ȸ�����
    /// </summary>
    public void EnterGame()
    {
        isStart = true;
        domain.Invoke("HotFix_Project.Scrips.Main", "Init", null, null);
        TimerManager.Instance.doOnce(1000, timerFun);
    }

    private void timerFun()
    {
        Debug.Log("timerFun");
    }

    private void Update()
    {
        if(isStart)
            domain.Invoke("HotFix_Project.Scrips.Main", "Update", null, null);
    }
    private void LateUpdate()
    {
        if (isStart)
            domain.Invoke("HotFix_Project.Scrips.Main", "LateUpdate", null, null);
    }
    private void FixedUpdate()
    {
        if (isStart)
            domain.Invoke("HotFix_Project.Scrips.Main", "FixedUpdate", null, null);
    }


}

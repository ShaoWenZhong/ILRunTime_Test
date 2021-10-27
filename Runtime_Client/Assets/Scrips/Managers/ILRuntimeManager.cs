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
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
        }

        InitializeILRuntime();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        domain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
    }

    /// <summary>
    /// 进入热更代码
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

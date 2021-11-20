using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using System;

public class ILRuntimeManager : Singleton_Mono<ILRuntimeManager>
{
    public ILRuntime.Runtime.Enviorment.AppDomain domain;
    MemoryStream fs;
    MemoryStream p;
    private bool isStart = false;


    private void Awake()
    {
        isStart = false;
        domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        RegistDelete();
    }

    void RegistDelete()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        //appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
        //TestDelegateMethod, 这个委托类型为有个参数为int的方法，注册仅需要注册不同的参数搭配即可
        domain.DelegateManager.RegisterMethodDelegate<int>();
        //带返回值的委托的话需要用RegisterFunctionDelegate，返回类型为最后一个
        domain.DelegateManager.RegisterFunctionDelegate<int, string>();
        //Action<string> 的参数为一个string
        domain.DelegateManager.RegisterMethodDelegate<string>();

        domain.DelegateManager.RegisterMethodDelegate<GameObject>();
        domain.DelegateManager.RegisterMethodDelegate<UnityEngine.Object>();

        //ILRuntime内部是用Action和Func这两个系统内置的委托类型来创建实例的，所以其他的委托类型都需要写转换器
        //将Action或者Func转换成目标委托类型

        domain.DelegateManager.RegisterDelegateConvertor<TestDelegateMethod>((action) =>
        {
            //转换器的目的是把Action或者Func转换成正确的类型，这里则是把Action<int>转换成TestDelegateMethod
            return new TestDelegateMethod((a) =>
            {
                //调用委托实例
                ((System.Action<int>)action)(a);
            });
        });
        //对于TestDelegateFunction同理，只是是将Func<int, string>转换成TestDelegateFunction
        domain.DelegateManager.RegisterDelegateConvertor<TestDelegateFunction>((action) =>
        {
            return new TestDelegateFunction((a) =>
            {
                return ((System.Func<int, string>)action)(a);
            });
        });

        //下面再举一个这个Demo中没有用到，但是UGUI经常遇到的一个委托，例如UnityAction<float>
        domain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<float>>((action) =>
        {
            return new UnityEngine.Events.UnityAction<float>((a) =>
            {
                ((System.Action<float>)action)(a);
            });
        });


        //注册UGUI事件
        domain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((action) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((System.Action)action)();
            });
        });
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
            domain.Invoke("HotFix_Project.Scrips.Main", "Update", null, Time.deltaTime);
    }
    private void LateUpdate()
    {
        if (isStart)
            domain.Invoke("HotFix_Project.Scrips.Main", "LateUpdate", null, Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (isStart)
            domain.Invoke("HotFix_Project.Scrips.Main", "FixedUpdate", null, Time.deltaTime);
    }


}

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
        //����Unity��Profiler�ӿ�ֻ���������߳�ʹ�ã�Ϊ�˱�����쳣����Ҫ����ILRuntime���̵߳��߳�ID������ȷ���������к�ʱ�����Profiler
        //appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //������һЩILRuntime��ע��
        //TestDelegateMethod, ���ί������Ϊ�и�����Ϊint�ķ�����ע�����Ҫע�᲻ͬ�Ĳ������伴��
        domain.DelegateManager.RegisterMethodDelegate<int>();
        //������ֵ��ί�еĻ���Ҫ��RegisterFunctionDelegate����������Ϊ���һ��
        domain.DelegateManager.RegisterFunctionDelegate<int, string>();
        //Action<string> �Ĳ���Ϊһ��string
        domain.DelegateManager.RegisterMethodDelegate<string>();

        domain.DelegateManager.RegisterMethodDelegate<GameObject>();
        domain.DelegateManager.RegisterMethodDelegate<UnityEngine.Object>();

        //ILRuntime�ڲ�����Action��Func������ϵͳ���õ�ί������������ʵ���ģ�����������ί�����Ͷ���Ҫдת����
        //��Action����Funcת����Ŀ��ί������

        domain.DelegateManager.RegisterDelegateConvertor<TestDelegateMethod>((action) =>
        {
            //ת������Ŀ���ǰ�Action����Funcת������ȷ�����ͣ��������ǰ�Action<int>ת����TestDelegateMethod
            return new TestDelegateMethod((a) =>
            {
                //����ί��ʵ��
                ((System.Action<int>)action)(a);
            });
        });
        //����TestDelegateFunctionͬ��ֻ���ǽ�Func<int, string>ת����TestDelegateFunction
        domain.DelegateManager.RegisterDelegateConvertor<TestDelegateFunction>((action) =>
        {
            return new TestDelegateFunction((a) =>
            {
                return ((System.Func<int, string>)action)(a);
            });
        });

        //�����پ�һ�����Demo��û���õ�������UGUI����������һ��ί�У�����UnityAction<float>
        domain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<float>>((action) =>
        {
            return new UnityEngine.Events.UnityAction<float>((a) =>
            {
                ((System.Action<float>)action)(a);
            });
        });


        //ע��UGUI�¼�
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

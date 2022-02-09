using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;
using System;

public class PreUpdateView : MonoBehaviour
{
    private Transform panel;
    private Slider slider;
    private Button loginBtn;
    private Text tips;

    private AsyncOperationHandle downloadDependencies;

    private void Awake()
    {
        panel = gameObject.transform;
        slider = CustomUtil.GetChild<Slider>(gameObject,"Slider");
        tips = CustomUtil.GetChild<Text>(gameObject,"tips");
        loginBtn = CustomUtil.GetChild<Button>(gameObject, "loginBtn");
        loginBtn.onClick.AddListener(delegate() {OnLoginClick(); });
        loginBtn.gameObject.SetActive(false);
        slider.value = 0;
        tips.text = "���¼����.......";
        DebugUtil.Log("PreUpdateView awake");
        StartCoroutine(SatrtPreLoad());
    }


    // Start is called before the first frame update
    void Start()
    {
        //SatrtPreLoad();
    }

    private void OnLoginClick()
    {
        GameLanch.Instance.StartLoadHotCore(this.gameObject);
    }

    private IEnumerator SatrtPreLoad()
    {
        DebugUtil.Log("����Ԥ����");
        Addressables.InitializeAsync();
        Caching.ClearCache();

        string label = QualityManager.Instance.GetQualityLableKey();
        //�����´�С
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(label);
        yield return getDownloadSize;



        DebugUtil.Log("getDownloadSize quality label:" + label + getDownloadSize.Result);
        if (getDownloadSize.Result > 0)
        {
            downloadDependencies = Addressables.DownloadDependenciesAsync(label);
            slider.gameObject.SetActive(true);
            yield return downloadDependencies;
        }

        //List<string> keyList = Addressables.();
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("default");
        yield return handle;
        DateTime startTime = DateTime.Now;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(string.Format("��ͨ��Labels��ȡKeys�ɹ���������{0} ��ʱ��{1:0.00}S", handle.Result.Count, (DateTime.Now - startTime).Seconds));
            List<object> keys = new List<object>();
            foreach (var item in handle.Result)
            {
                //keys.Add(item.PrimaryKey);
                Debug.Log(item.PrimaryKey);
            }
            //keysCallback?.Invoke(keys);
            //statusCallback?.Invoke(AsyncOperationStatus.Succeeded);
        }
        else
        {
            Debug.LogError(string.Format("����Labels��ȡKeysʧ�ܡ� ��ʱ��{0:0.00}S", (DateTime.Now - startTime).Seconds));
            //statusCallback?.Invoke(AsyncOperationStatus.Failed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (downloadDependencies.IsValid())
        {
            DebugUtil.Log(downloadDependencies.GetDownloadStatus().Percent.ToString());
            if (downloadDependencies.GetDownloadStatus().Percent < 1)
            {
                UpdateProcessBar(downloadDependencies.GetDownloadStatus().Percent);
            }
            else if (downloadDependencies.IsDone)
            {
                UpdateProcessBar(downloadDependencies.GetDownloadStatus().Percent);
                Addressables.Release(downloadDependencies);
                tips.text = "��ɸ���";
                loginBtn.gameObject.SetActive(true);
            }
        }
    }


    void UpdateProcessBar(float percent)
    {
        slider.value = percent;
        tips.text = "������" + Mathf.CeilToInt(percent * 100f) + "%...........";
    }
}

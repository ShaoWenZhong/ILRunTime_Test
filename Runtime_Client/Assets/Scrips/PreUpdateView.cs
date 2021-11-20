using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

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
        tips.text = "更新检测中.......";
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
        DebugUtil.Log("开启预下载");
        Addressables.InitializeAsync();
        Caching.ClearCache();

        string label = QualityManager.Instance.GetQualityLableKey();
        //检测更新大小
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(label);
        yield return getDownloadSize;

        DebugUtil.Log("getDownloadSize quality label:" + label + getDownloadSize.Result);
        if (getDownloadSize.Result > 0)
        {
            downloadDependencies = Addressables.DownloadDependenciesAsync(label);
            slider.gameObject.SetActive(true);
            yield return downloadDependencies;
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
                tips.text = "完成更新";
                loginBtn.gameObject.SetActive(true);
            }
        }
    }


    void UpdateProcessBar(float percent)
    {
        slider.value = percent;
        tips.text = "下载中" + Mathf.CeilToInt(percent * 100f) + "%...........";
    }
}

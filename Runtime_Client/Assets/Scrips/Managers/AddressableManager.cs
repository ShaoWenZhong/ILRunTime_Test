using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class AddressableManager:Singleton_Mono<AddressableManager>
{

    private List<UnityEngine.Object> objects = new List<UnityEngine.Object>(); 

    public byte[] LoadFild(string path)
    {
        DebugUtil.Log("LoadFile:" + path);
        return System.IO.File.ReadAllBytes(path);
    }

    public async Task<byte[]> LoadFile_Addressable(string path)
    {
        DebugUtil.Log("LoadFile_Addressable:" + path);
        var text = await Addressables.LoadAssetAsync<TextAsset>(path).Task;
        return text.bytes;
    }

    public async Task<byte[]> LoadFile_WebRequest(string path)
    {
        DebugUtil.Log("LoadFile_WebRequest:" + path);
        var request = UnityWebRequest.Get("file:///" + path);
        request.SendWebRequest();
        while (!request.isDone)
            await Task.Delay(200);
        if (!string.IsNullOrEmpty(request.error))
            DebugUtil.LogError(request.error);
        byte[] bytes = request.downloadHandler.data;
        request.Dispose();
        return bytes;
    }

    IEnumerator DownLoadScene(int sceneId)
    {
        DebugUtil.Log("DownLoadScene"+sceneId);
        string path = URLManager.GetScenePath(sceneId);
        var handler = Addressables.DownloadDependenciesAsync(path);
        handler.Completed += OnSceneDownLoadComplete;
        while (!handler.IsDone)
        {
            var status = handler.GetDownloadStatus();
            float progress = status.Percent;
            yield return null;
        }
    }

    private void OnSceneDownLoadComplete(AsyncOperationHandle obj)
    {
        
    }

    public async Task<UnityEngine.Object> LoadGameObject(string path,Action<UnityEngine.Object> action)
    {
        DebugUtil.Log("LoadGameObject_Addressable:" + path);
        UnityEngine.Object obj = await Addressables.LoadAssetAsync<UnityEngine.Object>(path).Task;
        if (action != null)
            action(obj);
        return obj; 
    }

    public async Task<Texture> LoadTexture(string path, Action<Texture> action)
    {
        DebugUtil.Log("LoadTexture_Addressable:" + path);
        Texture obj = await Addressables.LoadAssetAsync<Texture>(path).Task;
        if (action != null)
            action(obj);
        return obj;
    }

    public async Task<TObject> LoadAsset<TObject>(string path,Action<TObject> action)
    {
        DebugUtil.Log("LoadAsset:" + path);
        TObject obj = await Addressables.LoadAssetAsync<TObject>(path).Task;
        if (action != null)
            action(obj);
        return obj;
    }

    public void Release<TObject>(TObject obj)
    {
        Addressables.Release(obj);
    }

    public void LoadAssetAsync<TObject>(string bundleName,Action<TObject> action)
    {
        //DebugUtil.Log("LoadAssetAsync:" + bundleName);
        Addressables.LoadAssetAsync<TObject>(bundleName).Completed += new Action<AsyncOperationHandle<TObject>>((handle) =>
        {
            action(handle.Result);
        });
    }
}

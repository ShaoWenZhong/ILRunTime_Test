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
        //GameObject go = GameObject.Instantiate(obj) as GameObject;
        if (action != null)
            action(obj);
        return obj; 
    }

    private void action(AsyncOperationHandle<GameObject> obj)
    {
        throw new NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ILRuntime dll 打包
/// </summary>
public class ILRuntimeDllBundleBuild : EditorWindow
{

    static string hotFileName = "HotFix_Project";

    [MenuItem("Window/Asset Management/Build Dll")]
    public static void Build()
    {
        DllToBytes();
    }

    private static void DllToBytes()
    {
        string folderPath;
        //folderPath = EditorUtility.OpenFilePanel("选择DLL所在文件夹", PathManager.hotDll, string.Empty);
        folderPath = PathManager.hotDll;
        if (string.IsNullOrEmpty(folderPath))
            return;
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        if (directoryInfo == null)
            return;

        FileInfo[] fileInfos = directoryInfo.GetFiles();
        List<FileInfo> dllList = new List<FileInfo>();
        List<FileInfo> pdbList = new List<FileInfo>();

        //不打包多余的dll
        for (int i = 0; i < fileInfos.Length; i++)
        {
            //DebugUtil.Log(fileInfos[i].Name);
            //if (fileInfos[i].Name == hotFileName && fileInfos[i].Extension == ".dll")
            //    dllList.Add(fileInfos[i]);
            //else if (fileInfos[i].Name == hotFileName && fileInfos[i].Extension == ".pdb")
            //    pdbList.Add(fileInfos[i]);

            if (fileInfos[i].Name == hotFileName + ".dll")
                dllList.Add(fileInfos[i]);
            else if (fileInfos[i].Name == hotFileName + ".pdb")
                pdbList.Add(fileInfos[i]);
        }

        if (dllList.Count + pdbList.Count <= 0)
        {
            Debug.LogError("文件夹之下没有dll文件");
            return;
        }
        else
        {
            Debug.Log("dll打包路径：" + folderPath);
        }

        string savePath = PathManager.willBuildDll;
        //savePath = EditorUtility.OpenFilePanel("选择DLL转换后保存的文件夹", PathManager.willBuildDll, string.Empty);
        if (string.IsNullOrEmpty(savePath))
            return;

        Debug.Log("开始转换DLL文件");
        string path = string.Empty;
        for (int i = 0; i < dllList.Count; i++)
        {
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(dllList[i].Name)}_dll_res.bytes";
            BytesTofile(path, FileToBytes(dllList[i]));
        }

        Debug.Log("DLL 文件转换结束");
        Debug.Log("开始转换PDB文件 pdbList.Count" + pdbList.Count);

        for (int i = 0; i < pdbList.Count; i++)
        {
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(pdbList[i].Name)}_pdb_res.bytes";
            BytesTofile(path, FileToBytes(pdbList[i]));
        }

        Debug.Log("PDB 文件转换结束");
        Debug.Log("到处路径为："+ savePath);

        AssetDatabase.Refresh();
    }

    private static byte[] FileToBytes(FileInfo fileInfo)
    {
        return File.ReadAllBytes(fileInfo.FullName);
    }

    private static void BytesTofile(string path, byte[] bytes)
    {
        Debug.Log($"Path:{path}\nlength:{bytes.Length}");
        File.WriteAllBytes(path, bytes);
    }

}

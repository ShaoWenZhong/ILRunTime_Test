using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ILRuntime dll ���
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
        //folderPath = EditorUtility.OpenFilePanel("ѡ��DLL�����ļ���", PathManager.hotDll, string.Empty);
        folderPath = PathManager.hotDll;
        if (string.IsNullOrEmpty(folderPath))
            return;
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        if (directoryInfo == null)
            return;

        FileInfo[] fileInfos = directoryInfo.GetFiles();
        List<FileInfo> dllList = new List<FileInfo>();
        List<FileInfo> pdbList = new List<FileInfo>();

        //����������dll
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
            Debug.LogError("�ļ���֮��û��dll�ļ�");
            return;
        }
        else
        {
            Debug.Log("dll���·����" + folderPath);
        }

        string savePath = PathManager.willBuildDll;
        //savePath = EditorUtility.OpenFilePanel("ѡ��DLLת���󱣴���ļ���", PathManager.willBuildDll, string.Empty);
        if (string.IsNullOrEmpty(savePath))
            return;

        Debug.Log("��ʼת��DLL�ļ�");
        string path = string.Empty;
        for (int i = 0; i < dllList.Count; i++)
        {
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(dllList[i].Name)}_dll_res.bytes";
            BytesTofile(path, FileToBytes(dllList[i]));
        }

        Debug.Log("DLL �ļ�ת������");
        Debug.Log("��ʼת��PDB�ļ� pdbList.Count" + pdbList.Count);

        for (int i = 0; i < pdbList.Count; i++)
        {
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(pdbList[i].Name)}_pdb_res.bytes";
            BytesTofile(path, FileToBytes(pdbList[i]));
        }

        Debug.Log("PDB �ļ�ת������");
        Debug.Log("����·��Ϊ��"+ savePath);

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

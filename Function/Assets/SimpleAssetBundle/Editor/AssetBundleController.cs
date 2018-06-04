using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;


//测试打包
public class AssetBundleController : EditorWindow {


    //输出目录
    public static string OutDataPath = Application.dataPath + "/" + "SimpleAssetBundle/OutData";

    //资源目录
    public static string ResourcePath = Application.dataPath + "/SimpleAssetBundle/" + "Resources";

    //文件的描述
    public static string DescriptionName = "FileDescription.txt";

    //依赖的描述文件
    public static string DependencyXML = "DependencyXML.xml";


    //存储每一个文件
    public static Dictionary<string, AssetBundleInfo> assetBundleFiles = new Dictionary<string, AssetBundleInfo>();


    [MenuItem("Test/BundleAsset")]
    public static void BundleAssetBundle()
    {

        ClearAssetBundleName();
        AssetDatabase.RemoveUnusedAssetBundleNames();

        //取得所有文件夹
        string[] dirPaths = GetAllDir(ResourcePath,"*");
        for (int i = 0; i < dirPaths.Length;i++)
        {
            //遍历每一个文件夹里面的文件，取得最终父级，设置assetBundle
            SetAssetBundleName(dirPaths[i]);
        }
        AssetDatabase.Refresh();
        BuildPipeline.BuildAssetBundles(OutDataPath,BuildAssetBundleOptions.UncompressedAssetBundle,BuildTarget.Android);

        //设置文件的描述，用来检测那些文件需要更新
        SetFileDescription();
       
        //生成依赖关系
        SetDependency(OutDataPath+"/OutData");

        //生成XML文档，assetBundle对应的资源路径
        XMLFile xMLFile = GenerateDependencyXML();

        //写入XML文档
        File.WriteAllBytes(OutDataPath+"/"+DependencyXML,xMLFile.GetBytes());

        Debug.Log("成功");

    }


    //设置依赖关系
    public static void SetDependency(string mainAssetbundle){

        AssetBundle assetBundle = AssetBundle.LoadFromFile(mainAssetbundle);
        AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] bundles = assetBundleManifest.GetAllAssetBundles();

        for (int i = 0; i < bundles.Length; i++)
        {
            AssetBundleInfo assetBundleInfo = GetAssetBundleInfo(bundles[i]);
            if (assetBundleInfo == null) continue;
            string[] dependencs = assetBundleManifest.GetAllDependencies(bundles[i]);
            for (int j = 0; j < dependencs.Length; j++)
            {
                assetBundleInfo.assetDependency.Add(dependencs[j]);

            }

        }
    }

    //生成依赖关系的XML文档
    public static XMLFile GenerateDependencyXML()
    {

        XMLFile xMLFile = new XMLFile();
        xMLFile.StartNode("files");


        foreach(AssetBundleInfo a in assetBundleFiles.Values){

            xMLFile.StartNode("file");
            xMLFile.CreateElement("bundleName",a.assetBundleName);
            xMLFile.CreateElement("filePath",a.filePath);
            if(a.assetDependency!=null && a.assetDependency.Count>0)
            {
                xMLFile.StartNode("deps");
            for (int i = 0; i < a.assetDependency.Count; i++)
               {

                    xMLFile.CreateElement("dep", a.assetDependency[i]);
               }
                xMLFile.EndNode("deps");

            }
            xMLFile.EndNode("file");

        }

        xMLFile.EndNode("files");
        return xMLFile;
    }


    //获取指定的AssetBundleInfo
    public static AssetBundleInfo GetAssetBundleInfo(string bundleName)
    {
        foreach(AssetBundleInfo a in assetBundleFiles.Values){

            if(a.assetBundleName == bundleName){

                return a;
            }

        }
        return null;

    }


    //设置assetBundle的描述，生成md5值
    public static void SetFileDescription()
    {

        StringBuilder stringBuilder = new StringBuilder();
        string[] subFiles = Directory.GetFiles(OutDataPath);
        for (int i = 0; i < subFiles.Length;i++){

            string filePath = subFiles[i];
            string ext = Path.GetExtension(filePath);
            if (ext == ".meta" || ext == ".manifest") continue;
            FileStream fStream = File.Open(filePath, FileMode.Open);
            long size = fStream.Length;
            string md5 = GetMd5Hash(fStream);
            string fileName = Path.GetFileName(filePath);
            fStream.Close();

            string fileData = fileName + "|" + size + "|" + md5+"\n";
            stringBuilder.Append(fileData);

        }

        File.WriteAllText(OutDataPath+"/"+DescriptionName,stringBuilder.ToString());


    }

    public static string  GetMd5Hash(FileStream fileStream){

        MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
        byte[] hash = mD5.ComputeHash(fileStream);
        StringBuilder hashStr = new StringBuilder();
        for (int j = 0; j < hash.Length; j++)
        {
            hashStr.Append(hash[j]);

        }
        return hashStr.ToString();

    }

    //设置AssetBundle的标识，每一个文件夹为一个assetBundle，生成对应的路径表文件，用于读取assetBundle下的资源
    public static void SetAssetBundleName(string dirPath)
    {
 

        string[] filePaths = Directory.GetFiles(dirPath);
        int index = ResourcePath.LastIndexOf("/", System.StringComparison.Ordinal);
        string tempDirName = ResourcePath.Substring(index+1);

        for (int i = 0; i < filePaths.Length;i++)
        {
            string filePath = filePaths[i];

     
            string ext = Path.GetExtension(filePath);
            if (ext == ".meta") continue;
     

            int subIndex = filePath.IndexOf(tempDirName,System.StringComparison.Ordinal);
            string tempPath = filePath.Substring(subIndex+tempDirName.Length+1);
            //Debug.Log(tempPath);

            int nameIndex = tempPath.IndexOf("/", System.StringComparison.Ordinal);


            //文件夹名，作为Assetbundle的名称
            string assetName = tempPath.Substring(0,nameIndex);


            string loadPath = filePath.Replace(Application.dataPath, "");
            loadPath = "Assets" + loadPath;
      
            AssetImporter assetBundle = AssetImporter.GetAtPath(loadPath);

            if(assetBundle != null){

                assetBundle.assetBundleName = assetName;
                AssetBundleInfo assetBundleInfo = new AssetBundleInfo(assetName,tempPath);
                string name = Path.GetExtension(tempPath);
                name = tempPath.Replace(name,"");
                assetBundleFiles.Add(name,assetBundleInfo);

            }

        }

    }

    //清空所用AssetBundle的名称
    public static void ClearAssetBundleName()
    {
        string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < bundleNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(bundleNames[i], true);
        }
    }

    //获取所有的目录
    public static string[] GetAllDir(string path,string search)
    {

        string[] paths = Directory.GetDirectories(path, search, SearchOption.AllDirectories);

        return paths;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor : EditorWindow {


    [MenuItem("Test/TestZip")]
    public static void Test01(){

        string fileName = Application.dataPath + "/Scenes/ZIPCompress/OriginData/" + "AssetsT01.jpg";
        string outPath = Application.dataPath + "/Scenes/ZIPCompress/OutData/" + "AssetsT01.zip";
        ZipUtility.Instance.Zip(fileName, outPath, null, new TestCallBack());

    }

    [MenuItem("Test/TestZipDir")]
    public static void Test02(){

        string fileName = Application.dataPath + "/Scenes/ZIPCompress/OriginData";
        string outPath = Application.dataPath + "/Scenes/ZIPCompress/OutData/" + "OriginData.zip";
        ZipUtility.Instance.Zip(fileName, outPath, null, new TestCallBack());

    }

    [MenuItem("Test/UnZipTest")]
    public static void UnZipTest(){

        string fileName = Application.dataPath + "/Scenes/ZIPCompress/OutData/" + "OriginData.zip";
        string unZipPath = Application.dataPath + "/Scenes/ZIPCompress/UnZipData";
        ZipUtility.Instance.UnZip(fileName, unZipPath, null, new TestCallBack());
    }
}

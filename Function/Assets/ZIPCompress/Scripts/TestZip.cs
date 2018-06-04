using System.Collections;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;

public class TestCallBack:ZipCallBack{

	public override bool OnPreZip(ZipEntry zipEntry)
	{
        
        Debug.Log("压缩开始");
        return false;
	}

	public override bool OnAfterZip(ZipEntry zipEntry)
	{
        
        Debug.Log("压缩后");
        return false;
	}

	public override void OnFinish(bool isSure)
	{

        Debug.Log("压缩结束");

	}

}

public class TestZip : MonoBehaviour {



	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.A)){

            TestSingleFileZip();
        }
	}

	public void TestSingleFileZip(){

        string fileName = Application.dataPath + "/Scenes/ZIPCompress/OriginData/" + "AssetsT01.jpg";
        string outPath = Application.dataPath + "/Scenes/ZIPCompress/OutData/" + "AssetsT01.zip";
        ZipUtility.Instance.ZipFile(fileName,outPath,null,new TestCallBack());


    }

    public void TestUnZipFile(){

     
        string fileName = Application.dataPath + "/Scenes/ZIPCompress/OutData/" + "AssetsT01.zip";
        string unZipPath = Application.dataPath + "/Scenes/ZIPCompress/UnZipData";
        ZipUtility.Instance.UnZip(fileName, unZipPath, null, new TestCallBack());

    }

}

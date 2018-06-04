using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class MD5 : EditorWindow {

    [MenuItem("Test/MD5")]
    public static void Md5File(){

        string path = Application.dataPath + "/MD5Hash/Data/AssetsT02.jpg";
        FileStream fileStream = File.Create(path);
        MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
        byte[] mg5Bytes = mD5.ComputeHash(fileStream);

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < mg5Bytes.Length;i++){

            stringBuilder.Append(mg5Bytes[i]);
        }
        string path2 = Application.dataPath + "/MD5Hash/Data/AssetsT02.txt";

        File.WriteAllText(path2,stringBuilder.ToString());


    }

}

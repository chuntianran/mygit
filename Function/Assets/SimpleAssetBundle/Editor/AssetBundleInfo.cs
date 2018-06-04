using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleInfo  {

    //存储bundle的名称，以及对应的路径
    public string assetBundleName { get; set; }
    public string filePath { get; set; }
    public List<string> assetDependency { get; set; }

    public AssetBundleInfo(string assetBundleName,string filePath){

        this.assetBundleName = assetBundleName;
        this.filePath = filePath;
        assetDependency = new List<string>();

    }

}

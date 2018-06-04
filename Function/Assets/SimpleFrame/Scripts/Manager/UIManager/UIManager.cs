using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase {

    //单利
    public static UIManager _instance;

    //资源存储，负责UI存储
    private Dictionary<string, UIBehavior> resourceDic = new Dictionary<string, UIBehavior>();

    public void Awake()
	{
        _instance = this;
        managerId = ManagerId.UIManager;

	}

    public void AddResourceObj(string name,UIBehavior uIBehavior){

        if (resourceDic.ContainsKey(name)) return;
        resourceDic.Add(name,uIBehavior);
    }

    public void RemoveResourceObj(string name){

        if (!resourceDic.ContainsKey(name)) return;
        resourceDic.Remove(name);
    }

    //获取资源
    public UIBehavior GetResources(string name){

        if(!resourceDic.ContainsKey(name)){

            Debug.Log("该资源未注册");
            return null;
        }
        return resourceDic[name];

    }


    //发送信息
    public void SengMgs(MgsBase mgsBase){

        if (mgsBase == null) return;
        if (mgsBase.GetManager() == ManagerId.UIManager){

            //发送自己的
            ProcessEvent(mgsBase);

        }else{

            //转发到中心发送
            MgsCenter._instance.ProcessEvent(mgsBase);

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgsCenter : MonoBase
{
    //单利模式
    public static MgsCenter _instance;

    private Dictionary<ManagerId, MonoBase> manageDic = new Dictionary<ManagerId, MonoBase>();

	private void Awake()
	{
        _instance = this;
        this.gameObject.AddComponent<UIManager>();
        this.gameObject.AddComponent<NPCManager>();
        DontDestroyOnLoad(this);
	}

    //添加管理器
    public void AddManager(ManagerId manager,MonoBase monoBase){

        if (manageDic.ContainsKey(manager)) return;
        manageDic.Add(manager,monoBase);

    }

    //移除管理器
    public void RemoveManager(ManagerId manager){

        if (!manageDic.ContainsKey(manager)) return;
        manageDic.Remove(manager);
    }

    //负责转发消息，中转站
	public override void ProcessEvent(MgsBase mgsBase)
    {

        if (mgsBase == null) return;

        ManagerId managerId = mgsBase.GetManager();

        if(!manageDic.ContainsKey(managerId))
        {
            Debug.Log("未注册该管理器");
            return;
        }

        manageDic[managerId].ProcessEvent(mgsBase);
    }

}

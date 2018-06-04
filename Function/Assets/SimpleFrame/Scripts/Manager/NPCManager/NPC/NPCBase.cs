using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBase {

	public override void ProcessEvent(MgsBase mgsBase)
	{
        Debug.Log("从UIManager传来消息啦");
	}

    public void RegisterMgs(MonoBase monoBase, params ushort[] mgs)
    {

        NPCManager._instance.RegisterMgs(monoBase, mgs);
    }

    public void UnRegisterMgs(MonoBase monoBase, params ushort[] mgs)
    {

        NPCManager._instance.UnRegisterMgs(monoBase, mgs);
    }

    //发送消息
    public void SendMessage(MgsBase mgsBase)
    {

        NPCManager._instance.SengMgs(mgsBase);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBase {

	public override void ProcessEvent(MgsBase mgsBase)
	{
        
	}
    public void RegisterMgs(MonoBase monoBase,params ushort[] mgs){

        UIManager._instance.RegisterMgs(monoBase, mgs);
    }

    public void UnRegisterMgs(MonoBase monoBase,params ushort[] mgs){

        UIManager._instance.UnRegisterMgs(monoBase,mgs);
    }

    //发送消息
    public void SendMessage(MgsBase mgsBase){

        UIManager._instance.SengMgs(mgsBase);
    }


}

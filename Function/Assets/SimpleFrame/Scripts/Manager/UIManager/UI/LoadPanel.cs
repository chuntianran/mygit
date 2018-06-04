using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : UIBase {


    ushort[] mgs ={

        (ushort)TestEnum.Load

    };
	
	void Start () {

        RegisterMgs(this, mgs);
        UIManager._instance.GetResources("TestBtn").AddButtonListener(OnButtonClick);
	}

    public void OnButtonClick(){

        UIManager._instance.SengMgs(new MgsBase(((ushort)NPCEnum.Move)));
    }

	private void OnDestroy()
	{
        UnRegisterMgs(this,mgs);
	}

	public override void ProcessEvent(MgsBase mgsBase)
	{
        base.ProcessEvent(mgsBase);
        Debug.Log("物体传回来了消息");
	}

}

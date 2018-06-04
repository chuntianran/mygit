using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGroup : NPCBase {

    ushort[] mgs ={

        (ushort)NPCEnum.Move

    };

    void Start()
    {

        RegisterMgs(this, mgs);

    }

	public override void ProcessEvent(MgsBase mgsBase)
	{
        base.ProcessEvent(mgsBase);

        NPCManager._instance.GetResources("Player").Move();

        NPCManager._instance.SengMgs(new MgsBase((ushort)TestEnum.Load));
	}

	private void OnDestroy()
    {
        UnRegisterMgs(this, mgs);
    }
}

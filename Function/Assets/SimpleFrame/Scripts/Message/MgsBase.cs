using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgsBase  {

    //16位，4个字节，65536个信息，优化节点。
    public ushort mgsId;

    public MgsBase(ushort mgsId){

        this.mgsId = mgsId;
    }

    //获取相应的管理器
    public ManagerId GetManager(){

        int index = mgsId / FrameTool.mgsId;
        return (ManagerId)(index*FrameTool.mgsId);
    }

    //设置标识
    public void SetMgsId(ushort mgsId){

        this.mgsId = mgsId;
    }


}

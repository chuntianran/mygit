using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FrameTool {
    
    public const ushort mgsId = 3000;

}

//设置Manager的id，可以注册多个Manager
public enum ManagerId 
{
    //根据mgsId除以FrameTool，得到相应的Id
    UIManager = FrameTool.mgsId * 1,
    GameManager = FrameTool.mgsId * 2,
    NPCManager = FrameTool.mgsId * 3

}


public enum TestEnum
{

    Load = ManagerId.UIManager,
    Read,
    Have

}

public enum NPCEnum
{

    Move = ManagerId.NPCManager

}
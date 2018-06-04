using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ManagerBase : MonoBase
{
    //字典，存储信息
    private Dictionary<ushort, EventNode> MgsDictionary = new Dictionary<ushort, EventNode>();

    public ManagerId managerId;

    public virtual void Start(){
        
        MgsCenter._instance.AddManager(managerId, this);

    }

    //移除
    public virtual void OnDestroy()
	{
        MgsCenter._instance.RemoveManager(managerId);
	}

	//处理信息,通知其他模块，采用了观察者模式以及中介模式
	public override void ProcessEvent(MgsBase mgsBase)
    {
        //是否有注册该信息
        if(MgsDictionary.ContainsKey(mgsBase.mgsId)){
            
            EventNode eventNode = MgsDictionary[mgsBase.mgsId];

            while(eventNode != null){

                MonoBase monoBase = eventNode.GetData();
                if(monoBase != null){

                    monoBase.ProcessEvent(mgsBase);
                }
                eventNode = eventNode.GetNextNode();
            }
        }
    }

    //注册信息
    public void RegisterMgs(MonoBase mono,params ushort[] mgs){

        for (int i = 0; i < mgs.Length;i++){

            RegisterMgs(mgs[i],new EventNode(mono,null));
        }
    }

    //注册信息
    public void RegisterMgs(ushort key,EventNode eventNode){

        if(!MgsDictionary.ContainsKey(key)){

            MgsDictionary.Add(key,eventNode);

        }else{

            EventNode node = MgsDictionary[key];
            while(node.GetNextNode() != null){

                node = node.GetNextNode();
            }

            if(node != null && eventNode != node){
                node.SetNextNode(eventNode);
            }
        }

    }

    //获取相应的节点
    public EventNode GetNodeByKeyMono(ushort key,MonoBase monoBase){

        if (!MgsDictionary.ContainsKey(key)) return null;
        EventNode eventNode = MgsDictionary[key];

        while(eventNode != null){

            if(eventNode.GetData() == monoBase){

                return eventNode;
            }else{

                eventNode = eventNode.GetNextNode();
            }
        }
        return null;
    }

    //移除多个点
    public void UnRegisterMgs(MonoBase mono,params ushort[] mgs){

        for (int i = 0; i < mgs.Length;i++){

            UnRegisterMgs(mgs[i],mono);
        }
    }


    //移除某一个消息节点
    public void UnRegisterMgs(ushort key,MonoBase mono){
        
        EventNode eventNode = GetNodeByKeyMono(key,mono);
        if (eventNode == null){
            Debug.Log("查找不到该节点");
            return;
        }
        EventNode parent = eventNode.GetParentNode();
        EventNode nextNode = eventNode.GetNextNode();

        if(parent != null && nextNode != null){ //移除中间的节点

            parent.SetNextNode(nextNode);
            nextNode.SetParentNode(parent);
            eventNode.CleartNode();

        }
        else if(parent == null && nextNode != null){ //移除头节点

            eventNode.CleartNode();
            MgsDictionary.Remove(key);
            nextNode.SetParentNode(null);
            MgsDictionary.Add(key,nextNode);

        }else if(parent != null && nextNode == null){ //移除尾节点

            parent.SetNextNode(null);
            eventNode.CleartNode();

        }else{

            MgsDictionary.Remove(key);

        }

    }

    //取消注册消息
    public void UnRegisterMgs(ushort key){

        if(MgsDictionary.ContainsKey(key)){

            MgsDictionary.Remove(key);
        }
    }
}

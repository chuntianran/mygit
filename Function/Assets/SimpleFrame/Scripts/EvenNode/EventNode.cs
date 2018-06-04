using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//数据结构，使用链表数据结构，这样每一个Key，对应EventNode时，就可以有多个节点了
public class EventNode  {

    //数据
    private MonoBase data;

    //上一个节点
    private EventNode parent;

    //下一个节点
    private EventNode nextEvent;

    public EventNode(MonoBase data,EventNode eventNode){

        this.data = data;
        this.nextEvent = eventNode;

    }

    //获取该节点数据
    public MonoBase GetData(){

        return data;
    }

    //获取下一个节点
    public EventNode GetNextNode(){

        return nextEvent;
    }

    //设置下一个节点
    public void SetNextNode(EventNode eventNode){

        this.nextEvent = eventNode;

        if(eventNode != null)
        eventNode.parent = this;
    }

    //获取父类节点
    public EventNode GetParentNode(){

        return parent;
    }

    //设置父类节点
    public void SetParentNode(EventNode eventNode){

        this.parent = eventNode;
    }

    //清空信息
    public void CleartNode(){

        this.parent = null;
        this.nextEvent = null;
        this.data = null;
    }
}

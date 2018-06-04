using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {



    void Awake () {

        NPCManager._instance.AddResourceObj(this.name,this);
    }


    public void Move(){

        Debug.Log("我走走");
    }

}

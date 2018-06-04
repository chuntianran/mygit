using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {


    public GameObject hookGbj;

	private void Update()
	{

        if(Input.GetMouseButtonDown(0)){

            Vector3 worldPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.5f;
            GameObject hook = Instantiate(hookGbj,this.transform.position,Quaternion.identity);
            hook.GetComponent<Role>().destination = worldPos;
            hook.GetComponent<Role>().playerGbj = this.gameObject;
        }
	}

}

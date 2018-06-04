using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

	void Awake () {

        UIManager._instance.AddResourceObj(this.name,this);
	}
	

    public void AddButtonListener(UnityAction action){

        if(action != null){


           Button btn = this.GetComponent<Button>();
            if(btn != null){

                btn.onClick.AddListener(action);
            }
        }

    }

    public void RemoveButtonListener(){

         Button btn = this.GetComponent<Button>();
        if (btn != null)
        {

            btn.onClick.RemoveAllListeners();
        }
    }

}

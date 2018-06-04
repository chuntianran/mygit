using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class XMLFile {

    private StringBuilder stringBuilder;
	
    public XMLFile(){

        stringBuilder = new StringBuilder();
    }

    public void StartNode(string name){

        stringBuilder.AppendFormat("<{0}>", name);

    }

    public void CreateElement(string name,string values){

        stringBuilder.AppendFormat("<{0}>{1}</{0}>",name,values);

    }

    public void EndNode(string name){

        stringBuilder.AppendFormat("</{0}>",name);

    }

    public byte[] GetBytes(){

        return  Encoding.UTF8.GetBytes(stringBuilder.ToString());

    }

}

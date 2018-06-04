using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {

    public GameObject Player;
    private LineRenderer lineRenderer;

    public Vector3 destination;
    public float speed = 2;
    public float distance = 2;
    private GameObject lastNode;

    [HideInInspector]
    public GameObject playerGbj;

    public GameObject Node;

    private bool isEnd = false;


    private List<GameObject> lineTransform;
	// Use this for initialization
	void Start () {

        lineTransform = new List<GameObject>();
        lineRenderer = this.GetComponent<LineRenderer>();
        lastNode = this.gameObject;
        lineTransform.Add(this.gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed);

        if (this.transform.position != destination)
        {
            if (Vector3.Distance(lastNode.transform.position, playerGbj.transform.position) > distance)
            {

                CreateNode();

            }
        }
        else {
        
            if(!isEnd){

                isEnd = true;

                while(Vector3.Distance(lastNode.transform.position, playerGbj.transform.position) > distance){

                    CreateNode();
                }

                lastNode.GetComponent<HingeJoint>().connectedBody = playerGbj.GetComponent<Rigidbody>();

            }

        }

        PaintLine();
    }

    public void PaintLine(){

        lineRenderer.positionCount = lineTransform.Count+1;


        for (int i = 0; i < lineTransform.Count;i++){

            lineRenderer.SetPosition(i, lineTransform[i].transform.position);
        }

        lineRenderer.SetPosition(lineTransform.Count,playerGbj.transform.position);

    }

    public void CreateNode(){

        Vector3 dir = playerGbj.transform.position - lastNode.transform.position;
        dir.Normalize();
        dir *= distance;
        GameObject node =   Instantiate(Node,lastNode.transform.position+dir,Quaternion.identity);
        lineTransform.Add(node);

        node.transform.SetParent(transform);
        lastNode.GetComponent<HingeJoint>().connectedBody = node.GetComponent<Rigidbody>();
        lastNode = node;
    }
}

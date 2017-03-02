using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour {
    public GameObject lookAtObject;
    public GameObject distanceMeasure;
    public float turnDistance;
    private float distance;
    public string inputString;
    public bool done;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distance = (transform.position - distanceMeasure.transform.position).magnitude;
        if(done)
        {
            turnDistance = 0;
        }
        if (distance < turnDistance)
        {
            transform.LookAt(lookAtObject.transform.position);
            if(Input.GetAxis(inputString) > .8f)
            {
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().dialogue = GetComponent<Dialogue>();
            }
        }
        else
        {
            transform.LookAt(transform.position + (transform.position - lookAtObject.transform.position));
        }
	}
}

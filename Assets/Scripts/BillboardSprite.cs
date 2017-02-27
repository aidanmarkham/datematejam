using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour {
    public GameObject lookAtObject;
    public GameObject distanceMeasure;
    public float turnDistance;
    private float distance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distance = (transform.position - distanceMeasure.transform.position).magnitude;
        if (distance < turnDistance)
        {
            transform.LookAt(lookAtObject.transform.position);
        }
        else
        {
            transform.LookAt(transform.position + (transform.position - lookAtObject.transform.position));
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigCenter : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public Camera cam;
    public Vector3 targetPosition;
    public float speed;
    public float furthestDistance;
    public float minDistance;
    public float maxDistance;
    public float distanceScale;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        targetPosition = (player1.transform.position + player2.transform.position) / 2;

        transform.position += (targetPosition - transform.position) * speed * Time.deltaTime;

        
        
        furthestDistance = (player1.transform.position - transform.position).magnitude;

        furthestDistance = Mathf.Clamp(furthestDistance, minDistance, maxDistance);
        cam.transform.localPosition = new Vector3(0, furthestDistance * distanceScale, 0);
    }
}

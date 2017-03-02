using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFollowPlayerCenter : MonoBehaviour {
    public GameObject playerCenter;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Animator>().GetBool("Sink") == false){
            Vector3 pos = transform.position;
            pos.x = playerCenter.transform.position.x;
            pos.z = playerCenter.transform.position.z;
            transform.position = pos;
        }
    }
}

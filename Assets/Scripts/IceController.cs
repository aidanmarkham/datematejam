using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public float levelThreshold;

    public float distanceThreshold;


    private float playerDistance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float playerDistance = (player1.transform.position - player2.transform.position).magnitude;
		if(Mathf.Abs(player1.transform.position.y - player2.transform.position.y) < levelThreshold)
        {
            if (playerDistance < distanceThreshold)
            {
                if (player1.transform.position.y < .1f && player2.transform.position.y < .1f)
                {
                    GetComponent<Animator>().SetBool("Sink", true);
                }
            }
        }
	}
}

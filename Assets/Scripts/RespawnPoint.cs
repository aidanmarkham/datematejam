using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    public Transform playerOneRespawn;
    public Transform playerTwoRespawn;

    public GameObject ice;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Aidan" || other.name == "Kain")
        {
            GameObject.Find("Aidan").transform.position = playerOneRespawn.position;

            GameObject.Find("Kain").transform.position = playerTwoRespawn.position;
        }
        ice.GetComponent<Animator>().SetBool("Sink", false);
    }
}

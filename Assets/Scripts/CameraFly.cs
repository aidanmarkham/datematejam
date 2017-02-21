using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour {
    public float speed;
    public string horizontalAxis;
    public string verticalAxis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += GameObject.Find("Main Camera").transform.forward * Input.GetAxisRaw(verticalAxis) * speed * Time.deltaTime;
        transform.position += GameObject.Find("Main Camera").transform.right * Input.GetAxisRaw(horizontalAxis) * speed * Time.deltaTime;
    }
}

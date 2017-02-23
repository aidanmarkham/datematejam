using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterDriver : MonoBehaviour {
    public float moveSpeed;
    public GameObject cameraRig;
    public CharacterController characterController;
    private Quaternion lookDir;
    public string horizontalAxis;
    public string verticalAxis;
    private float speed;
    public float animSpeed;
    private Animator animator;
    public float gravity;
    
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        characterController.Move(cameraRig.transform.TransformDirection( new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis))) * moveSpeed * Time.deltaTime);
        speed = cameraRig.transform.TransformDirection(new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis))).magnitude * moveSpeed * Time.deltaTime;

        lookDir = Quaternion.LookRotation(cameraRig.transform.TransformDirection(new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis))));
        
        if(Input.GetAxis(horizontalAxis) != 0 || Input.GetAxis(verticalAxis) != 0)
        {
            transform.rotation = lookDir;
        }
        animator.SetFloat("PlayerSpeed", speed * animSpeed);
        animator.SetBool("IsWalking", speed * animSpeed > 0.1f);

        characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
    }
}

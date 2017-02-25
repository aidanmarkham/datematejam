using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterDriver : MonoBehaviour {
    public GameObject cameraRig;
    public bool isPlayer;

    [Space(10)]
    private CharacterController characterController;
    private Quaternion lookDir;

    [Header("Animation")]
    public float animSpeed;
    private Animator animator;
    [Space(10)]


    [Header("Movement & Controls")]
    public float moveSpeed;
    public string horizontalAxis;
    public string verticalAxis;
    private float speed;

    private Vector3 oldPosition;
    public float gravity;

    [Space(20)]
    [Header("Actor Things")]
    public GameObject[] nodes;
    public int currentNode;
    public float threshold;
    public bool playerWait;
    public GameObject player;
    public float distance;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (isPlayer)
        {
            //moves the character based on input;
            characterController.Move(cameraRig.transform.TransformDirection(new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis))) * moveSpeed * Time.deltaTime);
        }
        else
        {
            //do whatever non player characters do
            if (currentNode < nodes.Length)
            {
                if ((nodes[currentNode].transform.position - transform.position).magnitude > threshold )
                {
                    characterController.Move((nodes[currentNode].transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
                }
                else
                {
                    if (playerWait)
                    {
                        Debug.Log((transform.position - player.transform.position).magnitude + "<" + distance);
                        if ((transform.position - player.transform.position).magnitude < distance)
                        {
                            currentNode += 1;
                        }
                    }
                    else
                    {
                        currentNode += 1;
                    }
                    
                }
            }
        }

        //calculates movement speed;
        speed = (transform.position - oldPosition).magnitude;
        
        //sets face direction
        
        if(speed!=0)
        {
            lookDir = Quaternion.LookRotation(cameraRig.transform.TransformDirection((transform.position - oldPosition).normalized));
            transform.rotation = lookDir;
        }

        //updates animation states
        animator.SetFloat("PlayerSpeed", speed * animSpeed);
        animator.SetBool("IsWalking", speed * animSpeed > 0.1f);

        //applies gravity, updates oldposition
        characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
        oldPosition = transform.position;
    }
}

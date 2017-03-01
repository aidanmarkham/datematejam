using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterDriver : MonoBehaviour {
    public GameObject cameraRig;
    public GameObject center;
    public bool isPlayer;
    public GameObject otherPlayer;

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
    public string handHoldAxis;
    private float speed;

    private Vector3 oldPosition;
    public float gravity;

    public bool holdsLeftHand;
    public bool holdingHands;
    public float holdingHandsDistance;
    public float holdingHandsThreshold;
    private float handInputOld;
    


    [Space(20)]
    [Header("Actor Things")]
    public bool go;
    public GameObject[] nodes;
    public int currentNode;
    public float threshold;
    public bool playerWait;
    public GameObject player;
    public float distance;
    public Transform startCenter;

    [Space(20)]
    [Header("handHolding Things")]
    public bool handHoldController;
    public GameObject player1;
    public GameObject player2;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        if (isPlayer)
        {
            startCenter = center.transform;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (isPlayer)
        {
            //moves the character based on input;
            characterController.Move(cameraRig.transform.TransformDirection(new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis))) * moveSpeed * Time.deltaTime);

            #region handholding
            if (Input.GetAxis(handHoldAxis) == 1)
            {               
                if(Input.GetAxis(handHoldAxis)!=handInputOld)
                {
                    startCenter = center.transform;
                }
                holdingHands = false;
                Vector3 moveVector;
                Vector3 targetPosition;
                float distanceBetweenPlayers;
                if (holdsLeftHand)
                {
                    animator.SetBool("LHand", true);
                    targetPosition = startCenter.transform.position + startCenter.transform.right * holdingHandsDistance;
                    moveVector = targetPosition - transform.position;
                    distanceBetweenPlayers = moveVector.magnitude;
                }
                else
                {
                    animator.SetBool("RHand", true);
                    targetPosition = startCenter.transform.position - startCenter.transform.right * holdingHandsDistance;
                    moveVector = targetPosition  - transform.position;
                    distanceBetweenPlayers = moveVector.magnitude;
                    
                }



                /* code to move players together before snapping them onto the other controller
                if (distanceBetweenPlayers > holdingHandsThreshold)
                {
                    moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;
                    moveVector = Vector3.ClampMagnitude(moveVector, distanceBetweenPlayers);
                    characterController.Move(moveVector);
                    holdingHands = false;
                }
                else
                {
                    transform.position = targetPosition;
                    transform.rotation = startCenter.rotation;
                    holdingHands = true;
                }
                
                *///*
                if (distanceBetweenPlayers < holdingHandsThreshold && (otherPlayer.GetComponent<Animator>().GetBool("LHand") || otherPlayer.GetComponent<Animator>().GetBool("RHand")))
                {
                    transform.position = targetPosition;
                    transform.rotation = startCenter.rotation;
                    holdingHands = true;
                }
                //*/
            }
            else
            {
                animator.SetBool("LHand", false);
                animator.SetBool("RHand", false);
                holdingHands = false;
            }
            handInputOld = Input.GetAxis(handHoldAxis);

            #endregion

            //updates animation states
            animator.SetFloat("PlayerSpeed", speed * animSpeed);
            animator.SetBool("IsWalking", speed * animSpeed > 0.1f);
        }
        else if (handHoldController)
        {
            if(player1.GetComponent<CharacterDriver>().holdingHands && player2.GetComponent<CharacterDriver>().holdingHands)
            {
                float horizontal = (Input.GetAxis(player1.GetComponent<CharacterDriver>().horizontalAxis) + Input.GetAxis(player2.GetComponent<CharacterDriver>().horizontalAxis)) / 2;
                float vertical = (Input.GetAxis(player1.GetComponent<CharacterDriver>().verticalAxis) + Input.GetAxis(player2.GetComponent<CharacterDriver>().verticalAxis)) / 2;
                Debug.Log("Horizontal: " + horizontal + " Vertical: " + vertical);
                characterController.Move(cameraRig.transform.TransformDirection(new Vector3(horizontal, 0, vertical)) * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            //do whatever non player characters do
            //updates animation states
            animator.SetFloat("PlayerSpeed", speed * animSpeed);
            animator.SetBool("IsWalking", speed * animSpeed > 0.1f);
            if (currentNode < nodes.Length && go)
            {
                if ((nodes[currentNode].transform.position - transform.position).magnitude > threshold )
                {
                    characterController.Move((nodes[currentNode].transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
                }
                else
                {
                    if (playerWait)
                    {

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
        
        if(speed!=0 && !holdingHands && go)
        {
            lookDir = Quaternion.LookRotation(cameraRig.transform.TransformDirection((transform.position - oldPosition).normalized));
            transform.rotation = lookDir;
        }

        

        //applies gravity, updates oldposition
        characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
        oldPosition = transform.position;
    }
}

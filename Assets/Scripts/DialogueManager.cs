using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue lastDialogue;
    private int currentDialogue;

    public GameObject rightSide;
    public GameObject leftSide;
    public float characterOffset;
    public Vector3 direction;
    public float initialDistance;

    public string continueAxis;
    public Blur blur;

    public Vector3 startPosition;
    public Vector3 endPositionOffset;
    public float snapSpeed;


    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        




        if (dialogue != lastDialogue)
        {
            leftSide = dialogue.leftPlayers[0];
            rightSide = dialogue.rightPlayers[0];

            currentDialogue = 0;
            dialogue.sprite.enabled = false;

        }
        if (leftSide != null && rightSide != null)
        {
            rightSide.transform.position = transform.position + direction.normalized * initialDistance;


            leftSide.transform.position = transform.position - direction.normalized * initialDistance;
        }
        if (dialogue != null)
        {
            blur.enabled = true;
            transform.position += (startPosition - transform.position) * snapSpeed * Time.deltaTime;
            if (dialogue.dialogueBoxes[currentDialogue].active == false)
            {
                dialogue.dialogueBoxes[currentDialogue].SetActive(true);
                leftSide.GetComponent<Animator>().SetBool("Talking", dialogue.leftSideTalking[currentDialogue]);
                rightSide.GetComponent<Animator>().SetBool("Talking", !dialogue.leftSideTalking[currentDialogue]);
            }
            if (dialogue.texts[currentDialogue].GetComponent<TextTyper>().done)
            {
                leftSide.GetComponent<Animator>().SetBool("Talking", false);
                rightSide.GetComponent<Animator>().SetBool("Talking", false);
            }
            if (dialogue.texts[currentDialogue].GetComponent<TextTyper>().done && (Input.GetAxis(continueAxis) > .8f))
            {
                dialogue.dialogueBoxes[currentDialogue].SetActive(false);
                currentDialogue++;
            }
            if (currentDialogue == dialogue.texts.Length)
            {
                CharacterDriver cd = dialogue.goAfterDone as CharacterDriver;
                cd.go = true;
                dialogue = null;
            }

        }
        else
        {
            blur.enabled = false;

            transform.position += ((startPosition + endPositionOffset) - transform.position) * snapSpeed * Time.deltaTime;
        }

        lastDialogue = dialogue;
    }

    void AddDialogue(Dialogue d)
    {
        dialogue = d;
    }
}

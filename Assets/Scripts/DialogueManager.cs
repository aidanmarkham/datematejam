using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue lastDialogue;
    private int currentDialogue;
    
    public GameObject[] rightSide;
    public GameObject[] leftSide;
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
        
        for (int i = 0; i < rightSide.Length; i++)
        {
            rightSide[i].transform.position = transform.position + (direction.normalized * initialDistance + direction.normalized * characterOffset * i);
        }
        for (int i = 0; i < leftSide.Length; i++)
        {
            leftSide[i].transform.position = transform.position - (direction.normalized * initialDistance + direction.normalized * characterOffset * i);
        }
        


        if (dialogue != lastDialogue)
        {
            
            currentDialogue = 0;
            dialogue.sprite.enabled = false;

        }

        if (dialogue != null)
        {
            blur.enabled = true;
            transform.position += (startPosition - transform.position) * snapSpeed * Time.deltaTime;
            if (dialogue.dialogueBoxes[currentDialogue].active == false)
            {
                dialogue.dialogueBoxes[currentDialogue].SetActive(true);
                leftSide[0].GetComponent<Animator>().SetBool("Talking", dialogue.leftSideTalking[currentDialogue]);
                rightSide[0].GetComponent<Animator>().SetBool("Talking", !dialogue.leftSideTalking[currentDialogue]);
            }
            if (dialogue.texts[currentDialogue].GetComponent<TextTyper>().done)
            {
                leftSide[0].GetComponent<Animator>().SetBool("Talking", false);
                rightSide[0].GetComponent<Animator>().SetBool("Talking", false);
            }
            if (dialogue.texts[currentDialogue].GetComponent<TextTyper>().done && (Input.GetAxis(continueAxis + "_P1") > .8 || Input.GetAxis(continueAxis + "_P2") > .8))
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

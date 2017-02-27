using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class DialogueManager : MonoBehaviour
{
    public Queue<Dialogue> dialogues;
    public GameObject[] rightSide;
    public GameObject[] leftSide;
    public float characterOffset;
    public Vector3 direction;
    public float initialDistance;
    public Blur blur;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < rightSide.Length; i++)
        {
            rightSide[i].transform.position = transform.position + ( direction.normalized * initialDistance + direction.normalized * characterOffset * i);
        }
        for (int i = 0; i < leftSide.Length; i++)
        {
            rightSide[i].transform.position = transform.position - (direction.normalized * initialDistance + direction.normalized * characterOffset * i);
        }

        if(dialogues.Count > 0)
        {
            blur.enabled = true;
        }
        else
        {
            blur.enabled = false;
        }
    }

    void AddDialogue(Dialogue d)
    {
        dialogues.Enqueue(d);
    }
}

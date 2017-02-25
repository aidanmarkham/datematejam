using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    public GameObject[] rightSide;
    public GameObject[] leftSide;
    public float characterOffset;
    public Vector3 direction;
    public float initialDistance;
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
    }
}

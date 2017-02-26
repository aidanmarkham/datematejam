using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCenter : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject orb;
    public Light light;
    public float currentSize;
    public float lightScale;
    public float size;
    private float targetSize;
    public float scaleSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.GetComponent<CharacterDriver>().holdingHands && !player2.GetComponent<CharacterDriver>().holdingHands)
        {
            transform.position = (player1.transform.position + player2.transform.position) / 2;
            transform.LookAt(transform.position + (player1.transform.forward + player2.transform.forward).normalized);
            targetSize = 0;
        }
        else
        {
            targetSize = size;

            
        }
        currentSize += (targetSize - currentSize) * scaleSpeed * Time.deltaTime;
        currentSize = Mathf.Clamp(currentSize, 0, size);
        orb.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        light.intensity = currentSize * lightScale;
    }
}

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
    public CharacterController cc;
    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        cc.enabled = false;
        if (player1.GetComponent<CharacterDriver>().holdingHands && player2.GetComponent<CharacterDriver>().holdingHands)
        {
            targetSize = size;
            cc.enabled = true;
            
            
        }
        else
        {
            transform.position = (player1.transform.position + player2.transform.position) / 2;
            transform.LookAt(transform.position + (player1.transform.forward + player2.transform.forward).normalized);
            targetSize = 0;

        }
        currentSize += (targetSize - currentSize) * scaleSpeed * Time.deltaTime;
        currentSize = Mathf.Clamp(currentSize, 0, size);
        orb.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        light.intensity = currentSize * lightScale;
    }
}

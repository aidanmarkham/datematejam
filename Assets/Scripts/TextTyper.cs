using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour {

    public string text;
    private int currentIndex;
    public float typeSpeed;
    private float timer;
    public bool done;
    
	// Use this for initialization
	void Start () {
        currentIndex = 0;
        timer = 0;
        done = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > typeSpeed)
        {
            currentIndex += 1;
            timer = 0;
        }
        currentIndex = Mathf.Clamp(currentIndex, 0, text.Length);
        GetComponent<Text>().text = text.Substring(0, currentIndex);

        if(currentIndex == text.Length)
        {
            done = true;
        }
	}
}

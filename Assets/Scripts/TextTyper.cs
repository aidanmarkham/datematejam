using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour
{

    public string text;
    private int currentIndex;
    public float typeSpeed;
    private float timer;
    public bool done;

    public AudioClip[] voiceSounds;
    public float minPitch;
    public float maxPitch;
    public float offSet;
    public float targetPitch;
    public AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        currentIndex = 0;
        timer = 0;
        done = false;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > typeSpeed)
        {
            currentIndex += 1;
            if (!done)
            {
                audioSource.PlayOneShot(voiceSounds[Random.Range(0, voiceSounds.Length)]);
            }
            if (Random.Range(0f, 1f) > .5f)
            {
                targetPitch += offSet;
            }
            else
            {
                targetPitch -= offSet;
            }
            targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);
            audioSource.pitch = targetPitch;
            timer = 0;
        }
        currentIndex = Mathf.Clamp(currentIndex, 0, text.Length);
        GetComponent<Text>().text = text.Substring(0, currentIndex);

        if (currentIndex == text.Length)
        {
            done = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowySteps : MonoBehaviour {
    public AudioClip[] clips;
	void Step()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}

using UnityEngine;
using System.Collections;

public class TextScroll : MonoBehaviour {

    public string text;

    public float delay;
    public float type;
    public float wait;
    public float fade;
    public bool interrupt;
    public delegate void Task();
    public Task task;
    public bool doerDone;
    public TextScroll(string t)
    {
        text = t;
        delay = 2f;
        type = .05f;
        wait = .5f;
        fade = 1f;
        interrupt = false;
        task = null;
        doerDone = true;
    }
    public TextScroll(string t, Task ta)
    {
        text = t;
        delay = 2f;
        type = .05f;
        wait = .5f;
        fade = 1f;
        interrupt = false;
        task = ta;
        doerDone = false;
    }
    public TextScroll(string t, bool i)
    {
        text = t;
        delay = 2f;
        type = .05f;
        wait = .5f;
        fade = 1f;
        interrupt = i;
        task = null;
        doerDone = true;
    }
    public TextScroll(string t, float d, bool i)
    {
        text = t;
        delay = d;
        type = .05f;
        wait = .5f;
        fade = 1f;
        interrupt = i;
        task = null;
        doerDone = true;
    }
    public TextScroll(string t, float d, float ty, float w, float f, bool i)
    {
        text = t;
        delay = d;
        type = ty;
        wait = w;
        fade = f;
        interrupt = i;
        task = null;
        doerDone = true;
    }
}

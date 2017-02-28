using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class TextScroller : MonoBehaviour
{
    public LinkedList<TextScroll> texts = new LinkedList<TextScroll>();
    public TextScroll textScroll;
    public string[] queuedLines;

    public Stage stage;

    public float timer;

    public string currentString;

    public int stringIndex;

    public float alpha;
    public bool showTheText;
    public AudioClip soundEffect;
    public AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        Reset();
        AddText(textScroll);
    }
    public enum Stage { Delay, Type, Wait, Fade, Hold };
    // Update is called once per frame
    void Update()
    {

        

        if (stage == Stage.Delay)
        {
            
            if (timer > texts.First.Value.delay && showTheText)
            {
                timer = 0;
                stage = Stage.Type;
                
            }
        }
        if (stage == Stage.Type)
        { 
            if (timer > texts.First.Value.type)
            {
                timer = 0;
                if (stringIndex < texts.First.Value.text.Length)
                {
                    stringIndex++;
                    audioSource.PlayOneShot(soundEffect);
                }
                else
                {
                    stage = Stage.Wait;
                }
            }

            currentString = texts.First.Value.text.Substring(0, stringIndex);
            
        }
        
        if (stage == Stage.Wait)
        {
            
            if (timer > texts.First.Value.wait)
            {
                timer = 0;
                stage = Stage.Fade;
            }
        }
        if (stage == Stage.Fade)
        {
            if(texts.First.Value.doerDone == false)
            {
                texts.First.Value.doerDone = true;
                texts.First.Value.task();
            }
            alpha -= texts.First.Value.fade * Time.deltaTime;
            if (alpha < 0)
            {
                texts.RemoveFirst();
                Reset();
            }
        }
        if (stage == Stage.Hold)
        {
            
            
            if(texts.Count > 0)
            {
                stage = Stage.Delay;
                
            }
            else
            {
                showTheText = false;    
            }
        }

        

        GetComponent<Text>().text = currentString;
        GetComponent<Text>().color = new Color(1, 1, 1, alpha);
        timer += Time.deltaTime;
    }

    void Reset()
    {
        stage = Stage.Hold;
        stringIndex = 0;
        currentString = "";
        alpha = 1;
    }

    public void AddText(TextScroll t)
    {
        if (t.interrupt)
        {
            texts.RemoveFirst();
            texts.AddFirst(t);
            Reset();
        }
        else
        {
            texts.AddLast(t);
        }
    }

    
}

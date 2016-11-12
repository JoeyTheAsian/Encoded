﻿using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
    public GameObject ActiveBackground;
    public GameObject InactiveBackground;
    //enums for the different possible transitions between backgrounds
    public enum transitions {
        //fades in active, fades out inactive simultaneously
        Fade,
        FadeToWhite,
        FadeToBlack
    }
	// Use this for initialization
	void Start () {
        ActiveBackground = GameObject.Find("Background1");
        InactiveBackground = GameObject.Find("Background2");
        //ChangeBackground("test2", transitions.Fade);
        ActiveBackground.GetComponent<Background>().MakeTransparent();
        InactiveBackground.GetComponent<Background>().MakeTransparent();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            ChangeBackground("test2", transitions.Fade);
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            ChangeBackground("test2", transitions.FadeToBlack);
        }
    }
    //changes the current background only a file name is required
    public void ChangeBackground(string s)
    {
        Sprite newBackground = Resources.Load<Sprite>("Backgrounds/" + s);
        if ((newBackground) == null)
        {
            newBackground = Resources.Load<Sprite>("Backgrounds/NoTexture");
        }
        if (ActiveBackground.GetComponent<SpriteRenderer>().sprite != null) {
            InactiveBackground.GetComponent<SpriteRenderer>().sprite = ActiveBackground.GetComponent<SpriteRenderer>().sprite;
        }
        ActiveBackground.GetComponent<SpriteRenderer>().sprite = newBackground;
        ActiveBackground.tag = "ActiveBackground";
        InactiveBackground.tag = "InactiveBackground";
        ActiveBackground.GetComponent<Background>().AutoSize();
        InactiveBackground.GetComponent<Background>().AutoSize();
    }
    //for changing the background with a transition, default duration is 1 second, use overloaded method for custom duration
    public void ChangeBackground(string s, transitions t) {
        Sprite newBackground = Resources.Load<Sprite>("Backgrounds/" + s);
        if ((newBackground) == null) {
            newBackground = Resources.Load<Sprite>("Backgrounds/NoTexture");
        }
        if (ActiveBackground.GetComponent<SpriteRenderer>().sprite != null) {
            InactiveBackground.GetComponent<SpriteRenderer>().sprite = ActiveBackground.GetComponent<SpriteRenderer>().sprite;
        }
        InactiveBackground.GetComponent<SpriteRenderer>().sprite = ActiveBackground.GetComponent<SpriteRenderer>().sprite;
        ActiveBackground.GetComponent<SpriteRenderer>().sprite = newBackground;
        ActiveBackground.GetComponent<Background>().AutoSize();
        InactiveBackground.GetComponent<Background>().AutoSize();
        Transition(t);
    }
    public void Transition( transitions t) {
        switch (t) {
            case transitions.Fade:
                ActiveBackground.GetComponent<Background>().MakeTransparent();
                ActiveBackground.GetComponent<Background>().FadeInInit(1f);
                InactiveBackground.GetComponent<Background>().FadeOutInit(1f);
                break;
            case transitions.FadeToBlack:
                InactiveBackground.GetComponent<SpriteRenderer>().sprite = ActiveBackground.GetComponent<SpriteRenderer>().sprite;
                ActiveBackground.GetComponent<SpriteRenderer>().sprite = null;
                InactiveBackground.GetComponent<Background>().MakeOpaque();
                InactiveBackground.GetComponent<Background>().FadeOutInit(1f);
                //ActiveBackground.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }
}

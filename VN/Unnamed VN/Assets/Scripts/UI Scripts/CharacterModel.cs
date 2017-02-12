using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterModel : MonoBehaviour {
	public Animation animationObj;
    public SkinnedMeshRenderer meshRenderer;
    public Animation eyes;
    float blinkTimer;
    //the percentage offset of each degree of freedom in a vector3
    //maximum value of +/-.5f since all objects are centered
    public Vector3 offsetPercentage;
    public enum animations
    {
        Idle,
        Blink
    }
    public List<animations> animationState;
    public void Start()
    {
        animationObj = gameObject.GetComponent<Animation>();
        //animationState.Add(animations.Idle);
        AutoSize();
    }
    public void Update()
    {
        /*switch (animationState){
            case animations.Idle:
                animationObj.Play("Idle");
                break;
            case animations.None:
                break;
        }*/
        if (animationState.Contains(animations.Blink))
        {
            if (blinkTimer <= 0f)
            {
                blinkTimer = UnityEngine.Random.Range(4f, 10f);
                eyes.Play("Blink");
            }
            else
            {
                blinkTimer -= Time.deltaTime;
            }
        }
        if (animationState.Contains(animations.Idle))
        {
            animationObj.Play("Idle");
        }
    }
    //start & stop animations, bad parameter cases currently not handled
    public bool StartAnimation(string animation)
    {
        if(animation.ToUpper() == "IDLE" && !animationState.Contains(animations.Idle))
        {
            animationState.Add(animations.Idle);
            return true;
        }
        else if (animation.ToUpper() == "BLINK" && !animationState.Contains(animations.Blink))
        {
            animationState.Add(animations.Blink);
            return true;
        }
        return false;
    }
    public void StopAnimation(string animation)
    {
        if (animation.ToUpper() == "IDLE" && animationState.Contains(animations.Idle))
        {
            animationState.Remove(animations.Idle);
        }
        else if (animation.ToUpper() == "BLINK" && animationState.Contains(animations.Blink))
        {
            animationState.Remove(animations.Blink);
        }
    }
    public void StopAllAnimations()
    {
        if(animationState.Count > 0)
        {
            animationState.Clear();
        }
    }
    //scale to screen
	public void AutoSize()
    {
        try
        {
            gameObject.transform.localScale = new Vector3((offsetPercentage.x >= 0) ? 40 : -40, 40, 1);

            gameObject.transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect + Camera.main.orthographicSize * 2f * Camera.main.aspect * (offsetPercentage.x / 100),
                                                        Camera.main.transform.position.y + Camera.main.orthographicSize + Camera.main.orthographicSize * 2f * (offsetPercentage.y / 100), 0f);
        }
        catch (NullReferenceException) { }
    }
}

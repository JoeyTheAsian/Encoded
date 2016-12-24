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
    public void StartAnimation(string animation)
    {
        if(animation.ToUpper() == "IDLE" && !animationState.Contains(animations.Idle))
        {
            animationState.Add(animations.Idle);
        }
        else if (animation.ToUpper() == "BLINK" && !animationState.Contains(animations.Blink))
        {
            animationState.Add(animations.Blink);
        }
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

            float width = meshRenderer.bounds.size.x;
            float height = meshRenderer.bounds.size.y;

            float imageRatio = width / height;

            float finalHeight;
            float finalWidth;

            finalHeight = Camera.main.orthographicSize * 4f/3f;
            finalWidth = finalHeight;

            gameObject.transform.position = Camera.main.transform.position + Camera.main.orthographicSize * 2f * Camera.main.aspect * offsetPercentage;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
        catch (NullReferenceException) { }
    }
}

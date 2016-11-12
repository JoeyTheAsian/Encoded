using UnityEngine;
using System;
using System.Collections;

public class Background : MonoBehaviour {
    bool FadeInActive = false;
    bool FadeOutActive = false;
    bool FadeToWhiteActive = false;
    float ActiveTransitionDur = 0.0f;

    // Use this for initialization
    void Start() {
            AutoSize();
    }
	
	// Update is called once per frame
	void Update () {
        if (FadeInActive) {
            FadeIn(ActiveTransitionDur);
            if(gameObject.GetComponent<SpriteRenderer>().color.a >= 1f) {
                FadeInActive = false;
            }
        }
        else if (FadeOutActive) {
            FadeOut(ActiveTransitionDur);
            if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0) {
                FadeOutActive = false;
            }
        }
        else if (FadeToWhiteActive) {
            FadeToWhite(ActiveTransitionDur);
            if (gameObject.GetComponent<SpriteRenderer>().color.r >= 255f) {
                FadeToWhiteActive = false;
            }
        }
    }
    public void AutoSize() {
        try {
            if (gameObject.GetComponent<SpriteRenderer>() != null) {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                float imagex = gameObject.GetComponent<SpriteRenderer>().sprite.texture.width;
                float imagey = gameObject.GetComponent<SpriteRenderer>().sprite.texture.height;
                float width = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                float height = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

                float cameraRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
                float imageRatio = imagex / imagey;

                float worldScreenHeight;
                float worldScreenWidth;
                if (cameraRatio < imageRatio) {
                    worldScreenHeight = Camera.main.orthographicSize * 2f;
                    worldScreenWidth = worldScreenHeight * (imagex / imagey);
                } else {
                    worldScreenWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
                    worldScreenHeight = worldScreenWidth * (imagey / imagex);
                }
                gameObject.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
                gameObject.transform.position = Camera.main.transform.position;
                gameObject.transform.Translate(new Vector3(0, 0, 1f));
            }
        } catch (NullReferenceException) { }
    }

    //make transparent or opaque
    public void MakeTransparent()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
    }
    public void MakeOpaque() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
    }

    public void FadeInInit(float duration) {
        ActiveTransitionDur = duration;
        FadeInActive = true;
    }
    public void FadeOutInit(float duration) {
        ActiveTransitionDur = duration;
        FadeOutActive = true;
    }
    public void FadeWhiteInit(float duration) {
        ActiveTransitionDur = duration;
        FadeToWhiteActive = true;
    }
    //fade a character in with a default duration of 1 second
    public void FadeIn() {
        try {
            float curAlpha = gameObject.GetComponent<SpriteRenderer>().color.a;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, curAlpha + Time.deltaTime * 1f);
        } catch (System.NullReferenceException) { }
    }
    //takes a float that specifies how long the fade will last
    //this mutates the alpha value of the texture for ONE frame, it must be called repeatedly for the actual animation to occur
    public void FadeIn(float duration)
    {
        try { 
            float curAlpha = gameObject.GetComponent<SpriteRenderer>().color.a;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, curAlpha + Time.deltaTime * (1f / duration));
            Debug.Log(curAlpha);
        }
        catch (System.NullReferenceException) { }
    }
    public void FadeOut(float duration) {
        try {
            float curAlpha = gameObject.GetComponent<SpriteRenderer>().color.a;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, curAlpha - Time.deltaTime * (1f / duration));
            Debug.Log(curAlpha);
        } catch (System.NullReferenceException) { }
    }
    public void FadeToWhite(float duration) {
        try {
            Color curColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(curColor.r + Time.deltaTime * (255f / duration), curColor.g + Time.deltaTime * (255f / duration), curColor.b + Time.deltaTime * (255f / duration));
        } catch (System.NullReferenceException) { }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

public class Character : MonoBehaviour{
	public string Name {get; set;}
    public Animation animationObj;
    //the percentage offset of each degree of freedom in a vector3
    //maximum value of +/-.5f since all objects are centered
    public Vector3 offsetPercentage;
    public enum animations
    {
        None,
        Idle
    }
    public animations animationState;
    public void Start()
    {
        animationObj = gameObject.GetComponent<Animation>();
        animationState = animations.Idle;
        offsetPercentage = new Vector3(-.15f,0f,.1f);
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
    }
	public override string ToString() {
		return string.Format("Character {{Name = {0}}}", Name);
	}
    public void AutoSize()
    {
        try
        {
            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                float posX = gameObject.transform.position.x;
                float posY = gameObject.transform.position.y;
                float width = gameObject.GetComponent<MeshRenderer>().bounds.size.x;
                float height = gameObject.GetComponent<MeshRenderer>().bounds.size.y;

                float imageRatio = width / height;

                float finalHeight;
                float finalWidth;

                finalHeight = Camera.main.orthographicSize * 4f/3f;
                finalWidth = finalHeight * imageRatio;

                gameObject.transform.localScale = new Vector3(finalWidth/width,1f, finalHeight/height);
                gameObject.transform.position = Camera.main.transform.position + (Camera.main.orthographicSize * 2f * Camera.main.aspect * offsetPercentage);
            }
        }
        catch (NullReferenceException) { }
    }
}

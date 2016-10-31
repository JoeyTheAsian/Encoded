using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public Camera MainCamera;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            float imagex = gameObject.GetComponent<SpriteRenderer>().sprite.texture.width;
            float imagey = gameObject.GetComponent<SpriteRenderer>().sprite.texture.height;
            float width = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            float height = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

            float cameraRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
            float imageRatio = imagex / imagey;

            float worldScreenHeight;
            float worldScreenWidth;
            if (cameraRatio < imageRatio)
            {
                worldScreenHeight = MainCamera.orthographicSize * 2f;
                worldScreenWidth = worldScreenHeight * (imagex / imagey);
            }
            else
            {
                worldScreenWidth = MainCamera.orthographicSize * 2f * MainCamera.aspect;
                worldScreenHeight = worldScreenWidth * (imagey / imagex);
            }
            gameObject.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
            gameObject.transform.position = MainCamera.transform.position;
            gameObject.transform.Translate(new Vector3(0, 0, 1f));
        }
    }
    public void MakeTransparent()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
    }
    //fade a character in with a default duration of 1 second
    public void FadeIn()
    {
        try
        {
                float curAlpha = GameObject.FindWithTag("ActiveBackground").GetComponent<SpriteRenderer>().color.a;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, curAlpha + Time.deltaTime * 1f);
        }
        catch (System.NullReferenceException) { }
    }
    //takes a float that specifies how long the fade will last
    public void FadeIn(float duration)
    {
        try { 
            float curAlpha = GameObject.FindWithTag("ActiveBackground").GetComponent<SpriteRenderer>().color.a;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, curAlpha + Time.deltaTime * (1f / duration));
        }
        catch (System.NullReferenceException) { }
    }
}

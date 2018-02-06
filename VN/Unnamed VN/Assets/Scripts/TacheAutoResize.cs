using UnityEngine;
using System.Collections;

public class TacheAutoResize : MonoBehaviour {

    public Camera MainCamera;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        float width = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float height = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        float imagex = gameObject.GetComponent<SpriteRenderer>().sprite.texture.width;
        float imagey = gameObject.GetComponent<SpriteRenderer>().sprite.texture.height;
        Debug.Log(imagex);
        Debug.Log(imagey);
        float worldScreenHeight = MainCamera.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * (imagex / imagey);
        //Debug.Log((float)Camera.main.pixelWidth / Camera.main.pixelHeight);
        //transform.localScale.Set(worldScreenWidth / width, worldScreenHeight / height, 1);
        gameObject.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
    }
}

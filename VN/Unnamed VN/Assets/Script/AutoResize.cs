using UnityEngine;
using System.Collections;

public class AutoResize : MonoBehaviour {

    public Camera MainCamera;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        float imagex = gameObject.GetComponent<SpriteRenderer>().sprite.texture.width;
        float imagey = gameObject.GetComponent<SpriteRenderer>().sprite.texture.height;
        float width = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float height = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        float cameraRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
        float imageRatio = imagex / imagey;

        float worldScreenHeight;
        float worldScreenWidth;
        if (cameraRatio < imageRatio ) {
            worldScreenHeight = MainCamera.orthographicSize * 2f;
            worldScreenWidth = worldScreenHeight * (imagex / imagey);
        } else {
            worldScreenWidth = MainCamera.orthographicSize * 2f * MainCamera.aspect;
            worldScreenHeight = worldScreenWidth * (imagey / imagex);
        }
        gameObject.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
    }
}

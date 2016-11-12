using UnityEngine;
using System;
using System.Collections;

public class CharacterModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AutoSize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void AutoSize() {
        if (gameObject.GetComponent<MeshRenderer>() != null) {
             gameObject.transform.localScale = new Vector3(1, 1, 1);
             float imagex = gameObject.GetComponent<MeshRenderer>().bounds.min.x;
             float imagey = gameObject.GetComponent<MeshRenderer>().bounds.min.y;
             float width = gameObject.GetComponent<MeshRenderer>().bounds.max.x - imagex;
             float height = gameObject.GetComponent<MeshRenderer>().bounds.max.y - imagey;

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
             gameObject.transform.localScale = new Vector3(worldScreenWidth/5 / width * 2, worldScreenHeight/7 / height);
            Debug.Log(worldScreenWidth + ": " + height + ":" + gameObject.transform.localScale);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth/4,Camera.main.pixelHeight/2, 10f));
         }
        //gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 90f);
    }
}

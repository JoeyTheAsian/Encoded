using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneControl : MonoBehaviour {

    public GameObject bg;

	// Use this for initialization
	void Start () {
	}

    void ChangeBG(string path) {
        Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Textures/" + path, typeof(Texture2D));
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        bg.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, rec, new Vector2(0, 0), 1);
        //Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Textures/" + path, typeof(Texture2D));
        //bg.GetComponent<Renderer>().material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("b")) ChangeBG("test2.jpg");
	}
}

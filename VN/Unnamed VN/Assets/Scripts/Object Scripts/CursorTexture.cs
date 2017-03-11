using UnityEngine;
using System.Collections;

public class CursorTexture : MonoBehaviour {
    public Texture2D cursorTexture;
	// Use this for initialization
	void Start () {
        Cursor.SetCursor(cursorTexture,Vector2.zero, CursorMode.Auto);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class PlaneResControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(1f, width, height );
        print(Screen.width);
        print(Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

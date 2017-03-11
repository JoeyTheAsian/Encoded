using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color c = gameObject.GetComponent<Image>().color;
        c = new Color(c.r,c.g,c.b,gameObject.transform.parent.GetComponent<DialogueBox>().Opacity);
        gameObject.GetComponent<Image>().color = c;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoBar : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnGUI()
    {
        gameObject.GetComponent<Text>().text = "" + System.DateTime.Now;
    }
}

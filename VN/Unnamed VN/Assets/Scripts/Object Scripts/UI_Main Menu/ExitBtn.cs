using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Button

public class ExitBtn : MonoBehaviour {

    public Button ExitButton;
	// Use this for initialization
	void Start () {
        ExitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    // Update is called once per frame
    void Update () {
	
	}
}

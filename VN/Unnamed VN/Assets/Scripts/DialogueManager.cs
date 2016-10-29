using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    public Text dialogueBox;
    string bufferText;
    float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        SetText("Time:" + timer + " seconds");
	}
    void SetText(string s)
    {
        dialogueBox.text = s;
    }
    void DisplayText()
    {
        
    }
    void AppendText(string s)
    {
        
    }
}

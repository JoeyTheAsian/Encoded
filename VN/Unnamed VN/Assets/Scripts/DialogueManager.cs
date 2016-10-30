using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    public Text dialogueBox;
    string bufferText;
    float timer;
    public float letterPause = 0.2f;
    // Use this for initialization
    void Start () {
        
        bufferText = dialogueBox.text;
        StartCoroutine(SlowIn());
    }
    IEnumerator SlowIn()
    { foreach (char letter in bufferText.ToCharArray()) {
            dialogueBox.text = dialogueBox.text + letter ;
            yield return new WaitForSeconds(letterPause);
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        SetText("Time:" + timer + " seconds");
	}
    void SetText(string s)
    {
        
    }
    void DisplayText()
    {
        
    }
    void AppendText(string s)
    {
        
    }
}

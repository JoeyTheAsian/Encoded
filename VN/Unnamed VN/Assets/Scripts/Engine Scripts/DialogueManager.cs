using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {
    //reference to the dialogue box UI element
    public Text dialogueBox;

    //A buffered queue that stores the chars to be displayed
    Queue<char> bufferText;

    //The current active text that *should* be displayed on the screen
    //(not accounting for animation buffer)
    public string currentText;

    //Timing & Timer for letter pause, should be adjustable by the user
    public float letterPause = 0.2f;
    float letterTimer;

    // Use this for initialization
    void Start () {
        SetText(currentText);
        letterTimer = letterPause;
        dialogueBox.color = new Color(dialogueBox.color.r, dialogueBox.color.g, dialogueBox.color.b, 200f);
    }

	// Update is called once per frame
	void Update () {
        //Subtract the passed time from the timer
        letterTimer -= Time.deltaTime;

        //Check if enqueued text is null, if it has any characters, 
        //and if the timer on the character delay is up
        if(bufferText != null && bufferText.Count > 0 && letterTimer <= 0.0f)
        {
            for(int i = 0; i < (int)(letterTimer * -1000f); i += (int)(letterPause * 1000f)) { 
                if (bufferText.Count > 0) { 
                    DisplayText(bufferText.Dequeue() + "");
                }
            }
            letterTimer = letterPause;
        }
    }
    //displays text to the screen
    void DisplayText(string s)
    {
        dialogueBox.text += s;
    }
    //Turn string s into a queue of chars and add to the bufferText queue
    void SetText(string s)
    {
        bufferText = new Queue<char>(s.ToCharArray());
    }
    //Add text to the end of the current active text
    void AppendText(string s)
    {
        char[] newChars = bufferText.ToArray().Concat(s.ToCharArray()).ToArray();
        bufferText = new Queue<char>(newChars);
    }
}

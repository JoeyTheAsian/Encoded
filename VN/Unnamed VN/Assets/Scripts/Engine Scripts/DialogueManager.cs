using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class DialogueManager : MonoBehaviour {
    //reference to the dialogue box UI element
    public Text dialogueBox;
    public GameObject AudioManager;

	Scripting scripting;

    //A buffered queue that stores the chars to be displayed
    Queue<char> bufferText;
    public float PercentageMargin = .02f;

    //The current active text that *should* be displayed on the screen
    //(not accounting for animation buffer)
    public string currentText;

    //Timing & Timer for letter pause, should be adjustable by the user
    public float letterPause = 0.2f;
    float letterTimer;

    public float textScrollPause = .07f;
    float textScrollTimer;
    // Use this for initialization
    void Start () {
		scripting = GameObject.Find("Scripting").GetComponent<Scripting>();
		scripting.Next();
        //SetText(currentText);
        letterTimer = letterPause;
        textScrollTimer = textScrollPause;
        dialogueBox.color = new Color(dialogueBox.color.r, dialogueBox.color.g, dialogueBox.color.b, 1f);
        RectTransform rectTransform = dialogueBox.GetComponent<RectTransform>();

        float width = GameObject.Find("DialogueBox").GetComponent<RectTransform>().rect.width;
        float height = GameObject.Find("DialogueBox").GetComponent<RectTransform>().rect.height;
        //rectTransform.sizeDelta = new Vector2(width * .96f, height * .30f);
        rectTransform.offsetMax = new Vector2(width * -PercentageMargin, width * -PercentageMargin);
        rectTransform.offsetMin = new Vector2(width * PercentageMargin, width * PercentageMargin);
        
    }

	// Update is called once per frame
	void Update () {
        //Subtract the passed time from the timer
        letterTimer -= Time.deltaTime;
		
        //Display all text on left click
		if (Input.GetMouseButtonDown(0)) {
			if (bufferText.Count > 0) {
				StringBuilder stringBuilder = new StringBuilder("", bufferText.Count);
				while (bufferText.Count > 0) {
					stringBuilder.Append(bufferText.Dequeue());
				}
				DisplayText(stringBuilder.ToString());
			}
			else {
				ClearText();
				scripting.Next();
			}
		}
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

        textScrollTimer -= Time.deltaTime;
        if(bufferText.Count > 0 && textScrollTimer <= 0) {
            AudioManager.GetComponent<AudioManager>().PlayTextScroll();
            textScrollTimer = textScrollPause;
        }
    }

	void ClearText() {
		dialogueBox.text = "";
	}

    //displays text to the screen
    void DisplayText(string s)
    {
        dialogueBox.text += s;
    }
    //Turn string s into a queue of chars and add to the bufferText queue
    public void SetText(string s)
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

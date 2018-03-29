using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour {
    public Button okButton;
    public Button cancelButton;
    public Text displayText;
    public GameObject container;

    public InputField textInput;
    public bool displayTextField = false;

    public void Display()
    {
        container.SetActive(container.activeSelf ? false : true);
    }
    public void Hide()
    {
        container.SetActive(false);
    }

    public void Save() {
        string input = textInput.text;
        if(input.ToUpper() != "LIVESAVE" && input.ToUpper() != "SCRIPT" && input != "") {
            GameObject.Find("Scripting").GetComponent<Scripting>().Save(input);
            Hide();
        }else {
            displayText.text = "Invalid File Name";
        }
        

    }
}

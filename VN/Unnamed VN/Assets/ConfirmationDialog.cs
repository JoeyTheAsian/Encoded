using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour {
    public Button okButton;
    public Button cancelButton;
    public Text displayText;
    public GameObject container;

    public string textFieldData = "Name";
    public bool displayTextField = false;
    void OnGUI()
    {
        if (displayTextField)
        {
            Vector2 pos = new Vector2(Screen.width/2f, Screen.height/2f);
            float width = gameObject.GetComponent<RectTransform>().rect.width;
            float height = gameObject.GetComponent<RectTransform>().rect.height;
            textFieldData = GUI.TextField(new Rect(pos.x - 100, pos.y - 10, 200, 20), textFieldData, 25);
        }
    }
    public void Display()
    {
        container.SetActive(container.activeSelf ? false : true);
    }
    public void Hide()
    {
        container.SetActive(false);
    }
    public void ButtonPress()
    {
        Hide();
    }
}
